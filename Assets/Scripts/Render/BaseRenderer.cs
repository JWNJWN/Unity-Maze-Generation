using UnityEngine;

public class BaseRenderer
{
    public MeshData meshData;

    int[,] map;

    public BaseRenderer()
    {
        meshData = new MeshData();
    }

    public virtual void Render(int[,] map)
    {
        this.map = map;
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

        mesh.RecalculateNormals();

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

    public void DrawQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        if (!meshData.vertices.ContainsKey(v1))
            meshData.vertices.Add(v1, meshData.vertices.Count);
        if (!meshData.vertices.ContainsKey(v2))
            meshData.vertices.Add(v2, meshData.vertices.Count);
        if (!meshData.vertices.ContainsKey(v3))
            meshData.vertices.Add(v3, meshData.vertices.Count);
        if (!meshData.vertices.ContainsKey(v4))
            meshData.vertices.Add(v4, meshData.vertices.Count);

        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v1]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v2]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v3]);

        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v1]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v3]);
        meshData.triangles.Add(meshData.triangles.Count, meshData.vertices[v4]);

    }
}
