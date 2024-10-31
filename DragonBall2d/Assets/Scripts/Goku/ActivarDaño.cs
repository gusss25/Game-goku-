using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarDaño : MonoBehaviour
{
    public GameObject Colision2D;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(ActivarColision2D());
        }
        else
        {
            Colision2D.SetActive(false);
        }
    }
    IEnumerator ActivarColision2D()
    {
        Colision2D.SetActive(true);
        yield return new WaitForSeconds(0.06f);
        Colision2D.SetActive(false);
    }
}
