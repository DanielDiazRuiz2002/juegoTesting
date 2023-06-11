using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscribirTexto : MonoBehaviour
{
    public TMP_Text TMPComparar;
    public TMP_Text textMeshPro;
    public Color similarColor;
    public Color noTintColor;
    public Color differentColor;
    public RectTransform tinta;

    private Vector2 currentPosition;
    public float posObj;
    // Start is called before the first frame update
    void Start()
    {
        posObj = 0f;
        textMeshPro.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // Si se presiona la tecla de retroceso
                {
                    if (textMeshPro.text.Length > 0)
                    {
                        textMeshPro.text = textMeshPro.text.Substring(0, textMeshPro.text.Length - 1);
                    }
                }
                else if (c != '\n' && c != '\r') // Si el carácter no es un salto de línea
                {
                    if(c == ' ')
                    {
                        textMeshPro.text += c;
                    }
                    else if (currentPosition.x - 0.5f > -96f)
                    {
                        textMeshPro.text += c;

                        currentPosition = tinta.anchoredPosition;
                        posObj -= 2.5f;
                    }
                }
            }

            string originalText = textMeshPro.text;
            string compareToText = TMPComparar.text;

            int minLength = Mathf.Min(originalText.Length, compareToText.Length);

            textMeshPro.color = similarColor;
            if (posObj <= -96f)
            {
                textMeshPro.color = noTintColor;
            }
            for (int i = 0; i < minLength; i++)
            {
                if (originalText[i] != compareToText[i])
                {
                    // Si los caracteres difieren, se considera diferente
                    textMeshPro.color = differentColor;
                    return;
                }
            }
        }
        /*if (posObj < currentPosition.x)
        {
            //currentPosition.x = currentPosition.x - 10f;
            float moveSpeed = 3f; // Define la velocidad de movimiento
            Vector2 targetPosition = new Vector2(posObj, currentPosition.y);
            currentPosition = Vector2.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            tinta.anchoredPosition = currentPosition;
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InkWell"))
        {
            Vector2 currentPosition = tinta.anchoredPosition;

            posObj = posObj + 39f;

            if(posObj > -3)
            {
                posObj = -3;
            }

            // tinta.anchoredPosition = currentPosition;

            collision.gameObject.SetActive(false);
            // El objeto ha hecho contacto con el collider del personaje
            // Realiza las acciones correspondientes aquí
        }
    }
}
