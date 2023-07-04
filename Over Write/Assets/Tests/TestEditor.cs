using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestEditor
{
    private GameObject editor;
    private GameObject textos;

    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.LoadScene("Nivel_2");
        editor = GameObject.Find("editor 1");
        textos = GameObject.Find("Textos");
    }

    [UnityTest]
    public IEnumerator EnemigoVaDesapareciendo()
    {
        EditorSceneManager.LoadScene("Nivel_2");
        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        textos = GameObject.Find("Textos");
        RectTransform textosRT = textos.GetComponent<RectTransform>();

        textosRT.anchoredPosition = new Vector2(-812f, textosRT.anchoredPosition.y);

        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        editor = GameObject.Find("editor 1");
        RawImage editorImage = editor.GetComponent<RawImage>();
        float finalPos = editorImage.color.a;
        Assert.IsTrue(finalPos < 100f);
    }

    [UnityTest]
    public IEnumerator EnemigoDesaparece()
    {
        EditorSceneManager.LoadScene("Nivel_2");
        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        editor = GameObject.Find("editor 1");
        textos = GameObject.Find("Textos");
        RectTransform textosRT = textos.GetComponent<RectTransform>();

        textosRT.anchoredPosition = new Vector2(-812f, textosRT.anchoredPosition.y);

        RawImage editorImage = editor.GetComponent<RawImage>();

        editorImage.color = new Color(editorImage.color.r, editorImage.color.g, editorImage.color.b, 0.001f);

        yield return new WaitForSeconds(2f); // Esperar 1 segundo
        editor = GameObject.Find("editor 1");
        editorImage = editor.GetComponent<RawImage>();
        float finalPos = editorImage.color.a;
        Assert.IsTrue(finalPos == 0);
    }
}
