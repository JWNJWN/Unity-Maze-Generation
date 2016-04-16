using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Player : MonoBehaviour {

    public Maze maze;

    //Tile Selection
    List<Vector2> visitedTiles;
    
    //Mouse Positions
    Vector3 worldMousePosition;
    Vector2 gridMousePosition;

    //Meshing
    MeshFilter meshFilter;
    BaseRenderer playerRenderer;

    //Timer
    private Stopwatch tmr;

    /*Dragging Tile Pseudo-Code
        
        OnMouseDown 
            If visitedTiles !Contains Cell
                If Cell is adjacent to already visited cell
                    visitedTiles add Cell
                    justAddedTiles add Cell
            else if justAddedTiles !Contains Cell
                visitedTile remove Cell

    */

    //Camera Controls
    float zoomSpeed = 0.5f;
    float dragSpeed = 0.1f;
    Vector3 dragOrigin;
    bool cameraDragging = false;

    void Start()
    {
        //Init
        visitedTiles = new List<Vector2>();
        playerRenderer = new PlainPlayerRenderer(maze);
        meshFilter = GetComponent<MeshFilter>();

        tmr = new Stopwatch();

        gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Texture");
        gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", playerRenderer.GetTexture());
        maze.mainCamera.orthographicSize = Mathf.Clamp(maze.mainCamera.orthographicSize, 5f, (maze.mazeHeight >= maze.mazeWidth) ? maze.mazeHeight : maze.mazeWidth);
    }

    void OccupyCell(Vector2 cellPosition)
    {
        visitedTiles.Add(cellPosition);
    }

    void RemoveBranch(Vector2 startCellPosition)
    {
        int index = visitedTiles.IndexOf(startCellPosition)+1;
        visitedTiles.RemoveRange(index, visitedTiles.Count - index);
    }

    void Update()
    {
        dragSpeed = maze.mainCamera.orthographicSize / 300;

        if (Input.touchCount == 1)
        {
            worldMousePosition = maze.mainCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(0).position);
            gridMousePosition = new Vector2(Mathf.FloorToInt(worldMousePosition.x), Mathf.FloorToInt(worldMousePosition.y));

            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    dragOrigin = Input.GetTouch(0).position;
                    if(!cameraDragging)
                    {
                        if (visitedTiles.Count > 0)
                        {
                            if(visitedTiles.Contains(gridMousePosition) && visitedTiles[visitedTiles.Count-1] != gridMousePosition)
                            {
                                RemoveBranch(gridMousePosition);
                            }
                            else if(IsCellConnected(gridMousePosition, visitedTiles[visitedTiles.Count - 1]) && !visitedTiles.Contains(gridMousePosition))
                            {
                                OccupyCell(gridMousePosition);
                            }
                        }
                        else if(gridMousePosition == maze.startCell)
                        {
                            OccupyCell(gridMousePosition);
                            tmr.Start();
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    dragOrigin = Input.GetTouch(0).position;
                    if (!cameraDragging && visitedTiles.Count > 0)
                    {
                        if (visitedTiles.Contains(gridMousePosition) && IsCellConnected(gridMousePosition, visitedTiles[visitedTiles.Count-1]))
                        {
                            RemoveBranch(gridMousePosition);
                        }
                        else if(IsCellConnected(gridMousePosition, visitedTiles[visitedTiles.Count-1]))
                        {
                            OccupyCell(gridMousePosition);
                        }
                    }
                    break;
            }
        }

        RenderMesh();
        PlayerPrefs.SetFloat("LevelCurrentTime", tmr.ElapsedMilliseconds/1000f);

        if (HasWon())
        {
            tmr.Stop();
            maze.mainCamera.GetComponent<InGame>().ChangeScene(1);
        }
    }

    void LateUpdate()
    {
        DragMove();

        ZoomHandle();
    }

    void DragMove()
    {
        if (Input.touchCount == 1)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    if (!visitedTiles.Contains(gridMousePosition) && gridMousePosition != maze.startCell)
                    {
                        cameraDragging = true;
                    }
                    break;
                case TouchPhase.Moved:
                    if (cameraDragging)
                    {
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        maze.mainCamera.transform.Translate(-touchDeltaPosition * dragSpeed, 0);
                        float cameraX = Mathf.Clamp(maze.mainCamera.transform.position.x, 0, maze.mazeWidth);
                        float cameraY = Mathf.Clamp(maze.mainCamera.transform.position.y, 0, maze.mazeHeight);

                        maze.mainCamera.transform.position = new Vector3(cameraX, cameraY, -1);
                    }
                    break;
                case TouchPhase.Ended:
                    cameraDragging = false;
                    break;
            }
        }
    }

    void ZoomHandle()
    {
        if(Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            float prevTouchDeltaMag = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float touchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;


            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            maze.mainCamera.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
            maze.mainCamera.orthographicSize = Mathf.Clamp(maze.mainCamera.orthographicSize, 5f, (maze.mazeHeight >= maze.mazeWidth) ? maze.mazeHeight : maze.mazeWidth);
        }
    }

    bool HasWon()
    {
        if (visitedTiles.Contains(maze.endCell))
            return true;
        return false;
    }

    bool IsCellConnected(Vector2 position, Vector2 referenceCell)
    {
        switch (maze.mazeCells[(int)referenceCell.x, (int)referenceCell.y])
        {
            case 0:
                break;
            case 1:
                if ((referenceCell + new Vector2(0, 1)).y < maze.mazeHeight && position == referenceCell + new Vector2(0, 1))
                    return true;
                break;
            case 2:
                if ((referenceCell + new Vector2(0, -1)).y >= 0 && position == referenceCell + new Vector2(0, -1))
                    return true;
                break;
            case 3:
                if ((referenceCell + new Vector2(0, 1)).x < maze.mazeWidth && position == referenceCell + new Vector2(0, 1))
                    return true;
                if ((referenceCell + new Vector2(0, -1)).y >= 0 && position == referenceCell + new Vector2(0, -1))
                    return true;
                break;
            case 4:
                if ((referenceCell + new Vector2(1, 0)).x < maze.mazeWidth && position == referenceCell + new Vector2(1, 0))
                    return true;
                break;
            case 5:
                if ((referenceCell + new Vector2(0, 1)).y < maze.mazeHeight && position == referenceCell + new Vector2(0, 1))
                    return true;
                if ((referenceCell + new Vector2(1, 0)).x < maze.mazeWidth && position == referenceCell + new Vector2(1, 0))
                    return true;
                break;
            case 6:
                if ((referenceCell + new Vector2(0, -1)).y >= 0 && position == referenceCell + new Vector2(0, -1))
                    return true;
                if ((referenceCell + new Vector2(1, 0)).x < maze.mazeWidth && position == referenceCell + new Vector2(1, 0))
                    return true;
                break;
            case 7:
                if ((referenceCell + new Vector2(0, 1)).y < maze.mazeHeight && position == referenceCell + new Vector2(0, 1))
                    return true;
                if ((referenceCell + new Vector2(0, -1)).y >= 0 && position == referenceCell + new Vector2(0, -1))
                    return true;
                if ((referenceCell + new Vector2(1, 0)).x < maze.mazeWidth && position == referenceCell + new Vector2(1, 0))
                    return true;
                break;
            case 8:
                if ((referenceCell + new Vector2(-1, 0)).x >= 0 && position == referenceCell + new Vector2(-1, 0))
                    return true;
                break;
            case 9:
                if ((referenceCell + new Vector2(0, 1)).y < maze.mazeHeight && position == referenceCell + new Vector2(0, 1))
                    return true;
                if ((referenceCell + new Vector2(-1, 0)).x >= 0 && position == referenceCell + new Vector2(-1, 0))
                    return true;
                break;
            case 10:
                if ((referenceCell + new Vector2(0, -1)).y >= 0 && position == referenceCell + new Vector2(0, -1))
                    return true;
                if ((referenceCell + new Vector2(-1, 0)).x >= 0 && position == referenceCell + new Vector2(-1, 0))
                    return true;
                break;
            case 11:
                if ((referenceCell + new Vector2(0, 1)).y < maze.mazeHeight && position == referenceCell + new Vector2(0, 1))
                    return true;
                if ((referenceCell + new Vector2(0, -1)).y >= 0 && position == referenceCell + new Vector2(0, -1))
                    return true;
                if ((referenceCell + new Vector2(-1, 0)).x >= 0 && position == referenceCell + new Vector2(-1, 0))
                    return true;
                break;
            case 12:
                if ((referenceCell + new Vector2(1, 0)).x < maze.mazeWidth && position == referenceCell + new Vector2(1, 0))
                    return true;
                if ((referenceCell + new Vector2(-1, 0)).x >= 0 && position == referenceCell + new Vector2(-1, 0))
                    return true;
                break;
            case 13:
                if ((referenceCell + new Vector2(0, 1)).y < maze.mazeHeight && position == referenceCell + new Vector2(0, 1))
                    return true;
                if ((referenceCell + new Vector2(1, 0)).x < maze.mazeWidth && position == referenceCell + new Vector2(1, 0))
                    return true;
                if ((referenceCell + new Vector2(-1, 0)).x >= 0 && position == referenceCell + new Vector2(-1, 0))
                    return true;
                break;
            case 14:
                if ((referenceCell + new Vector2(0, -1)).y >= 0 && position == referenceCell + new Vector2(0, -1))
                    return true;
                if ((referenceCell + new Vector2(1, 0)).x < maze.mazeWidth && position == referenceCell + new Vector2(1, 0))
                    return true;
                if ((referenceCell + new Vector2(-1, 0)).x >= 0 && position == referenceCell + new Vector2(-1, 0))
                    return true;
                break;
            case 15:
                if ((referenceCell + new Vector2(0, 1)).y < maze.mazeHeight && position == referenceCell + new Vector2(0, 1))
                    return true;
                if ((referenceCell + new Vector2(0, -1)).y >= 0 && position == referenceCell + new Vector2(0, -1))
                    return true;
                if ((referenceCell + new Vector2(1, 0)).x < maze.mazeWidth && position == referenceCell + new Vector2(1, 0))
                    return true;
                if ((referenceCell + new Vector2(-1, 0)).x >= 0 && position == referenceCell + new Vector2(-1, 0))
                    return true;
                break;
        }
        return false;
    }
    
    void RenderMesh()
    {
        playerRenderer.Render(visitedTiles);
        meshFilter.sharedMesh = playerRenderer.ToMesh(meshFilter.sharedMesh);
    }

}
