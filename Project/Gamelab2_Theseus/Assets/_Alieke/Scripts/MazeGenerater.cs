using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//r = walls 0
//b = pillar 1
//otherwise nothing 2

//ExecuteInEditMode]
public class MazeGenerater : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;

    int[,] intArray;

    public int xRes, yRes;

    public void Awake()
    {
        GenerateLabirynth();
    }

	public void GenerateLabirynth()
    {
        intArray = new int[xRes , yRes];

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Texture2D tex = renderer.material.mainTexture as Texture2D;

        for (int x = 0; x < xRes; x++)
        {
            for (int y = 0; y < yRes; y++)
            {

                if(tex.GetPixel(x,y).r >= .5f)
                {
                    intArray[x,y] = 0;
                }
                else if(tex.GetPixel(x, y).b >= .5f)
                {
                    intArray[x, y] = 1;
                }
                else
                {
                    intArray[x, y] = 2;
                }
            }
        }

        for(int x = 0; x < xRes ; x++)
        {
            for(int y = 0; y < yRes; y++)
            {
                if (intArray[x,y] == 0)
                {
                    float localX   =  x / 640 - 0.5f * 10;
                    float localY  = y / 480 - 0.5f * 10;
                    var worldPos = transform.TransformPoint(new Vector3(localX, 0, localY));
                    Instantiate(cube, worldPos, Quaternion.identity);
                }
                else if (intArray[x, y] == 1)
                {
                    float localX = x / 640 - 0.5f * 10;
                    float localY = y / 480 - 0.5f * 10;
                    var worldPos = transform.TransformPoint(new Vector3(localX, 0, localY));
                    Instantiate(sphere, worldPos, Quaternion.identity);
                }
            }
        }

    }

    Vector3 UvTo3D(Vector2 uv)  {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        int[] tris = mesh.triangles;
        Vector2[] uvs = mesh.uv;
        Vector3[] verts = mesh.vertices;

        for (int i = 0; i< tris.Length; i += 3){
            Vector2 u1 = uvs[tris[i]]; 
            Vector2 u2 = uvs[tris[i + 1]];
            Vector2 u3 = uvs[tris[i + 2]];
 
            float a = Area(u1, u2, u3); if (a == 0) continue;
            float a1 = Area(u2, u3, uv)/a; if (a1< 0) continue;
            float a2 = Area(u3, u1, uv)/a; if (a2< 0) continue;
            float a3 = Area(u1, u2, uv)/a; if (a3< 0) continue;

            Vector3 p3D = a1 * verts[tris[i]]+a2 * verts[tris[i + 1]]+a3* verts[tris[i + 2]];
            return transform.TransformPoint(p3D);
        }
        return Vector3.zero;
    }


    float Area(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        Vector2 v1 = p1 - p3;
        Vector2 v2 = p2 - p3;
        return (v1.x * v2.y - v1.y * v2.x) / 2;
    }
}
