using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestCritico
{
    private GameObject critico;
    private GameObject jugador;
    // A Test behaves as an ordinary method

    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.LoadScene("Nivel_3");
        critico = GameObject.Find("Critico");
        jugador = GameObject.Find("Jugador");
    }

    [UnityTest]
    public IEnumerator EnemigoMueve()
    {
        EditorSceneManager.LoadScene("Nivel_3");
        critico = GameObject.Find("Critico");
        jugador = GameObject.Find("Jugador");
        RectTransform criticoRT = critico.GetComponent<RectTransform>();
        MovimientoJugador mjJugador = jugador.GetComponent<MovimientoJugador>();

        Vector2 initialPos = criticoRT.position;

        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        critico = GameObject.Find("Critico");
        criticoRT = critico.GetComponent<RectTransform>();
        Vector2 finalPos = criticoRT.position;
        Assert.AreNotSame(finalPos.y, initialPos.y);
    }

    [UnityTest]
    public IEnumerator EnemigoAtaca1()
    {
        EditorSceneManager.LoadScene("Nivel_3");
        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        critico = GameObject.Find("Critico");
        jugador = GameObject.Find("Jugador");
        criticoEnemigo criticoEnem = critico.GetComponent<criticoEnemigo>();
        MovimientoJugador mjJugador = jugador.GetComponent<MovimientoJugador>();

        criticoEnem.attack = true;
        criticoEnem.atackElect = true;

        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        critico = GameObject.Find("Critico");
        criticoEnem = critico.GetComponent<criticoEnemigo>();
        Assert.IsTrue(criticoEnem.ataque.GetComponent<RawImage>().color.a > 0);
    }

    [UnityTest]
    public IEnumerator EnemigoAtaca2()
    {
        EditorSceneManager.LoadScene("Nivel_3");
        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        critico = GameObject.Find("Critico");
        jugador = GameObject.Find("Jugador");
        criticoEnemigo criticoEnem = critico.GetComponent<criticoEnemigo>();
        MovimientoJugador mjJugador = jugador.GetComponent<MovimientoJugador>();

        criticoEnem.attack = true;
        criticoEnem.atackElect = false;

        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        critico = GameObject.Find("Critico");
        criticoEnem = critico.GetComponent<criticoEnemigo>();
        Assert.IsTrue(criticoEnem.ayuda.GetComponent<RawImage>().color.a > 0);
    }
}
