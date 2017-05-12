using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Texture2D mazeMap;

    public Walls wallPrefabs;
    public GameObject pillar;
    public float breakRate;

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
                    GameObject wall = PickPrefab();
                    spawnedObject = (GameObject)Instantiate(wall, new Vector3(x * 1.5f, 0, y * 1.5f), Quaternion.identity);
                } 
                if(mazeMap.GetPixel(x,y).g == 1)
                {
                    GameObject wall = PickPrefab();
                    spawnedObject = (GameObject)Instantiate(wall, new Vector3(x * 1.5f, 0, y * 1.5f), Quaternion.identity);
                    spawnedObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                }
                if (mazeMap.GetPixel(x, y).b == 1)
                {
                    spawnedObject = (GameObject)Instantiate(pillar, new Vector3(x * 1.5f, 0, y * 1.5f), Quaternion.identity);
                }
            }
        }
    }
	
    public GameObject PickPrefab()
    {
        GameObject prefab;
        int number = Random.Range(0, 100);
        if(number < breakRate)
        {
            number = Random.Range(0, wallPrefabs.brokenWalls.Length);
            prefab = wallPrefabs.brokenWalls[number];
        }
        else
        {
            number = Random.Range(0, wallPrefabs.normalWalls.Length);
            prefab = wallPrefabs.normalWalls[number];
        }
        return prefab;
    }
}

[System.Serializable]
public class Walls
{
    public GameObject[] brokenWalls;
    public GameObject[] normalWalls;
}
