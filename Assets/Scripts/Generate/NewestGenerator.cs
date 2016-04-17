﻿public class NewestGenerator : BaseGenerator {

    public NewestGenerator(Maze maze) : base(maze) { }

    public override void SelectNextCell()
    {
        index = branchCells.Count - 1;
        currentPosition = branchCells[index];
    }
}
