using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewestOldestGenerator : BaseGenerator {

    public NewestOldestGenerator(Maze maze, int randomChance) : base(maze)
    {
        this.randomChance = randomChance;
    }

    int randomChance;

    public override void SelectNextCell()
    {
        if(maze.rand.Next(0,100) > randomChance)
        {
            if(branchCells.Count > 0)
            {
                index = branchCells.Count - 1;
                currentPosition = branchCells[index];
            }
        }
        else
        {
            if(branchCells.Count > 0)
            {
                index = 0;
                currentPosition = branchCells[index];
            }
        }
    }
}
