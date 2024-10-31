using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esfera : MonoBehaviour
{
    public GameObject ActivarPorunga;
    public Animator MapeadoAnimator;
    public Animator EsferaAnimator;
    public GameObject Sai1;
    public GameObject Sai2;
    public GameObject Sai3;
    SaibaimanMuerte SaiMuerte_1;
    SaibaimanMuerte SaiMuerte_2;
    SaibaimanMuerte SaiMuerte_3;
    int i = 0;
    GokuMuerte manage;
    public static bool PowActivo = false;

    private void Awake()
    {
        SaiMuerte_1 = Sai1.GetComponent<SaibaimanMuerte>();
        SaiMuerte_2 = Sai2.GetComponent<SaibaimanMuerte>();
        SaiMuerte_3 = Sai3.GetComponent<SaibaimanMuerte>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TocandoEsfera"))
        {

            StartCoroutine(MoveEsfera());
            StartCoroutine(MoveMap());
            LlamarPorunga();
            SaiMuerte_1.MandarGolpe();
            SaiMuerte_2.MandarGolpe();
            SaiMuerte_3.MandarGolpe();


        }
    }

    void LlamarPorunga()
    {

        i += 1;
        EsferaAnimator.SetInteger("ObtenerEsfera", i);
        Debug.Log("Veces que toco la esfera" + i);

    }

    IEnumerator MoveEsfera()
    {
        float time = 0;
        float duration = 0.1f;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = (Vector2)transform.position + Vector2.up * 0.2f;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        time = 0;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(targetPosition, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;
    }
    IEnumerator MoveMap()
    {
        MapeadoAnimator.enabled = true;
        yield return new WaitForSeconds(0.75f);
        MapeadoAnimator.enabled = false;
        if (i > 2)
        {
            ActivarPorunga.SetActive(true);
            Destroy(gameObject);
        }
    }

}
