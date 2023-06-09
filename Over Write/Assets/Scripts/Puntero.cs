using UnityEngine;
using TMPro;

public class Puntero : MonoBehaviour
{
    public Transform camara;
    public RectTransform textos;
    public TMP_Text textoEscrito; // Referencia al objeto de texto
    private float espacio_w;

    // Start is called before the first frame update
    void Start()
    {
        espacio_w = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float widthText = textoEscrito.preferredWidth;
        if (widthText <= 170f)
        {
            Vector2 targetPosition = new Vector2(espacio_w + camara.transform.position.x - 1015f + (25f + (widthText) * 1.013f) * 5f, transform.position.y);

            transform.position = targetPosition;
        }
        else
        {
            // + (widthText) * 5f  - 150f * 5
            Vector2 targetPosition = new Vector2(espacio_w + 35f + camara.transform.position.x - 1015f + (25f + (widthText) * 1.013f) * 5f + (170f - widthText) * 5f, transform.position.y);

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
                    espacio_w += 7.99f * 5;
                }
                else if (espacio_w > 0)
                {
                    espacio_w = 0f;
                    Debug.Log(widthText);
                }
            }
        }
    }
}
