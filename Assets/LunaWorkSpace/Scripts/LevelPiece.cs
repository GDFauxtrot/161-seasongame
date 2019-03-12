using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPiece: MonoBehaviour
{
    public int lvlID;
    public List<int> illegalAdjacentLvls;
    public List<int> requiredAdjacentLvl;
    public List<int> requiredUpperLvl;

    public List<Transform> seasonSpawnLocations;
    public List<Transform> shoppersSpawnLocations;
}
