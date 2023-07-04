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

    public int racha;
    public int mejorPunt;
    public float presicion;
    public int errores;
    public int correctos;
    public int integridad;
    private bool error;

    private bool detectError;
    private MovimientoJugador mj;
    private Vector2 currentPosition;
    public float posObj;
    // Start is called before the first frame update
    void Start()
    {
        racha = 0;
        mejorPunt = 0;
        presicion = 0f;
        errores = 0;
        correctos = 0;
        integridad = 100;
        error = false;

        posObj = 0f;
        mj = GetComponent<MovimientoJugador>();
        if (mj.nivel != 0)
        {
            textMeshPro.text = "";
        }
        else
        {
            posObj = -70f;
        }

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
                    detectError = false;
                    if (c == ' ')
                    {
                        textMeshPro.text += c;
                    }
                    else if (posObj > -96f)
                    {
                        if (error)
                        {
                            errores++;
                        }
                        textMeshPro.text += c;

                        currentPosition = tinta.anchoredPosition;
                        posObj -= 2.5f;
                        racha++;
                        if (racha > mejorPunt)
                        {
                            mejorPunt = racha;
                        }
                    }
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
                error = true;
                detectError = true;
                if (racha > mejorPunt)
                {
                    mejorPunt = racha;
                    racha = 0;
                }
                return;
            }
            else
            {
                error = false;
            }
        }
        if (detectError)
        {
            errores++;
            detectError = false;
        }
        if (originalText.Equals(compareToText))
        {
            mj.gana = true;

            /*
            NINGUNA PASIÓN COMO EL MIEDO, LE ARREBATA CON TAL EFICACIA A LA MENTE LA CAPACIDAD DE ACTUAR Y RAZONAR – EDMUND BURKE
            EL TIEMPO PARA RELAJARTE ES CUANDO NO TIENES TIEMPO PARA ELLO. – SYDNEY J. HARRIS
            SI TIENES UN PROBLEMA QUE NO TIENE SOLUCIÓN, ¿PARA QUÉ TE PREOCUPAS? Y SI TIENE SOLUCIÓN, ¿PARA QUÉ TE PREOCUPAS? – PROVERBIO CHINO
            LA PREOCUPACIÓN NO ELIMINA EL DOLOR DEL MAÑANA, SINO QUE ELIMINA LA FUERZA DEL HOY – CORRIE TEN BOOM
            LA MEJOR ARMA CONTRA EL ESTRÉS ES LA HABILIDAD PARA ELEGIR UN PENSAMIENTO SOBRE EL OTRO – WILLIAM JAMES
            OCULTAR O REPRIMIR LA ANSIEDAD PRODUCE, DE HECHO, MÁS ANSIEDAD – SCOTT STOSSEL.
            Un escritor no escoge sus temas, son los temas quienes le escogen - Mario Vargas Llosa
             */
        }

        if (mj.pierde || mj.gana)
        {
            correctos = originalText.Length - errores;
            presicion = (correctos * 100) / compareToText.Length;
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
        if (collision.gameObject.CompareTag("Critica"))
        {
            string originalText = textMeshPro.text;
            if (originalText.Length > 7)
            {
                string newText = originalText.Substring(0, originalText.Length - 7);
                textMeshPro.text = newText;
            }
            else
            {
                textMeshPro.text = "";
            }
            integridad -= 10;
            if (integridad < 0)
            {
                integridad = 0;
            }
        }
        if (collision.gameObject.CompareTag("Consejo"))
        {
            posObj -= (posObj) / 2;

            if (posObj > 0)
            {
                posObj = 0;
            }
            collision.gameObject.SetActive(false);
            integridad -= 5;
            if (integridad < 0)
            {
                integridad = 0;
            }
        }
        if (collision.gameObject.CompareTag("TextoRobado"))
        {
            string text = collision.gameObject.GetComponent<TextMeshPro>().text;
            if(text == "lumine" || text == "veritas" || text == "sacra" || text == "tenebris" || text == "invicta" || text == "revelatur")
            {
                posObj += 10;
            }
            else
            {
                integridad -= 16; // 10
            }
            collision.gameObject.SetActive(false);
            GameObject padre = collision.transform.parent.gameObject;
            padre.gameObject.SetActive(false);
        }
    }
}
