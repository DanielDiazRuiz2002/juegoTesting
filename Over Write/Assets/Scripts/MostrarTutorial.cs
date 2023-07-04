using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MostrarTutorial : MonoBehaviour
{
    public MovimientoJugador jugador;
    public GameObject mostrarNivel;
    public RawImage tutorial_1;
    public RawImage tutorial_2;
    public RawImage tutorial_3;
    public RawImage tutorial_3_1;

    public GameObject dificultad;

    public RawImage fondoSelectDificult;
    public RawImage facilImage;
    public RawImage medioImage;
    public RawImage dificilImage;
    public TMP_Text facilText;
    public TMP_Text medioText;
    public TMP_Text dificilText;

    public bool pressMovKey;
    public bool endTutorial;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.level = 0;
        pressMovKey = false;
        endTutorial = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!endTutorial)
        {
            mostrarAyuda(jugador.tintaET.mejorPunt == 0, tutorial_1);
            mostrarAyuda(jugador.tintaET.posObj <= -80f && jugador.tintaET.posObj > -96f, tutorial_2);
            if (jugador.tintaET.posObj <= -96f)
            {
                pressMovKey = true;
                jugador.puedeCaminar = true;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) && pressMovKey)
            {
                pressMovKey = false;
                endTutorial = true;
            }
        }
        mostrarAyuda(pressMovKey, tutorial_3);
        mostrarAyuda(pressMovKey, tutorial_3_1);
        if (dificultad.activeSelf)
        {
            mostrarDificultad(fondoSelectDificult, facilImage, medioImage, dificilImage, facilText, medioText, dificilText);
        }
    }

    void mostrarAyuda(bool aparece, RawImage tutorial)
    {
        if (aparece)
        {
            float alpha = tutorial.color.a;

            alpha += Time.deltaTime * 1f; // Incrementamos el alpha gradualmente

            // Limitamos el alpha a un máximo de 121
            alpha = Mathf.Clamp(alpha, 0f, 1f);

            // Asignamos el nuevo valor de alpha a la imagen
            tutorial.color = new Color(tutorial.color.r, tutorial.color.g, tutorial.color.b, alpha);
        }
        else
        {
            float alpha = tutorial.color.a;
            if (alpha != 0)
            {
                alpha -= Time.deltaTime * 0.8f; // Incrementamos el alpha gradualmente

                // Limitamos el alpha a un máximo de 121
                alpha = Mathf.Clamp(alpha, 0f, 1f);

                // Asignamos el nuevo valor de alpha a la imagen
                tutorial.color = new Color(tutorial.color.r, tutorial.color.g, tutorial.color.b, alpha);
            }
        }
    }

    void mostrarDificultad(RawImage fondo, RawImage img1, RawImage img2, RawImage img3, TMP_Text text1, TMP_Text text2, TMP_Text text3)
    {
        float alpha = fondo.color.a;
        if (alpha < 1f)
        {
            alpha += Time.deltaTime * 1f; // Incrementamos el alpha gradualmente

            // Limitamos el alpha a un máximo de 121
            alpha = Mathf.Clamp(alpha, 0f, 1f);

            // Asignamos el nuevo valor de alpha a la imagen
            fondo.color = new Color(fondo.color.r, fondo.color.g, fondo.color.b, alpha);
            Color alphaPuertas = new Color(img1.color.r, img1.color.g, img1.color.b, alpha);
            img1.color = alphaPuertas;
            img2.color = alphaPuertas;
            img3.color = alphaPuertas;
            text1.color = alphaPuertas;
            text2.color = alphaPuertas;
            text3.color = alphaPuertas;
        }
    }
    public void botonDificultad(int dif)
    {
        GlobalVariables.level++;
        GlobalVariables.dificult = dif;
        SceneManager.LoadScene("Nivel_1");
    }
}
