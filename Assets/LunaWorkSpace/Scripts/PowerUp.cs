using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public LevelManager levelManager;


    void Start(){
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform newLocation = levelManager.ChangeSeasoningLocation();
        transform.position = newLocation.position;    
    }
}
