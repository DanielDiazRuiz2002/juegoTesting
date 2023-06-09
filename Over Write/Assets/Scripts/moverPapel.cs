using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoverPapel : MonoBehaviour
{
    public Transform papeles;
    public Transform papel_1;
    public Transform papel_2;
    public Transform textos;
    public TMP_Text textoEscrito;
    private float posAct;
    private Transform camara;
    private float initialOffset;
    private float initialCamPos;
    private int cantSaltosCtrl;

    // Start is called before the first frame update
    void Start()
    {
        posAct = 0f;
        cantSaltosCtrl = 1;
        camara = Camera.main.transform;
        initialOffset = transform.position.x - camara.position.x;
        initialCamPos = papeles.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        float widthText_2 = textoEscrito.renderedWidth;
        float widthText = textoEscrito.preferredWidth;
        if (widthText >= 170f)
        {
            RectTransform textosRectTransform = textos.GetComponent<RectTransform>();
            Vector2 currentPosition = textosRectTransform.anchoredPosition;
            Vector2 targetPosition = new Vector2((170f - widthText) * 5f, currentPosition.y);
            float moveSpeed = 5f; // Define la velocidad de movimiento

            // Aplica una interpolación lineal para suavizar el movimiento
            currentPosition = Vector2.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

            textosRectTransform.anchoredPosition = currentPosition;


            RectTransform papelesRectTransform = papeles.GetComponent<RectTransform>();
            currentPosition = papelesRectTransform.anchoredPosition;
            targetPosition = new Vector2(camara.transform.position.x + initialOffset - 1015f + (170f - widthText) * 5f, currentPosition.y);

            // Aplica una interpolación lineal para suavizar el movimiento
            currentPosition = Vector2.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            papelesRectTransform.anchoredPosition = currentPosition;
        }
        else
        {
            RectTransform papelesRectTransform = papeles.GetComponent<RectTransform>();
            Vector2 currentPosition = papelesRectTransform.anchoredPosition;
            float moveSpeed = 5f; // Define la velocidad de movimiento
            Vector2 targetPosition = new Vector2(camara.transform.position.x + initialOffset - 1015f, currentPosition.y);
            currentPosition = Vector2.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            papelesRectTransform.anchoredPosition = currentPosition;
        }

        /*if(papeles.position.x > initialCamPos + 500f)
        {
            papel_1.position = papel_2.position;
            papel_2.position = new Vector3(papel_1.position.x - 2000f, papel_2.position.y, papel_2.position.z);
            initialCamPos = papeles.position.x;
            Debug.Log("XDDDDD"); 
        }*/
        if (widthText_2 > 180f + 480 * cantSaltosCtrl)
        {
            papel_1.position = papel_2.position;
            papel_2.position = new Vector3(papel_1.position.x + 2400f, papel_2.position.y, papel_2.position.z);
            cantSaltosCtrl++;
        }
    }
}
