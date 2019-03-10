using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Stats")]
    public int rows;
    public int columns;
    public int levelWidth;
    public int levelHeight;

    [Header("Level Pieces")]
    public int levelTypes;
    
    //Regular level pieces Type
    public List<GameObject> levelPiecesA;
    public List<GameObject> levelPiecesB;
    public List<GameObject> levelPiecesC;
    [Space]
    [Space]

    //Floor connectors
    public List<GameObject> levelPiecesD; //Level piece with holes in the floor
    public List<GameObject> levelPiecesE; //Level piece with stairs to go to upper floor
    public GameObject walls;
    public GameObject seasoning;

    public List<Transform> seasoningLocations;

    //2DList that keep tracks of all level pieces and their locations based on index
    List<List<GameObject>> map = new List<List<GameObject>>();

    //Parent of all level pieces generated on scene
    Transform levelHolder;


    private void Awake()
    {
        InitializeMap();
        PopulateLevel();
        CreateWalls();
        ConnectFloors();
        GetAllSeasoningTransforms();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        StartSeasoning();
    }

    //Randomly generates a levle by using normal level chunk pieces
    void PopulateLevel()
    {
        levelHolder = new GameObject("Holder").transform;
        int xPosition = 0;
        int yPosition = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                
                GameObject toInstantiate = Instantiate(IdToLvlType(Random.Range(0, 3)),
                    new Vector3(xPosition, yPosition, 0), Quaternion.identity);
               
                toInstantiate.transform.parent = levelHolder;
                map[i][j] = toInstantiate; //added to map 2DList of Map
                xPosition += levelWidth;
            }
            xPosition = 0;
            yPosition += levelHeight;
        }
    }
    /*
    int placeStairs()
    {
        int xPosition = 0;
        int yPosition = 0;
        int floorNum = Random.Range(0, columns);
        switch(floorNum)
        {
            case 0:
                xPosition = 0;
                break;
            case 1:
                xPosition = levelWidth;
                break;
            case 2:
                xPosition = levelWidth * 2;
                break;
        }
        GameObject stairs = Instantiate(IdToLvlType(4), 
            new Vector3(xPosition, yPosition, 0),
            Quaternion.identity);

        return floorNum;
    }
    */


    //Returns a random level piece from the level chunk specified in param
    GameObject IdToLvlType(int id)
    {
        GameObject levelPiece = null;
        switch (id) {
            case 0:
                levelPiece = levelPiecesA[Random.Range(0, levelPiecesA.Count)];
                break;
            case 1:
                levelPiece = levelPiecesB[Random.Range(0, levelPiecesB.Count)];
                break;
            case 2:
                levelPiece = levelPiecesC[Random.Range(0, levelPiecesC.Count)];
                break;
        }

        if (levelPiece == null)
            Debug.LogError("Piece no found id:" + id);
        return levelPiece;
    }

    //Populate the map 2D List GameObj base on rows & columns with empty GameObj placeholders
    void InitializeMap()
    {
        for(int i = 0; i < rows; i++)
        {
            List<GameObject> temp = new List<GameObject>();
            for(int j = 0; j < columns; j++)
            {
                var empty = new GameObject();
                temp.Add(empty);
            }
            map.Add(temp);
        }
    }

    //Puts the right and left walls on the level
    void CreateWalls()
    {
       
        for(int i = 12*rows; i > 0; i--)
        {
            Instantiate(walls, new Vector3(-1, i -7, 0), Quaternion.identity);
            Instantiate(walls, new Vector3(20*(columns)-1, i-7, 0), Quaternion.identity);
        }
    }

    //Creates path that connects floors by using special level chunks
    void ConnectFloors()
    {
        int stairs = 999999;
        for(int i = 0; i < rows -1; i++)
        {
            int tileToChange = Random.Range(0, columns);
            while (tileToChange == stairs)
                tileToChange = Random.Range(0, columns);

            GameObject choosenTile = map[i][tileToChange];
            GameObject aboveChoosenTile = map[i+1][tileToChange];
            Destroy(choosenTile);
            Destroy(aboveChoosenTile);
            
            GameObject stairsTile = (GameObject)Instantiate(levelPiecesE[Random.Range(0, levelPiecesE.Count)], new Vector3(tileToChange*levelWidth, i*levelHeight, 0), Quaternion.identity);
            map[i][tileToChange] = stairsTile;


            GameObject holeTile = (GameObject)Instantiate(levelPiecesD[Random.Range(0, levelPiecesD.Count)], new Vector3(tileToChange * levelWidth, (i+1)*levelHeight, 0), Quaternion.identity);
            map[i + 1][tileToChange] = holeTile;

            stairs = tileToChange;
        }
    }

    void GetAllSeasoningTransforms(){
        foreach(List<GameObject> list in map){
            foreach(GameObject piece in list){
                if(piece.GetComponent<LevelPiece>() != null){

                    foreach(Transform each in piece.GetComponent<LevelPiece>().seasonSpawnLocations){
                        if(each != null)
                            seasoningLocations.Add(each);
                    }
                }
            }
        }
    }

    void StartSeasoning(){
        seasoning.transform.position = ChangeSeasoningLocation().position;
        
    }

    

    public Transform ChangeSeasoningLocation(){
        int ranInt = Random.Range(0, seasoningLocations.Count);
        Transform newLocation = seasoningLocations[ranInt];
        return newLocation;
    }
}
