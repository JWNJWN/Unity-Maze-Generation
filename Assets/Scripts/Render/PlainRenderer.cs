using UnityEngine;

public class PlainRenderer : BaseRenderer
{
    private float halfWallWidth = 0.1f;
    enum DIRECTION { N, S, E, W };

    public PlainRenderer() : base() { }

    public override void Render(int[,] map)
    {
        base.Render(map);
        for (int x = 0; x < map.GetLength(0); x++) 
        {
            for (int y = 0; y < map.GetLength(1); y++) 
            {
                switch(map[x, y])
                {
                    case 0:
                        //Filled
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + 1, 0), new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y, 0));
                        break;
                    case 1:
                        //Bottom
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth, 0), new Vector3(x + 1, y, 0));
                        //Right
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1, 0), new Vector3(x + 1, y + 1, 0));
                        //Left
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1 , 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x, y, 0));

                        break;
                    case 2:
                        //Right
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + 1, y + 1, 0));
                        //Top
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1, 0));
                        //Left
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + halfWallWidth, y, 0), new Vector3(x, y, 0));

                        break;
                    case 3:
                        //Right
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + 1, 0), new Vector3(x + 1, y + 1, 0));
                        //Left
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1, 0), new Vector3(x + halfWallWidth, y, 0), new Vector3(x, y, 0));

                        break;
                    case 4:
                        //Bottom
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x + 1, y + halfWallWidth, 0), new Vector3(x + 1, y, 0));
                        //Top
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y + 1 - halfWallWidth, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1, 0));
                        //Left
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x, y, 0));

                        break;
                    case 5:
                        //Bottom
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x + 1, y + halfWallWidth, 0), new Vector3(x + 1, y, 0));
                        //Left
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x, y, 0));
                        //TR
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y + 1 - halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1, 0));

                        break;
                    case 6:
                        //Top
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y + 1 - halfWallWidth, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1, 0));
                        //Left
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + halfWallWidth, y, 0), new Vector3(x, y, 0));
                        //BR
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth), new Vector3(x + 1, y + halfWallWidth, 0));
                        break;
                    case 7:
                        //Left
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1, 0), new Vector3(x + halfWallWidth, y, 0), new Vector3(x, y, 0));

                        //BR
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth), new Vector3(x + 1, y + halfWallWidth, 0));
                        //TR
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y + 1 - halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1, 0));
                        break;
                    case 8:
                        //Bottom
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth, 0), new Vector3(x + 1, y, 0));
                        //Right
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + 1, y + 1, 0));
                        //Top
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1, 0));

                        break;
                    case 9:
                        //Bottom
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth, 0), new Vector3(x + 1, y, 0));
                        //Right
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + 1, 0), new Vector3(x + 1, y + 1, 0));
                        //TL
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth), new Vector3(x, y + 1 - halfWallWidth, 0));
                        break;
                    case 10:
                        //Right
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + 1, y + 1, 0));
                        //Top
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1, 0));
                        //BL
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + halfWallWidth, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x + halfWallWidth, y, 0));

                        break;
                    case 11:
                        //Right
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + 1, 0), new Vector3(x + 1, y + 1, 0));
                        //BL
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + halfWallWidth, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x + halfWallWidth, y, 0));
                        //TL
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth), new Vector3(x, y + 1 - halfWallWidth, 0));

                        break;
                    case 12:
                        //Bottom
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + halfWallWidth, 0), new Vector3(x + 1, y + halfWallWidth, 0), new Vector3(x + 1, y, 0));
                        //Top
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1, 0));

                        break;
                    case 13:
                        //Bottom
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + halfWallWidth, 0), new Vector3(x + 1, y + halfWallWidth, 0), new Vector3(x + 1 , y, 0));
                        //TL
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth), new Vector3(x, y + 1 - halfWallWidth, 0));
                        //TR
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y + 1 - halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1, 0));
                        break;
                    case 14:
                        //Top
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1 - halfWallWidth, 0), new Vector3(x, y + 1, 0));
                        //BL
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + halfWallWidth, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x + halfWallWidth, y, 0));
                        //BR
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth), new Vector3(x + 1, y + halfWallWidth, 0));

                        break;
                    case 15:
                        //BL
                        DrawQuad(new Vector3(x, y, 0), new Vector3(x, y + halfWallWidth, 0), new Vector3(x + halfWallWidth, y + halfWallWidth, 0), new Vector3(x + halfWallWidth, y, 0));
                        //BR
                        DrawQuad(new Vector3(x + 1, y, 0), new Vector3(x + 1 - halfWallWidth, y, 0), new Vector3(x + 1 - halfWallWidth, y + halfWallWidth), new Vector3(x + 1, y + halfWallWidth, 0));
                        //TL
                        DrawQuad(new Vector3(x, y + 1, 0), new Vector3(x + halfWallWidth, y + 1, 0), new Vector3(x + halfWallWidth, y + 1 - halfWallWidth), new Vector3(x, y + 1 - halfWallWidth, 0));
                        //TR
                        DrawQuad(new Vector3(x + 1, y + 1, 0), new Vector3(x + 1, y + 1 - halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1 - halfWallWidth, 0), new Vector3(x + 1 - halfWallWidth, y + 1, 0));
                        break;
                }
            }
        }
    }
}
