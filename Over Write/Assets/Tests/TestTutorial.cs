using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class TestTutorial
{
    private GameObject tutorial;
    private GameObject jugador;

    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.LoadScene("Tutorial");
        tutorial = GameObject.Find("Tutorial");
        jugador = GameObject.Find("Jugador");
    }

    [UnityTest]
    public IEnumerator Tutorial1SeMuestra()
    {
        tutorial = GameObject.Find("Tutorial");
        MostrarTutorial mtTutorial = tutorial.GetComponent<MostrarTutorial>();

        yield return new WaitForSeconds(2f); // Esperar 1 segundo
        RawImage primerTutorial = mtTutorial.tutorial_1;
        Assert.IsTrue(primerTutorial.color.a == 1f);
    }

    [UnityTest]
    public IEnumerator Tutorial2SeMuestra()
    {
        tutorial = GameObject.Find("Tutorial");
        jugador = GameObject.Find("Jugador");
        MostrarTutorial mtTutorial = tutorial.GetComponent<MostrarTutorial>();
        MovimientoJugador mjJugador = jugador.GetComponent<MovimientoJugador>();

        RawImage primerTutorial = mtTutorial.tutorial_1;
        RawImage segundoTutorial = mtTutorial.tutorial_2;
        mjJugador.tintaET.mejorPunt = 1;
        mjJugador.tintaET.posObj = -83f;
        yield return new WaitForSeconds(2f); // Esperar 1 segundo
        Assert.IsTrue(primerTutorial.color.a == 0 && segundoTutorial.color.a == 1);
    }

    [UnityTest]
    public IEnumerator Tutorial3SeMuestra()
    {
        tutorial = GameObject.Find("Tutorial");
        jugador = GameObject.Find("Jugador");
        MostrarTutorial mtTutorial = tutorial.GetComponent<MostrarTutorial>();
        MovimientoJugador mjJugador = jugador.GetComponent<MovimientoJugador>();

        RawImage tercerTutorial = mtTutorial.tutorial_3;
        RawImage segundoTutorial = mtTutorial.tutorial_2;
        mjJugador.tintaET.racha = 1;
        mjJugador.tintaET.posObj = -97f;
        yield return new WaitForSeconds(2f); // Esperar 1 segundo
    }

    [UnityTest]
    public IEnumerator Tutorial3SeOculta()
    {
        tutorial = GameObject.Find("Tutorial");
        jugador = GameObject.Find("Jugador");
        MostrarTutorial mtTutorial = tutorial.GetComponent<MostrarTutorial>();
        MovimientoJugador mjJugador = jugador.GetComponent<MovimientoJugador>();

        RawImage tercerTutorial = mtTutorial.tutorial_3;

        mjJugador.tintaET.racha = 1;
        mjJugador.tintaET.posObj = -97f;

        // Luego el tercero se desactiva
        mtTutorial.pressMovKey = true;

        yield return new WaitForSeconds(2f); // Esperar 1 segundo
        Assert.IsFalse(tercerTutorial.color.a == 0);
    }
}

