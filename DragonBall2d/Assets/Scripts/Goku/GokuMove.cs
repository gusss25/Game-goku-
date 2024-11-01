
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuMove : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpForce = 12f;


    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private Camera cam;
    private float screenLeft;
    private float screenRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        CalculateScreenBounds();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleWrapAround();
        AnimatePlayer();
        if (rb.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }
    }


    void CalculateScreenBounds()
    {
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float spriteHalfWidth = sr.bounds.size.x / 2;
            screenLeft = cam.transform.position.x - camWidth / 2 - spriteHalfWidth;
            screenRight = cam.transform.position.x + camWidth / 2 + spriteHalfWidth;
        }
        else
        {

            screenLeft = cam.transform.position.x - camWidth / 2;
            screenRight = cam.transform.position.x + camWidth / 2;
        }
    }

    void HandleMovement()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }


        rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);


        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    void HandleJump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && VerificarSalto.esSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("Walking", false);
            animator.SetBool("Jumping", true);

        }



        if (!VerificarSalto.esSuelo)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
    }
    void HandleWrapAround()
    {
        Vector3 pos = transform.position;

        if (pos.x > screenRight)
        {
            pos.x = screenLeft;
            transform.position = pos;
        }
        else if (pos.x < screenLeft)
        {
            pos.x = screenRight;
            transform.position = pos;
        }


    }
    void AnimatePlayer()
    {

        if (VerificarSalto.esSuelo)
        {
            float horizontal = Mathf.Abs(rb.velocity.x);
            animator.SetBool("Walking", horizontal > 0.5f);
        }
    }

    void OnDrawGizmos()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            float left, right;
            if (sr != null)
            {
                float spriteHalfWidth = sr.bounds.size.x / 2;
                left = cam.transform.position.x - camWidth / 2 - spriteHalfWidth;
                right = cam.transform.position.x + camWidth / 2 + spriteHalfWidth;
            }
            else
            {
                left = cam.transform.position.x - camWidth / 2;
                right = cam.transform.position.x + camWidth / 2;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(left, transform.position.y - 5, 0), new Vector3(left, transform.position.y + 5, 0));
            Gizmos.DrawLine(new Vector3(right, transform.position.y - 5, 0), new Vector3(right, transform.position.y + 5, 0));
        }
    }
}

