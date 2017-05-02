using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Texture2D mazeMap;

    public GameObject wall_ver;
    public GameObject wall_ver_broken;

    public GameObject wall_hor;
    public GameObject wall_hor_broken;

    public float breakRate;


    public GameObject pillar;

    GameObject spawnedObject;

    void Start()
    {
        GenerateMaze();
    }


    public void GenerateMaze()
    {
        for(int x = 0; x < 71; x++)
        {
            for(int y = 0; y < 86; y++)
            {
                if(mazeMap.GetPixel(x,y).r == 1)
                {
                    spawnedObject = (GameObject)Instantiate(wall_hor, new Vector3(x * 1.5f, 0, y * 1.5f), Quaternion.identity);
                } 
                if(mazeMap.GetPixel(x,y).g == 1)
                {
                    spawnedObject = (GameObject)Instantiate(wall_ver, new Vector3(x * 1.5f, 0, y * 1.5f), Quaternion.identity);
                }
                if (mazeMap.GetPixel(x, y).b == 1)
                {
                    spawnedObject = (GameObject)Instantiate(pillar, new Vector3(x * 1.5f, 0, y * 1.5f), Quaternion.identity);
                }
            }
        }
    }
	
}
