using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseMazeRenderer
{
    public MeshData meshData;

    public Maze maze;

    List<Vector3> verts;
    List<Vector2> uvs;
    List<int> tris;


    public Texture2D texture;

    public BaseMazeRenderer(Maze maze)
    {
        this.maze = maze;

        verts = new List<Vector3>();
        uvs = new List<Vector2>();
        tris = new List<int>();
    }

    public virtual void Render()
    {
        verts.Clear();
        uvs.Clear();
        tris.Clear();
    }

    public virtual Mesh ToMesh(Mesh mesh)
    {
        if(verts.Count == 0)
        {
            GameObject.Destroy(mesh);
            return null;
        }

        if (mesh == null)
            mesh = new Mesh();

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        return mesh;
    }

    public virtual void DrawCell(Vector3 position, Vector2 uvPosition, MeshData meshData)
    {
        int count = verts.Count;
        
        verts.Add(position);
        uvs.Add(uvPosition / 4f);
        verts.Add(position + new Vector3(0, 1, 0));
        uvs.Add((uvPosition + new Vector2(0, 1)) / 4f);
        verts.Add(position + new Vector3(1, 1, 0));
        uvs.Add((uvPosition + new Vector2(1, 1)) / 4f);
        verts.Add(position + new Vector3(1, 0, 0));
        uvs.Add((uvPosition + new Vector2(1, 0)) / 4f);

        tris.Add(count);
        tris.Add(count + 1);
        tris.Add(count + 2);

        tris.Add(count);
        tris.Add(count + 2);
        tris.Add(count + 3);
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
