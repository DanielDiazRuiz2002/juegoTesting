using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscribirTexto : MonoBehaviour
{
    public TMP_Text textMeshPro;
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
                else
                {
                    textMeshPro.text += c;
                }
            }
        }
    }
}
