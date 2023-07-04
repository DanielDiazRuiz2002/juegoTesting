using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class TestFanatico
{
    private GameObject fanatico;
    private GameObject texto;
    // A Test behaves as an ordinary method

    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.LoadScene("Nivel_3");
        fanatico = GameObject.Find("Critico");
        texto = GameObject.Find("textoEscrito");
    }

    [UnityTest]
    public IEnumerator EnemigoMueve()
    {
        EditorSceneManager.LoadScene("Nivel_3");
        fanatico = GameObject.Find("Critico");
        texto = GameObject.Find("textoEscrito");
        fansENemigo fanEnemigo = fanatico.GetComponent<fansENemigo>();
        RectTransform fanTrans = fanatico.GetComponent<RectTransform>();
        RectTransform textoTrans = texto.GetComponent<RectTransform>();
        float initialPos = fanTrans.anchoredPosition.x;
        textoTrans.anchoredPosition = new Vector2(-415f, textoTrans.anchoredPosition.y);

        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        fanatico = GameObject.Find("Critico");
        fanTrans = fanatico.GetComponent<RectTransform>();
        Assert.AreNotSame(initialPos, fanTrans.anchoredPosition.x);
    }
}
