using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperMove : MonoBehaviour
{
    /*inspector field that allows us to randomize speed */
    [Header("Randomize Speed")]
    public int minSpeed = 10;
    public int maxSpeed = 25;
    private int moveSpeed = 0; //randomized speed
    private int direction = 0; //direction to move -1 or 1
    private Rigidbody2D shopperRB; //shopper's rigidBody
    private SpriteRenderer sprite; //shopper's spriterenderer
    // Start is called before the first frame update
    void Start()
    {
        direction = randomDirection();
        shopperRB = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    int randomDirection()
    {
        int newDirection = Random.Range(0, 2);
        int returnDirection = 0;
        
        if (newDirection == 0)
            returnDirection = -1;
        else if (newDirection == 1)
            returnDirection = 1;

        return returnDirection;
    }

    void flipShopper()
    {
        if (direction == -1) //lock left
            sprite.flipX = true;
        else if (direction == 1) //look right
            sprite.flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        shopperRB.velocity = new Vector3(direction * moveSpeed, shopperRB.velocity.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("InvisibleWall"))
            direction *= -1;
            flipShopper();
    }
}
