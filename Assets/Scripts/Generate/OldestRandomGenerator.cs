using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OldestRandomGenerator : BaseGenerator {

    public OldestRandomGenerator(Maze maze, int randomChance) : base(maze)
    {
        this.randomChance = randomChance;
    }

    int randomChance;

    public override IEnumerator Generate()
    {
        bool freeCell = false;
        List<DIRECTIONS> availableDirections = new List<DIRECTIONS>();
        availableDirections = CheckSurroundingTiles();

        if (availableDirections.Count > 0)
            freeCell = true;

        if (freeCell)
        {
            switch (ChooseNextTile(availableDirections))
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

        if(maze.rand.Next(0,100) > randomChance)
        {
            if (branchCells.Count > 0)
            {
                index = 0;
                currentPosition = branchCells[index];
            }
            else
            {
                maze.generated = true;
            }
        }
        else
        {
            if (branchCells.Count > 1)
            {
                index = maze.rand.Next(0, branchCells.Count - 1);
                currentPosition = branchCells[index];
            }
            else if (branchCells.Count > 0)
            {
                index = 0;
                currentPosition = branchCells[index];
            }
            else
            {
                maze.generated = true;
            }
        }
        return base.Generate();
    }
}
