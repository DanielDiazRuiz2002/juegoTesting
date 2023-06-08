using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moverPapel : MonoBehaviour
{
    public Transform papel_1;
    public Transform papel_2;
    public Transform textos;
    public TMP_Text textoEscrito;
    private float posAct;

    // Start is called before the first frame update
    void Start()
    {
        posAct = 0f;
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
