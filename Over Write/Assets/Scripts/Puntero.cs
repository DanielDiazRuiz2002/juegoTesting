using UnityEngine;
using TMPro;

public class Puntero : MonoBehaviour
{
    public Transform camara;
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
            Vector2 targetPosition = new Vector2( camara.transform.position.x - 1015f + (25f + (widthText) * 1.015f) * 5f, transform.position.y);

            transform.position = targetPosition;
        }
        else
        {
            // + (widthText) * 5f  - 150f * 5
            Vector2 targetPosition = new Vector2(35f + camara.transform.position.x - 1015f + (25f + (widthText) * 1.015f) * 5f + (170f - widthText) * 5f, transform.position.y);

            transform.position = targetPosition;
        }
    }
}
