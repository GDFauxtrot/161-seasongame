﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    //Use in several procedurally made objects to keep control of instances
    [System.Serializable]
    public class Counter
    {
        public int minimum;
        public int maximum;
        public Counter(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    [Header("Board Stats")]
    public int columns = 8;
    public int rows = 8;

    [Header("Amounts")]
    public Counter seasoningsCount = new Counter(5, 8);
    public Counter platformsCount = new Counter(5, 8);

    [Header("Tiles")]
    public List<GameObject> floorTiles;
    public List<GameObject> backgroundTiles;
    public List<GameObject> ceilingTiles;
    public List<GameObject> rightWallTiles;
    public List<GameObject> leftWallTiles;
    public List<GameObject> platformTiles;
    public List<GameObject> seasonings;

    public Transform levelHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void Init()
    {
        InitializeList();
        BoardSetUp();
        LayoutObject(seasonings, seasoningsCount.minimum, seasoningsCount.maximum);
        LayoutObject(platformTiles, platformsCount.minimum, platformsCount.maximum);
    }

    //Clears the grid position list and creates a new one based on rows and columns
    public void InitializeList()
    {
        gridPositions.Clear();
        for(int x = 0; x < columns -1; x++)
        {
            for(int y = 0; y < rows -1; y++)
                gridPositions.Add(new Vector3(x,y,0));            
        }
    }


    //Populates the scene based on the grid by randomly choosing tiles from the lists
    void BoardSetUp()
    {
        levelHolder = new GameObject("Board").transform;
        for(int x = -1; x < columns + 1; x++)
        {
            for(int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = backgroundTiles[Random.Range(0, backgroundTiles.Count)];
                if (x == columns)
                    toInstantiate = rightWallTiles[Random.Range(0, rightWallTiles.Count)];
                else if (x == -1)
                    toInstantiate = leftWallTiles[Random.Range(0, leftWallTiles.Count)];

                else if (y == rows)
                    toInstantiate = ceilingTiles[Random.Range(0, ceilingTiles.Count)];

                else if (y == -1 && x != -1 && x != columns)
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Count)];
                GameObject createdTile = Instantiate(toInstantiate, new Vector3(x,y,0), Quaternion.identity);
                createdTile.transform.SetParent(levelHolder);
            }
        }
    }

    //Chooses a random position from gridPositions, returns it, and deletes it from
    //gridPositions to prevent more than 1 object being place in such position
    Vector3 RandomPosition()
    {
        int randomPos = Random.Range(0, gridPositions.Count);
        Vector3 pos = gridPositions[randomPos];
        gridPositions.RemoveAt(randomPos);
        return pos; 
    }

    void LayoutObject(List<GameObject> tileObject, int min, int max )
    {
        int numberToInstantiate = Random.Range(min, max);
        for(int i = 0; i < numberToInstantiate; i++)
        {
            GameObject tileToSpawn = tileObject[Random.Range(0, tileObject.Count)];
            Instantiate(tileToSpawn, RandomPosition(), Quaternion.identity);
        }
    }

}