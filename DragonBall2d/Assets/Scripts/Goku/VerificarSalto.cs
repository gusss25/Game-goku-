using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarSalto : MonoBehaviour
{
    public static bool esSuelo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            esSuelo = false;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            esSuelo = false;
        }
        if(true)
        {
            esSuelo = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        esSuelo = false;
    }

}