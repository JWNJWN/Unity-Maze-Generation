using UnityEngine;
using System.Collections.Generic;
public class PlainPlayerRenderer : BaseRenderer
{

    public PlainPlayerRenderer(Maze maze) : base(maze)
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(1, 1, hexToColor("#882D61"));
        texture.Apply();
    }


    public override void Render(List<Vector2> visitedCells)
    {
        base.Render(visitedCells);
        foreach(Vector2 vec in visitedCells)
        {
            DrawQuad(vec, vec + new Vector2(0, 1), vec + new Vector2(1, 1), vec + new Vector2(1, 0), meshData);
        }
    }
}
