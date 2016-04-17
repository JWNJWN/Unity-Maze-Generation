using UnityEngine;
using System.Collections;

public class PlainMazeRenderer : BaseMazeRenderer
{
    private float halfWallWidth = 0.1f;

    public PlainMazeRenderer(Maze maze) : base(maze) { }

    enum DIRECTION { N, S, E, W };

    public override void Render()
    {
        base.Render();

        for (int x = 0; x < maze.mazeWidth; x++) 
        {
            for (int y = 0; y < maze.mazeHeight; y++) 
            {
                switch(maze.mazeCells[x, y])
                {
                    case 0:
                        DrawCell(new Vector3(x, y, 0), new Vector2(3, 0), meshData);
                        break;
                    case 1:
                        DrawCell(new Vector3(x, y, 0), new Vector2(3, 1), meshData);
                        break;
                    case 2:
                        DrawCell(new Vector3(x, y, 0), new Vector2(3, 3), meshData);
                        break;
                    case 3:
                        DrawCell(new Vector3(x, y, 0), new Vector2(3, 2), meshData);
                        break;
                    case 4:
                        DrawCell(new Vector3(x, y, 0), new Vector2(0, 0), meshData);
                        break;
                    case 5:
                        DrawCell(new Vector3(x, y, 0), new Vector2(0, 1), meshData);
                        break;
                    case 6:
                        DrawCell(new Vector3(x, y, 0), new Vector2(0, 3), meshData);
                        break;
                    case 7:
                        DrawCell(new Vector3(x, y, 0), new Vector2(0, 2), meshData);
                        break;
                    case 8:
                        DrawCell(new Vector3(x, y, 0), new Vector2(2, 0), meshData);
                        break;
                    case 9:
                        DrawCell(new Vector3(x, y, 0), new Vector2(2, 1), meshData);
                        break;
                    case 10:
                        DrawCell(new Vector3(x, y, 0), new Vector2(2, 3), meshData);
                        break;
                    case 11:
                        DrawCell(new Vector3(x, y, 0), new Vector2(2, 2), meshData);
                        break;
                    case 12:
                        DrawCell(new Vector3(x, y, 0), new Vector2(1, 0), meshData);
                        break;
                    case 13:
                        DrawCell(new Vector3(x, y, 0), new Vector2(1, 1), meshData);
                        break;
                    case 14:
                        DrawCell(new Vector3(x, y, 0), new Vector2(1, 3), meshData);
                        break;
                    case 15:
                        DrawCell(new Vector3(x, y, 0), new Vector2(1, 2), meshData);
                        break;
                }
            }
        }
    }
}
