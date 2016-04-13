using UnityEngine;
using System.Collections.Generic;

public class MeshData {

    //Value Key
    public Dictionary<Vector3, int> vertices = new Dictionary<Vector3, int>();
    public Dictionary<Vector3, int> colVertices = new Dictionary<Vector3, int>();
    //Key Value
    public Dictionary<int, int> triangles = new Dictionary<int, int>();
    public Dictionary<int, Vector2> uvs = new Dictionary<int, Vector2>();
    public Dictionary<int, int> colTriangles = new Dictionary<int, int>();
    public Dictionary<int, Vector3> normals = new Dictionary<int, Vector3>();

    public bool useRenderDataForCol = false;

    public MeshData() { }

    public void Clear()
    {
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();
        colVertices.Clear();
        colTriangles.Clear();
    }

    public void AddVertex(Vector3 vertex)
    {
        vertices.Add(vertex, vertices.Count);

        if (useRenderDataForCol)
        {
            colVertices.Add(vertex, colVertices.Count);
        }
    }

    public void AddTriangle(int tri)
    {
        triangles.Add(triangles.Count, tri);

        if (useRenderDataForCol)
        {
            colTriangles.Add(colTriangles.Count, tri - (vertices.Count - colVertices.Count));
        }
    }

    public void AddUV(Vector2 uv)
    {
        uvs.Add(uvs.Count, uv);
    }

    public Vector3[] VertexArray()
    {
        Vector3[] tempArray = new Vector3[vertices.Count];
        int i = 0;
        foreach (Vector3 key in vertices.Keys)
        {
            tempArray[i] = key;
            i++;
        }
        return tempArray;
    }

    public Vector3[] NormalArray()
    {
        Vector3[] tempArray = new Vector3[normals.Count];
        int i = 0;
        foreach (Vector3 key in normals.Values)
        {
            tempArray[i] = key;
            i++;
        }
        return tempArray;
    }

    public int[] TriangleArray()
    {
        int[] tempArray = new int[triangles.Count];
        int i = 0;
        foreach (int value in triangles.Values)
        {
            tempArray[i] = value;
            i++;
        }
        return tempArray;
    }

    public Vector2[] UVArray()
    {
        Vector2[] tempArray = new Vector2[uvs.Count];
        int i = 0;
        foreach (Vector2 value in uvs.Values)
        {
            tempArray[i] = value;
            i++;
        }
        return tempArray;
    }
}
