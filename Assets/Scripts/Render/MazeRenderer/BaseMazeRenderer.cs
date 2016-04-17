using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseMazeRenderer
{
    public Maze maze;

    List<Vector3> verts;
    List<Vector2> uvs;
    List<int> tris;


    public Texture2D texture;
    Texture2D[,] templateCells;
    int textureSize;

    public BaseMazeRenderer(Maze maze)
    {
        this.maze = maze;

        verts = new List<Vector3>();
        uvs = new List<Vector2>();
        tris = new List<int>();
        textureSize = 16;
        templateCells = SplitTemplateTexture();
        texture = new Texture2D(maze.mazeWidth*textureSize, maze.mazeHeight*textureSize);
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

    public virtual void DrawCell(Vector3 position)
    {
        int count = verts.Count;
        
        verts.Add(position);
        uvs.Add(new Vector2(0, 0));
        verts.Add(position + new Vector3(0, maze.mazeHeight, 0));
        uvs.Add(new Vector2(0, 1));
        verts.Add(position + new Vector3(maze.mazeWidth, maze.mazeHeight, 0));
        uvs.Add(new Vector2(1, 1));
        verts.Add(position + new Vector3(maze.mazeWidth, 0, 0));
        uvs.Add(new Vector2(1, 0));

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

    public Texture2D[,] SplitTemplateTexture()
    {
        Texture2D[,] tempTex = new Texture2D[4, 4];
        Texture2D templateTex = maze.material.GetTexture("_MainTex") as Texture2D;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Texture2D temp = new Texture2D(textureSize, textureSize);
                temp.SetPixels(0, 0, textureSize, textureSize, templateTex.GetPixels(i * textureSize, j * textureSize, textureSize, textureSize));
                tempTex[i, j] = temp;
            }
        }

        return tempTex;
    }

    public void AddCell(Vector2 newPos, Vector2 oldPos)
    {
        texture.SetPixels((int)newPos.x * textureSize, (int)newPos.y * textureSize, textureSize, textureSize, templateCells[(int)oldPos.x, (int)oldPos.y].GetPixels(0, 0, textureSize ,textureSize));
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
