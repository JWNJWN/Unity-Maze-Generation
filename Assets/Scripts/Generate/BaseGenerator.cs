using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseGenerator {

    public Maze maze;

    bool generated = false;

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
        generated = true;
        maze.mazeCells[0, maze.mazeHeight - 1] |= 8;
        maze.mazeCells[maze.mazeWidth - 1, 0] |= 4;
    }

    public virtual IEnumerator Generate()
    {
        if (branchCells.Count == 0)
            AddStartEndPoints();
        yield return new WaitForSeconds(1f);
    }

    public DIRECTIONS ChooseNextTile(List<DIRECTIONS> availableDirections)
    {
        return availableDirections[maze.rand.Next(0, availableDirections.Count - 1)];
    }

    public List<DIRECTIONS> CheckSurroundingTiles()
    {
        List<DIRECTIONS> availableDirections = new List<DIRECTIONS>();
        if (currentPosition.y + 1 < maze.mazeHeight && maze.mazeCells[(int)currentPosition.x, (int)currentPosition.y + 1] == 0)
            availableDirections.Add(DIRECTIONS.N);
        if (currentPosition.y - 1 >= 0 && maze.mazeCells[(int)currentPosition.x, (int)currentPosition.y - 1] == 0)
            availableDirections.Add(DIRECTIONS.S);
        if (currentPosition.x + 1 < maze.mazeWidth && maze.mazeCells[(int)currentPosition.x + 1, (int)currentPosition.y] == 0)
            availableDirections.Add(DIRECTIONS.E);
        if (currentPosition.x - 1 >= 0 && maze.mazeCells[(int)currentPosition.x - 1, (int)currentPosition.y] == 0)
            availableDirections.Add(DIRECTIONS.W);
        return availableDirections;
    }
}
