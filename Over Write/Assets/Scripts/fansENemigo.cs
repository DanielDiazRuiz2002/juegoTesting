using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fansENemigo : MonoBehaviour
{
    public GameObject mensajes;
    public GameObject[] selectorMensaje = new GameObject[6];
    public RectTransform textoRep;
    public bool letraOculta;
    public bool selectLetra;
    public int cantFanatico;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        letraOculta = false;
        selectLetra = false;
        cantFanatico = 0;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (cantFanatico) {
            case 0:
                if (textoRep.anchoredPosition.x < -410f) // lumine
                {
                    StartCoroutine(MoveRectTransform());
                    letraOculta = true;
                    cantFanatico++;
                }
                break;
            case 1:
                if (textoRep.anchoredPosition.x < -1457f) // veritas
                {
                    if (!selectLetra)
                    {
                        selectLetra = true;
                    }
                    StartCoroutine(MoveRectTransform());
                    letraOculta = true;
                    cantFanatico++;
                }
                break;
            case 2:
                if (textoRep.anchoredPosition.x < -2698f) // sacra
                {
                    if (!selectLetra)
                    {
                        selectLetra = true;
                    }
                    StartCoroutine(MoveRectTransform());
                    letraOculta = true;
                    cantFanatico++;
                }
                break;
            case 3:
                if (textoRep.anchoredPosition.x < -3246f) // tenebris
                {
                    if (!selectLetra)
                    {
                        selectLetra = true;
                    }
                    StartCoroutine(MoveRectTransform());
                    letraOculta = true;
                    cantFanatico++;
                }
                break;
            case 4:
                if (textoRep.anchoredPosition.x < -4300f) // invicta
                {
                    if (!selectLetra)
                    {
                        selectLetra = true;
                    }
                    StartCoroutine(MoveRectTransform());
                    letraOculta = true;
                    cantFanatico++;
                }
                break;
            case 5:
                if (textoRep.anchoredPosition.x < -5373f) // revelatur
                {
                    if (!selectLetra)
                    {
                        selectLetra = true;
                    }
                    StartCoroutine(MoveRectTransform());
                    letraOculta = true;
                    cantFanatico++;
                }
                break;

        }
        // ------------------LLAMAR FANATICO---------------------------------
        // StartCoroutine(MoveRectTransform(myRectTransform, 4));
    }
    IEnumerator MoveRectTransform()
    {
        float duration = 2;
        float time = 0;
        Vector2 startPosition = new Vector2(-1015, rectTransform.anchoredPosition.y);
        Vector2 endPosition = new Vector2(1015, rectTransform.anchoredPosition.y);

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
    }
}
