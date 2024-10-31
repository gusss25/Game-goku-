using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDeEvento : MonoBehaviour
{
    public AudioSource Musica;
    public GameObject Sai1;
    public GameObject Sai2;
    public GameObject Sai3;
    public GameObject FatherSai;
    public GameObject Win;
    public GameObject Goku;
    public GameObject Score;
    void Awake()
    {
        Musica.Play();
        StartCoroutine(ActivarSai());
    }
    IEnumerator ActivarSai()
    {
        yield return new WaitForSeconds(1f);
        Sai1.SetActive(true);
        yield return new WaitForSeconds(4f);
        Sai2.SetActive(true);
        yield return new WaitForSeconds(4f);
        Sai3.SetActive(true);
    }

}
