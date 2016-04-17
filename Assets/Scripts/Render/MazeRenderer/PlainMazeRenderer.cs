using UnityEngine;
using System.Collections;

public class PlainMazeRenderer : BaseMazeRenderer
{
    public PlainMazeRenderer(Maze maze) : base(maze) { }

    enum DIRECTION { N, S, E, W };

    public override void Render()
    {
        base.Render();

        DrawCell(Vector3.zero);

        for (int x = 0; x < maze.mazeWidth; x++) 
        {
            for (int y = 0; y < maze.mazeHeight; y++) 
            {
                switch(maze.mazeCells[x, y])
                {
                    case 0:
                        AddCell(new Vector2(x, y), new Vector2(3, 0));
                        break;
                    case 1:
                        AddCell(new Vector2(x, y), new Vector2(3, 1));
                        break;
                    case 2:
                        AddCell(new Vector2(x, y), new Vector2(3, 3));
                        break;
                    case 3:
                        AddCell(new Vector2(x, y), new Vector2(3, 2));
                        break;
                    case 4:
                        AddCell(new Vector2(x, y), new Vector2(0, 0));
                        break;
                    case 5:
                        AddCell(new Vector2(x, y), new Vector2(0, 1));
                        break;
                    case 6:
                        AddCell(new Vector2(x, y), new Vector2(0, 3));
                        break;
                    case 7:
                        AddCell(new Vector2(x, y), new Vector2(0, 2));
                        break;
                    case 8:
                        AddCell(new Vector2(x, y), new Vector2(2, 0));
                        break;
                    case 9:
                        AddCell(new Vector2(x, y), new Vector2(2, 1));
                        break;
                    case 10:
                        AddCell(new Vector2(x, y), new Vector2(2, 3));
                        break;
                    case 11:
                        AddCell(new Vector2(x, y), new Vector2(2, 2));
                        break;
                    case 12:
                        AddCell(new Vector2(x, y), new Vector2(1, 0));
                        break;
                    case 13:
                        AddCell(new Vector2(x, y), new Vector2(1, 1));
                        break;
                    case 14:
                        AddCell(new Vector2(x, y), new Vector2(1, 3));
                        break;
                    case 15:
                        AddCell(new Vector2(x, y), new Vector2(1, 2));
                        break;
                }
            }
        }
    }
}
