using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaibaimanMuerte : MonoBehaviour
{
    Animator animator;
    private SaibaimanMove MoveSai;
    public SpriteRenderer spriteRenderer;
    bool correr = false;
    BoxCollider2D boxCollider2D;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        MoveSai = GetComponent<SaibaimanMove>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunchHead"))
        {
            if (animator.GetBool("Dañando"))
            {
                StopCoroutine(AnimSaiDaño());
                animator.SetBool("Dañando", false);
                if (correr)
                {
                    animator.SetBool("Iracundo", true);
                    if (spriteRenderer.flipX == true)
                    {
                        MoveSai.speed = -2;
                    }
                    else
                    {
                        MoveSai.speed = 2;
                    }
                }
                else
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Dañando", false);
                    if (spriteRenderer.flipX == true)
                    {
                        MoveSai.speed = -1;
                    }
                    else
                    {
                        MoveSai.speed = 1;
                    }
                }


            }
            else
            {
                MoveSai.speed = 0f;
                StartCoroutine(AnimSaiDaño());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetBool("Dañando") && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MuerteSai());
        }
    }

    IEnumerator MuerteSai()
    {
        animator.SetBool("Muriendo", true);
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("Muriendo", false);
        Destroy(gameObject);
    }

    IEnumerator AnimSaiDaño()
    {

        animator.SetBool("Walk", false);
        animator.SetBool("Iracundo", false);
        animator.SetBool("Dañando", true);
        yield return new WaitForSeconds(6f);
        animator.SetBool("Iracundo", true);
        correr = true;
        animator.SetBool("Dañando", false);
        if (!animator.GetBool("Walk"))
        {
            if (spriteRenderer.flipX == true)
            {
                MoveSai.speed = -2;
            }
            else
            {
                MoveSai.speed = 2;
            }
        }
    }

    public void MandarGolpe()
    {
        if (animator == null) return;
        if (!animator.GetBool("Dañando"))
        {
            MoveSai.speed = 0f;
            StartCoroutine(AnimSaiDaño());
        }
        else
        {
            StopCoroutine(AnimSaiDaño());
            animator.SetBool("Dañando", false);
            if (correr)
            {
                animator.SetBool("Iracundo", true);
                if (spriteRenderer.flipX == true)
                {
                    MoveSai.speed = -2;
                }
                else
                {
                    MoveSai.speed = 2;
                }
            }
            else
            {
                animator.SetBool("Walk", true);
                if (spriteRenderer.flipX == true)
                {
                    MoveSai.speed = -1;
                }
                else
                {
                    MoveSai.speed = 1;
                }
            }

        }
    }
}
