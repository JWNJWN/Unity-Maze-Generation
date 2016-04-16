using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OldestRandomGenerator : BaseGenerator
{

    public OldestRandomGenerator(Maze maze, int randomChance) : base(maze)
    {
        this.randomChance = randomChance;
    }

    int randomChance;

    public override void SelectNextCell()
    {

        if (maze.rand.Next(0, 100) > randomChance)
        {
                index = 0;
                currentPosition = branchCells[index];
        }
        else
        {
            if (branchCells.Count > 1)
            {
                index = maze.rand.Next(0, branchCells.Count - 1);
                currentPosition = branchCells[index];
            }
            else 
            {
                index = 0;
                currentPosition = branchCells[index];
            }
        }
    }
}

