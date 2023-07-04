using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMusica
{
    private AudioSource musica;
    private int nivel;
    private GameObject jugador;

    [SetUp] 
    public void SetUp()
    {
        EditorSceneManager.LoadScene("Nivel_1");
        jugador = GameObject.Find("Jugador");
        //musica = jugador.GetComponent<AudioSource>();
        //nivel = jugador.GetComponent<MovimientoJugador>().nivel;
    }

    // A Test behaves as an ordinary method
    [UnityTest]
    public IEnumerator musicaSuena()
    {
        yield return null;

        jugador = GameObject.Find("Jugador");
        musica = jugador.GetComponent<AudioSource>();
        // Verificar que la música está reproduciendo
        Assert.IsTrue(musica.isPlaying);
    }

    [UnityTest]
    public IEnumerator musicaSuenaEnBucle()
    {
        yield return null;

        // Activar la reproducción en bucle
        jugador = GameObject.Find("Jugador");
        musica = jugador.GetComponent<AudioSource>();
        musica.loop = true;

        // Verificar que la música está reproduciendo en bucle
        Assert.IsTrue(musica.loop);
    }

    [UnityTest]
    public IEnumerator musicaSuenaEn2D()
    {
        yield return null;

        // Establecer el modo de audio en 2D
        jugador = GameObject.Find("Jugador");
        musica = jugador.GetComponent<AudioSource>();
        musica.spatialBlend = 0f;

        // Verificar que el modo de audio es 2D
        Assert.AreEqual(0f, musica.spatialBlend);
    }
}
