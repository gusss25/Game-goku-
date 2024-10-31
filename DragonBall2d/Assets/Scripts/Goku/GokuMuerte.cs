using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuMuerte : MonoBehaviour
{
    Vector2 respawnPoint = new Vector2(-1.02f, 3.05f);
    public AudioSource Mondongo;
    public Animator animator;
    public BoxCollider2D GokuCollider;
    public BoxCollider2D Cabello;
    public BoxCollider2D Verification;
    public BoxCollider2D Esferas;
    public GameObject Spawn;
    public GameObject FatherVidas;
    public GameObject GameOver;
    GokuMove Moving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GokuCollider = GetComponent<BoxCollider2D>();
        Moving = GetComponent<GokuMove>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Animator EnemyAnimator = collision.gameObject.GetComponent<Animator>();


            if (EnemyAnimator != null && !EnemyAnimator.GetBool("Dañando"))
            {
                StartCoroutine(AnimMuerte());
            }
        }
    }

    IEnumerator AnimMuerte()
    {
        GokuCollider.enabled = false;
        Cabello.enabled = false;
        Esferas.enabled = false;
        Verification.enabled = false;
        Moving.enabled = false;
        Mondongo.Play();
        animator.SetBool("Caida", true);
        yield return new WaitForSeconds(1.250f);
        if (FatherVidas.transform.childCount > 0)
        {
            Destroy(FatherVidas.transform.GetChild(0).gameObject);
        }
        else
        {
            GameOver.SetActive(true);
            Destroy(gameObject);
        }
        Spawn.SetActive(true);
        transform.position = respawnPoint;
        animator.SetBool("Caida", false);
        GokuCollider.enabled = true;
        Cabello.enabled = true;
        Esferas.enabled = true;
        Verification.enabled = true;
        Moving.enabled = true;
    }
}
