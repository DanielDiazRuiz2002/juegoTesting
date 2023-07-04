using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class TestEnemigo
{
    private GameObject enemigo;
    private GameObject jugador;
    private MovimientoEnemigo movimientoEnemigo;

    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.LoadScene("Nivel_1");

        enemigo = GameObject.Find("Enemigo");
        jugador = GameObject.Find("Jugador");
    }

    [UnityTest]
    public IEnumerator seMueveHaciaLaDerecha()
    {
        EditorSceneManager.LoadScene("Nivel_1");

        enemigo = GameObject.Find("Enemigo");
        float initialXPosition = enemigo.transform.position.x;

        yield return new WaitForSeconds(5f); // Esperar 1 segundo

        enemigo = GameObject.Find("Enemigo");
        float finalXPosition = enemigo.transform.position.x;

        Assert.Greater(finalXPosition, initialXPosition);
    }

    [UnityTest]
    public IEnumerator enemigoQuitaTinta()
    {
        jugador = GameObject.Find("Jugador");
        MovimientoJugador jugadorMovJug = jugador.GetComponent<MovimientoJugador>(); 
        float initialTinta = jugadorMovJug.tinta.anchoredPosition.x;

        yield return new WaitForSeconds(15f); // Esperar 1 segundo

        float finalTinta = jugadorMovJug.tinta.anchoredPosition.x;

        Assert.Less(finalTinta, initialTinta);
    }

    [UnityTest]
    public IEnumerator enemigoMataAlJugador()
    {
        enemigo = GameObject.Find("Enemigo");
        jugador = GameObject.Find("Jugador");
        movimientoEnemigo = enemigo.GetComponent<MovimientoEnemigo>();

        MovimientoJugador jugadorMovJug = jugador.GetComponent<MovimientoJugador>();
        Vector2 anchoredPosition = jugadorMovJug.tinta.anchoredPosition;
        anchoredPosition.x = -150f;
        jugadorMovJug.tinta.anchoredPosition = anchoredPosition;

        yield return new WaitForSeconds(12f); // Esperar 1 segundo

        jugador = GameObject.Find("Jugador");
        Assert.True(jugador.GetComponent<MovimientoJugador>().pierde);
    }
}
