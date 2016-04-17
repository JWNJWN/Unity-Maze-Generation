using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseRenderer
{
    public MeshData meshData;
    public MeshData collMeshData;

    public Maze maze;
    List<Vector2> visitedCells;

    public Texture2D texture;

    public BaseRenderer(Maze maze)
    {
        this.maze = maze;
        meshData = new MeshData();
        collMeshData = new MeshData();
    }

    public virtual void Render()
    {
        meshData.Clear();
    }

    public virtual void Render(List<Vector2> visitedCells)
    {
        this.visitedCells = visitedCells;
        meshData.Clear();
    }

    public virtual Mesh ToMesh(Mesh mesh)
    {
        if(meshData.vertices.Count == 0)
        {
            GameObject.Destroy(mesh);
            return null;
        }

        if (mesh == null)
            mesh = new Mesh();

        mesh.vertices = meshData.VertexArray();
        mesh.triangles = meshData.TriangleArray();
        mesh.uv = meshData.UVArray();

        mesh.RecalculateNormals();

        return mesh;
    }

    public virtual Mesh CollMesh()
    {
        collMeshData.Clear();
        Mesh mesh = new Mesh();

        DrawQuad(new Vector3(0,0,0), new Vector3(0, maze.mazeHeight,0), new Vector3(maze.mazeWidth, maze.mazeHeight, 0), new Vector3(maze.mazeWidth, 0, 0), collMeshData);

        return mesh;
    }

    public void DrawTri(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        if (!meshData.vertices.ContainsKey(v1))
            meshData.vertices.Add(v1, meshData.vertices.Count);
        if (!meshData.vertices.ContainsKey(v2))
            meshData.vertices.Add(v2, meshData.vertices.Count);
        if (!meshData.vertices.ContainsKey(v3))
            meshData.vertices.Add(v3, meshData.vertices.Count);

        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v1]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v2]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v3]);
    }

    public virtual void DrawQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, MeshData meshData)
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
            meshData.AddUV(v3 / maze.mazeHeight);
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

    public Texture2D GetTexture()
    {
        return texture;
    }

    public static Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
}
