using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public LevelManager levelManager;
    public UIManager uIManager;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Transform newLocation = levelManager.ChangeSeasoningLocation();
        transform.position = newLocation.position;
        uIManager.IncreaseScore(1);
    }
}
