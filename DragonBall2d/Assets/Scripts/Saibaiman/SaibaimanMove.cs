using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaibaimanMove : MonoBehaviour
{
    public float speed = 0.3f;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb2D;
    Animator animator;

    public Transform Portal1_5;
    public Transform Portal1;
    public Transform Portal2_5;
    public Transform Portal2;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (!animator.GetBool("Dañando"))
            {
                speed = -speed;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {

            if (Portal1 != null && Portal1_5 != null && Portal2 != null && Portal2_5 != null)
            {

                if (other.transform == Portal1)
                {
                    transform.position = Portal1_5.position;
                    ChangeDirection();
                }

                else if (other.transform == Portal2)
                {
                    transform.position = Portal2_5.position;
                    ChangeDirection();
                }
            }
            else
            {
                Debug.LogError("Una o más posiciones de tuberías no están asignadas en el inspector.");
            }
        }
    }

    private void ChangeDirection()
    {
        if (!animator.GetBool("Dañando"))
        {
            speed = -speed;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }


}
