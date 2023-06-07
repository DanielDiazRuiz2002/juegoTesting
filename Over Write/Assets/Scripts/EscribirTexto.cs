using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscribirTexto : MonoBehaviour
{
    public TMP_Text TMPComparar;
    public TMP_Text textMeshPro;
    public Color similarColor;
    public Color differentColor;
    // Start is called before the first frame update
    void Start()
    {
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
                    textMeshPro.text += c;
                }
            }

            string originalText = textMeshPro.text;
            string compareToText = TMPComparar.text;

            int minLength = Mathf.Min(originalText.Length, compareToText.Length);

            textMeshPro.color = similarColor;
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
    }
}
