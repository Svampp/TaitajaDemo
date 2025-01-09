using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : MonoBehaviour
{
    public float speed = 1; // Walking speed

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Move enemy
        rb.velocity = Vector2.right * speed;

        /* IMPORTANT!
         * You'll need to make own animations for every enemy + 
         * you'll need to make the animator same as the EnemyRocks.
         */
        // Set animations based on speed
        if (speed == 0)
        {
            anim.SetBool("IsRun", false);
        }
        else if (speed > 0)
        {
            anim.SetBool("IsRun", true);
            GetComponent<SpriteRenderer>().flipX = true;            
        }
        else if (speed < 0)
        {
            anim.SetBool("IsRun", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Change the direction
        if (collision.CompareTag("EndOfLine"))
        {
            speed *= -1;
        }
    }
}
