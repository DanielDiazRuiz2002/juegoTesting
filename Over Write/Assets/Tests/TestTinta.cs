using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class TestTinta
{
    private GameObject tinta;
    private GameObject jugador;

    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.LoadScene("Nivel_1");

        tinta = GameObject.Find("Botella");
        jugador = GameObject.Find("Jugador");
    }

    [UnityTest]
    public IEnumerator TintaSeCreaCorrectamente()
    {
        tinta = GameObject.Find("Botella");
        jugador = GameObject.Find("Jugador");

        // Clonar el elemento tinta 20 pixeles delante del jugador
        Vector3 jugadorPosition = jugador.transform.position;
        Vector3 tintaPosition = jugadorPosition + new Vector3(20f, 0f, 0f);
        GameObject cloneTinta = Object.Instantiate(tinta, tintaPosition, Quaternion.identity);

        // Verificar si la tinta se creó correctamente
        Assert.NotNull(cloneTinta);

        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator AumnentaBarraTinta()
    {
        jugador = GameObject.Find("Jugador");
        tinta = GameObject.Find("Botella");

        // Obtener la referencia al componente MovimientoJugador del jugador
        MovimientoJugador jugadorMovJug = jugador.GetComponent<MovimientoJugador>();
        Collider2D jugadorColl = jugador.GetComponent<Collider2D>();

        // Obtener la referencia al componente EscribirTexto de la tinta
        EscribirTexto tintaEscribirTexto = tinta.GetComponent<EscribirTexto>();
    
        Vector2 anchoredPosition = jugadorMovJug.tinta.anchoredPosition;
        anchoredPosition.x = -150f;
        jugadorMovJug.tinta.anchoredPosition = anchoredPosition;

        // Guardar el valor actual de la barra de tinta del jugador
        float barraTintaAntes = jugadorMovJug.tinta.anchoredPosition.x;

        // Simular la colisión del jugador con el collider de Barra Tinta
        jugador.transform.position = tinta.transform.position;

        yield return new WaitForSeconds(1f); // Esperar 1 segundo

        // Verificar si la barra de tinta aumentó
        Assert.Greater(jugadorMovJug.tinta.anchoredPosition.x, barraTintaAntes);

        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator TintaSeOculta()
    {
        tinta = GameObject.Find("Botella");
        jugador = GameObject.Find("Jugador");

        // Obtener el componente Renderer de la tinta
        Renderer tintaRenderer = tinta.GetComponent<Renderer>();

        // Guardar el estado de activación actual de la tinta
        bool isActiveBefore = tinta.activeSelf;

        jugador.transform.position = tinta.transform.position;

        yield return new WaitForSeconds(1f); // Esperar 1 segundo

        // Verificar si el estado de activación de la tinta cambió correctamente
        Assert.AreEqual(!isActiveBefore, tinta.activeSelf);

        // Use yield to skip a frame.
        yield return null;
    }
}
