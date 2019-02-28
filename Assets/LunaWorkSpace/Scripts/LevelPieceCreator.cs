using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPieceCreator : MonoBehaviour
{
    [Header("Level Stats")]
    public int rows;
    public int columns;
    public int levelWidth;
    public int levelHeight;

    [Header("Level Pieces")]
    public int levelTypes;
    public List<GameObject> levelPiecesA;
    public List<GameObject> levelPiecesB;
    public List<GameObject> levelPiecesC;
    public List<GameObject> levelPiecesD;
    public List<GameObject> levelPiecesE;
    public List<GameObject> levelPiecesF;

    Dictionary<Vector3, int> levelMap;
    Transform levelHolder;

    // Start is called before the first frame update
    void Start()
    {
        PopulateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopulateLevel()
    {
        levelHolder = new GameObject("Holder").transform;
        //int length = Random.Range(minLvP, maxLvP);
        int xPosition = 0;
        int yPosition = 0;
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GameObject toInstantiate = Instantiate(IdToLvlType(ChooseRandomPiece()),
                    new Vector3(xPosition, yPosition,0),
                    Quaternion.identity);
                toInstantiate.transform.parent = levelHolder;
                xPosition += levelWidth;
            }
            xPosition = 0;
            yPosition += levelHeight;
        }
    }

    int ChooseRandomPiece()
    {
        return Random.Range(0, levelTypes);
        
    }

    //Maps the id to a randomly grab list of pieces from the right piece based on ID
    GameObject IdToLvlType(int id)
    {
        GameObject levelPiece = null;
        switch (id) {
            case 0:
                levelPiece =levelPiecesA[Random.Range(0, levelPiecesA.Capacity)];
                break;
            case 1:
                levelPiece = levelPiecesB[Random.Range(0, levelPiecesB.Capacity)];
                break;
            case 2:
                levelPiece = levelPiecesC[Random.Range(0, levelPiecesC.Capacity)];
                break;
            case 3:
                levelPiece = levelPiecesD[Random.Range(0, levelPiecesD.Capacity)];
                break;
            case 4:
                levelPiece = levelPiecesE[Random.Range(0, levelPiecesE.Capacity)];
                break;
            case 5:
                levelPiece = levelPiecesF[Random.Range(0, levelPiecesF.Capacity)];
                break;
           
        }

        if (levelPiece == null)
            Debug.LogError("Piece no found id:" + id);
        return levelPiece;
    }

    
}
