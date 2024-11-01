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
            if (animator.GetBool("Da�ando"))
            {
                StopCoroutine(AnimSaiDa�o());
                animator.SetBool("Da�ando", false);
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
                    animator.SetBool("Da�ando", false);
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
                StartCoroutine(AnimSaiDa�o());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetBool("Da�ando") && collision.gameObject.CompareTag("Player"))
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

    IEnumerator AnimSaiDa�o()
    {

        animator.SetBool("Walk", false);
        animator.SetBool("Iracundo", false);
        animator.SetBool("Da�ando", true);
        yield return new WaitForSeconds(6f);
        animator.SetBool("Iracundo", true);
        correr = true;
        animator.SetBool("Da�ando", false);
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
        if (!animator.GetBool("Da�ando"))
        {
            MoveSai.speed = 0f;
            StartCoroutine(AnimSaiDa�o());
        }
        else
        {
            StopCoroutine(AnimSaiDa�o());
            animator.SetBool("Da�ando", false);
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
