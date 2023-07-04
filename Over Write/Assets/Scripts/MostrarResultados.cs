using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MostrarResultados : MonoBehaviour
{
    public RectTransform rectTransformFin;
    public EscribirTexto tintaET;
    public MovimientoJugador jugador;

    public GameObject dificultad;
    public GameObject resultados;

    public TMP_Text result1;
    public TMP_Text result2;
    public TMP_Text nota;
    public TMP_Text continuar;
    public TMP_Text repetir;

    public Button botonContinuar;
    private bool showReault;
    // Start is called before the first frame update
    void Start()
    {
        showReault = false;
        /* tintaET.correctos;
        tintaET.errores;
        tintaET.mejorPunt;
        tintaET.presicion;
        tintaET.racha; */
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador.pierde || jugador.gana)
        {
            if (jugador.pierde)
            {
                botonContinuar.interactable = false;
            }
            if (!showReault)
            {
                result2.text =
                    tintaET.errores + "\n" +
                    tintaET.mejorPunt + "\n" +
                    tintaET.integridad + "%\n" +
                    tintaET.presicion + "%";
                showReault = true;
            }
            Color colorBase = result1.color;
            Color colorBase2 = continuar.color;
            float alpha = colorBase.a; // Obtenemos el valor actual del alpha
            if (alpha < 255f)
            {
                alpha += Time.deltaTime * 0.2f; // Incrementamos el alpha gradualmente

                // Limitamos el alpha a un máximo de 121
                alpha = Mathf.Clamp(alpha, 0f, 1f);

                // Asignamos el nuevo valor de alpha a la imagen
                result1.color = new Color(colorBase.r, colorBase.g, colorBase.b, alpha);
                result2.color = new Color(colorBase.r, colorBase.g, colorBase.b, alpha);
                nota.color = new Color(colorBase.r, colorBase.g, colorBase.b, alpha);
                continuar.color = new Color(colorBase2.r, colorBase2.g, colorBase2.b, alpha);
                repetir.color = new Color(colorBase2.r, colorBase2.g, colorBase2.b, alpha);
            }
        }
    }

    public void volverAIntentar()
    {
        int nivAct = GlobalVariables.level;
        switch (nivAct)
        {
            case 0:
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                SceneManager.LoadScene("Nivel_1");
                break;
            case 2:
                SceneManager.LoadScene("Nivel_2");
                break;
            case 3:
                SceneManager.LoadScene("Nivel_3");
                break;
            case 4:
                SceneManager.LoadScene("Nivel_4");
                break;
            case 5:
                SceneManager.LoadScene("Nivel_5");
                break;
        }
    }

    public void continuarJugando()
    {
        //StartCoroutine(Finalizar(3f));
        int nivAct = GlobalVariables.level;
        switch (nivAct)
        {
            case 0:
                dificultad.SetActive(true);
                dificultad.SetActive(true);
                break;
            case 1:
                GlobalVariables.level++;
                SceneManager.LoadScene("Nivel_2");
                break;
            case 2:
                GlobalVariables.level++;
                SceneManager.LoadScene("Nivel_3");
                break;
            case 3:
                GlobalVariables.level++;
                SceneManager.LoadScene("Nivel_4");
                break;
            case 4:
                GlobalVariables.level++;
                SceneManager.LoadScene("Nivel_5");
                break;
            case 5:
                GlobalVariables.level++;
                SceneManager.LoadScene("Final");
                break;
        }
    }

    IEnumerator Finalizar(float time)
    {
        float targetScaleX = 31.27571f;
        float targetScaleY = 18.20387f;
        Vector3 originalScale = rectTransformFin.localScale;
        Vector3 targetScale = new Vector3(targetScaleX, targetScaleY, originalScale.z);
        float currentTime = 0.0f;

        do
        {
            rectTransformFin.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        rectTransformFin.localScale = targetScale;
    }
}
