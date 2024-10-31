using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SistemaDeEvento : MonoBehaviour
{
    public AudioSource Musica;
    public GameObject Sai1;
    public GameObject Sai2;
    public GameObject Sai3;
    public GameObject FatherSai;
    public GameObject Win;
    public GameObject Goku;
    public GameObject Puntos;
    private TextMeshProUGUI TextoPuntos;
    private int puntos = 0;
    private int previousChildCount;

    private void Start()
    {
        TextoPuntos = Puntos.GetComponent<TextMeshProUGUI>();
        previousChildCount = FatherSai.transform.childCount;
    }

    void Awake()
    {
        Musica.Play();
        StartCoroutine(ActivarSai());
    }

    private void Update()
    {
        int currentChildCount = FatherSai.transform.childCount;

        if (currentChildCount < previousChildCount)
        {
            IncrementarPuntos();
            previousChildCount = currentChildCount;
        }

        if (currentChildCount == 0)
        {
            Win.SetActive(true);
        }
    }

    void PuntosActualizar()
    {
        TextoPuntos.text = "Puntos: " + puntos;
    }

    public void IncrementarPuntos()
    {
        puntos += 1500;
        PuntosActualizar();
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
