using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomGenerator : BaseGenerator
{

    public RandomGenerator(Maze maze) : base(maze) { }

    public override void SelectNextCell()
    {
        if(branchCells.Count > 1)
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
