using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public LevelManager levelManager;
    public UIManager uIManager;
    public List<Sprite> seasonLook;
    SpriteRenderer spriteRenderer;
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeSpriteLook();
        collision.GetComponentInParent<Animator>().Play("Grab");        
            Transform newLocation = levelManager.ChangeSeasoningLocation();
            transform.position = newLocation.position;
            uIManager.IncreaseScore(1);
        
    }

    void ChangeSpriteLook()
    {
        spriteRenderer.sprite = seasonLook[Random.Range(0, seasonLook.Count)];
    }
}
