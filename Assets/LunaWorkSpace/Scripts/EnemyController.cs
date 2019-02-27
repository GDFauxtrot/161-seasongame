using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public GameObject agent; //GameObject in scene
    public Stats stats; //Scrip Obj with Stats

    [Header("Patrol")]
    public List<Transform> patrolLocations;
    public int nextPatrolLocation;

    
}
