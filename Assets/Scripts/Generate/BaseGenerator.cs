using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseGenerator {

    public Maze maze;
    public enum DIRECTIONS {  N, S, E, W };

    public Vector2 currentPosition;

    public List<Vector2> branchCells;
    public int index = 0;

    public BaseGenerator(Maze maze)
    {
        this.maze = maze;
        branchCells = new List<Vector2>();
        currentPosition = new Vector2(maze.rand.Next(0, maze.mazeWidth), maze.rand.Next(0, maze.mazeHeight));
        branchCells.Add(currentPosition);
    }

    public virtual void AddStartEndPoints()
    {
        maze.generated = true;
        maze.startCell = new Vector2(0, maze.mazeHeight - 1);
        maze.endCell = new Vector2(maze.mazeWidth - 1, 0);
        maze.mazeCells[(int)maze.startCell.x, (int)maze.startCell.y] |= 8;
        maze.mazeCells[(int)maze.endCell.x, (int)maze.endCell.y] |= 4;
    }

    public virtual void Generate()
    {

        if (Input.touchCount == 1)
            maze.isSkipping = true;

        bool freeCell = false;
        List<DIRECTIONS> availableDirections = new List<DIRECTIONS>();
        availableDirections = CheckSurroundingTiles(currentPosition);

        if (availableDirections.Count > 0)
            freeCell = true;

        if (freeCell)
        {
            SurroundingTileCheck(currentPosition);
            if (availableDirections.Count == 1)
                branchCells.Remove(currentPosition);
            switch (ChooseDirection(availableDirections))
            {
                case DIRECTIONS.N:
                    maze.mazeCells[(int)currentPosition.x, (int)currentPosition.y] |= 1;
                    maze.mazeCells[(int)currentPosition.x, (int)currentPosition.y + 1] |= 2;
                    currentPosition += new Vector2(0, 1);
                    break;
                case DIRECTIONS.S:
                    maze.mazeCells[(int)currentPosition.x, (int)currentPosition.y] |= 2;
                    maze.mazeCells[(int)currentPosition.x, (int)currentPosition.y - 1] |= 1;
                    currentPosition += new Vector2(0, -1);
                    break;
                case DIRECTIONS.E:
                    maze.mazeCells[(int)currentPosition.x, (int)currentPosition.y] |= 4;
                    maze.mazeCells[(int)currentPosition.x + 1, (int)currentPosition.y] |= 8;
                    currentPosition += new Vector2(1, 0);
                    break;
                case DIRECTIONS.W:
                    maze.mazeCells[(int)currentPosition.x, (int)currentPosition.y] |= 8;
                    maze.mazeCells[(int)currentPosition.x - 1, (int)currentPosition.y] |= 4;
                    currentPosition += new Vector2(-1, 0);
                    break;
            }
            branchCells.Add(currentPosition);
        }
        else
        {
            branchCells.RemoveAt(index);
        }

        if (branchCells.Count > 0)
            SelectNextCell();

        if (branchCells.Count == 0)
            AddStartEndPoints();
    }

    public virtual void SelectNextCell() { }

    public DIRECTIONS ChooseDirection(List<DIRECTIONS> availableDirections)
    {
        return availableDirections[maze.rand.Next(0, availableDirections.Count)];
    }

    public void SurroundingTileCheck(Vector2 currentTile)
    {
        if (currentTile.y + 1 < maze.mazeHeight && maze.mazeCells[(int)currentTile.x, (int)currentTile.y + 1] == 0)
        {
            if (CheckSurroundingTiles(currentTile + new Vector2(0, 1)).Count == 0)
                branchCells.Remove(currentTile + new Vector2(0, 1));
        }
        if (currentTile.y - 1 >= 0 && maze.mazeCells[(int)currentTile.x, (int)currentTile.y - 1] == 0)
        {
            if (CheckSurroundingTiles(currentTile + new Vector2(0, -1)).Count == 0)
                branchCells.Remove(currentTile + new Vector2(0, -1));
        }
        if (currentTile.x + 1 < maze.mazeWidth && maze.mazeCells[(int)currentTile.x + 1, (int)currentTile.y] == 0)
        {
            if (CheckSurroundingTiles(currentTile + new Vector2(1, 0)).Count == 0)
                branchCells.Remove(currentTile + new Vector2(1, 0));
        }
        if (currentTile.x - 1 >= 0 && maze.mazeCells[(int)currentTile.x - 1, (int)currentTile.y] == 0)
        {
            if (CheckSurroundingTiles(currentTile + new Vector2(-1, 0)).Count == 0)
                branchCells.Remove(currentTile + new Vector2(-1, 0));
        }
    }

    public List<DIRECTIONS> CheckSurroundingTiles(Vector2 currentTile)
    {
        List<DIRECTIONS> availableDirections = new List<DIRECTIONS>();
        if (currentTile.y + 1 < maze.mazeHeight && maze.mazeCells[(int)currentTile.x, (int)currentTile.y + 1] == 0)
            availableDirections.Add(DIRECTIONS.N);
        if (currentTile.y - 1 >= 0 && maze.mazeCells[(int)currentTile.x, (int)currentTile.y - 1] == 0)
            availableDirections.Add(DIRECTIONS.S);
        if (currentTile.x + 1 < maze.mazeWidth && maze.mazeCells[(int)currentTile.x + 1, (int)currentTile.y] == 0)
            availableDirections.Add(DIRECTIONS.E);
        if (currentTile.x - 1 >= 0 && maze.mazeCells[(int)currentTile.x - 1, (int)currentTile.y] == 0)
            availableDirections.Add(DIRECTIONS.W);
        return availableDirections;
    }
}
