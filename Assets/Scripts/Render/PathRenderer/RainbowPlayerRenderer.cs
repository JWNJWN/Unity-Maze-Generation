using UnityEngine;
using System.Collections.Generic;
public class RainbowPlayerRenderer : BaseRenderer
{

    public RainbowPlayerRenderer(Maze maze) : base(maze)
    {
        texture = new Texture2D(maze.mazeWidth, maze.mazeHeight);
        texture.filterMode = FilterMode.Point;
        for (int x = 0; x < maze.mazeWidth; x++)
        {
            for (int y = 0; y < maze.mazeHeight; y++)
            {
                texture.SetPixel(x, y, rainbowColours[maze.rand.Next(0,7)]);
            }
        }
        texture.Apply();
    }

    //ROYGBIV
    Color[] rainbowColours = new Color[] { Color.red, new Color(1, 0.6f, 0), Color.yellow, Color.green, Color.blue, new Color(.5f, 0, 1), new Color(1, 0, 1) };

    public override void Render(List<Vector2> visitedCells)
    {
        base.Render(visitedCells);

        foreach (Vector2 vec in visitedCells)
        {
            DrawQuad(vec, vec + new Vector2(0, 1), vec + new Vector2(1, 1), vec + new Vector2(1, 0), meshData, visitedCells.IndexOf(vec));
        }
    }



    public void DrawQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, MeshData meshData, int colourIndex)
    {
        if (!meshData.vertices.ContainsKey(v1))
        {
            meshData.vertices.Add(v1, meshData.vertices.Count);
            meshData.AddUV(v1 / maze.mazeHeight);
        }
        if (!meshData.vertices.ContainsKey(v2))
        {
            meshData.vertices.Add(v2, meshData.vertices.Count);
            meshData.AddUV(v2 / maze.mazeHeight);
        }
        if (!meshData.vertices.ContainsKey(v3))
        {
            meshData.vertices.Add(v3, meshData.vertices.Count);
            meshData.AddUV(v3/maze.mazeHeight);
        }
        if (!meshData.vertices.ContainsKey(v4))
        {
            meshData.vertices.Add(v4, meshData.vertices.Count);
            meshData.AddUV(v4 / maze.mazeHeight);
        }


        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v1]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v2]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v3]);

        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v1]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v3]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v4]);
    }
}
