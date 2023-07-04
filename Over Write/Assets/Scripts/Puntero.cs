using UnityEngine;
using TMPro;

public class Puntero : MonoBehaviour
{
    public Transform camara;
    public RectTransform textos;
    public TMP_Text textoEscrito; // Referencia al objeto de texto
    private float espacio_w;
    public int cambioPos;

    // Start is called before the first frame update
    void Start()
    {
        cambioPos = 1;
        espacio_w = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(cambioPos == -1)
        {
            textos.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            textos.rotation = Quaternion.Euler(0, 0, 0);
        }
        float widthText = textoEscrito.preferredWidth;
        if (widthText <= 170f)
        {
            Vector2 targetPosition = new Vector2(espacio_w + camara.transform.position.x + ( - 1020f + (25f + (widthText) * 1.012f) * 5f ) * cambioPos + (-23f + 23f * cambioPos), transform.position.y);

            transform.position = targetPosition;
        }
        else
        {
            // + (widthText) * 5f  - 150f * 5
            Vector2 targetPosition = new Vector2(espacio_w + 35f + camara.transform.position.x - 1020f + (25f + (widthText) * 1.013f) * 5f + (170f - (widthText)) * 5f , transform.position.y);

            transform.position = targetPosition;
        }
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (c == ' ')
                {
                    // Se agregó un espacio
                    // Realiza las acciones correspondientes aquí}
                    espacio_w += 7.99f * 5 * cambioPos;
                }
                else if (espacio_w * cambioPos > 0)
                {
                    if (c == '\b') // Si se presiona la tecla de retroceso
                    {
                        espacio_w -= 7.99f * 5 * cambioPos;
                    }
                    else
                    {
                        espacio_w = 0f;
                    }
                }
                if (c == '\b') // Si se presiona la tecla de retroceso
                {
                    int count = 0; // Variable para contar los espacios

                    if (textoEscrito.text.Length > 0)
                    {
                        char lastChar = textoEscrito.text[textoEscrito.text.Length - 1]; // Obtener el último carácter del texto

                        if (lastChar != ' ')
                        {
                            lastChar = textoEscrito.text[textoEscrito.text.Length - 2];
                            while (lastChar == ' ')
                            {
                                count++; // Si el último carácter es un espacio, inicializamos count en 1
                                lastChar = textoEscrito.text[textoEscrito.text.Length - 2 - count];
                            }
                        }
                    }

                    if (count > 0)
                    {
                        espacio_w = 7.99f * 5 * count * cambioPos + (-23f + 23f * cambioPos);
                    }
                }
            }
        }
    }
}
