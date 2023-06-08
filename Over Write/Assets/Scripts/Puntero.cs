using UnityEngine;
using TMPro;

public class Puntero : MonoBehaviour
{
    public RectTransform textos;
    public TMP_Text textoEscrito; // Referencia al objeto de texto

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float widthText = textoEscrito.preferredWidth;
        if (widthText <= 170f)
        {
            Vector2 targetPosition = new Vector2((25f + widthText) * 5f, transform.position.y);

            transform.position = targetPosition;
        }
        else
        {
            // + (widthText) * 5f  - 150f * 5
            Vector2 targetPosition = new Vector2(widthText , transform.position.y);

            transform.position = targetPosition;
        }
    }
}
