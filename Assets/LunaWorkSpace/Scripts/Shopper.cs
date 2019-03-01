using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shopper : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float slowDownAmount;
    public float smoothness;

    [Header("Physics")]
    public Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, speed, smoothness), 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            speed = -speed;
        
    }
}
