using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class editorEnemigo : MonoBehaviour
{
    public EscribirTexto et;

    private RectTransform transf;
    private RawImage editor;
    // Start is called before the first frame update
    void Start()
    {
        transf = GetComponent<RectTransform>();
        editor = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 1700 && transform.position.x > 1000)
        {
            float alpha = editor.color.a;
            alpha -= Time.deltaTime * (0.05f - GlobalVariables.dificult * 0.01f);
            // Limitamos el alpha a un máximo de 121
            alpha = Mathf.Clamp(alpha, 0f, 1f);
            // Asignamos el nuevo valor de alpha a la imagen
            editor.color = new Color(editor.color.r, editor.color.g, editor.color.b, alpha);
        }
        else if(transform.position.x <= 1000)
        {
            if(editor.color.a > 0.015)
            {
                editor.color = new Color(editor.color.r, editor.color.g, editor.color.b, 1);
                et.integridad -= 20;
            }
            else{
                editor.color = new Color(editor.color.r, editor.color.g, editor.color.b, 0);
            }
        }
    }
}
