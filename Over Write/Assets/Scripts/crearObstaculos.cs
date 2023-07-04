using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class crearObstaculos : MonoBehaviour
{
    public GameObject [] obstaculos = new GameObject[6];
    public fansENemigo fanEnemigo;

    private Transform jugador;
    private int cantCreado;
    // Start is called before the first frame update
    void Start()
    {
        cantCreado = 0;
        jugador = GetComponent<Transform>();

        crearObstaculo(obstaculos[0]);
        crearObstaculo(obstaculos[1]);

    }

    // Update is called once per frame
    void Update()
    {
        if((jugador.position.x + 100f) > (450f + 850f * cantCreado))
        {
            if (cantCreado < 17)
            {
                if (fanEnemigo != null)
                {
                    if (fanEnemigo.letraOculta)
                    {
                        crearObstaculo(fanEnemigo.selectorMensaje[fanEnemigo.cantFanatico - 1]);
                        string text1 = "", text2 = "";
                        Debug.Log(fanEnemigo.cantFanatico);
                        switch (fanEnemigo.cantFanatico)
                        {
                            case 1:
                                text1 = "lumine";
                                text2 = "lumen";
                                break;
                            case 2:
                                text1 = "veritas";
                                text2 = "veriditas";
                                break;
                            case 3:
                                text1 = "sacra";
                                text2 = "sacer";
                                break;
                            case 4:
                                text1 = "tenebris";
                                text2 = "tenebrae";
                                break;
                            case 5:
                                text1 = "invicta";
                                text2 = "invictus";
                                break;
                            case 6:
                                text1 = "revelatur";
                                text2 = "revelo";
                                break;
                        }
                        crearLetra(fanEnemigo.mensajes, text1, text2);
                        //crearLetra(fanEnemigo.mensajes[0 + rand], text1);
                        //crearLetra(fanEnemigo.mensajes[(1 + rand) % 2], text2);
                        fanEnemigo.letraOculta = false; /// -22.85858

                    }
                    else
                    {
                        int rand = Random.Range(0, 6);
                        crearObstaculo(obstaculos[rand]);
                    }
                }
                else
                {
                    int rand = Random.Range(0, 6);
                    crearObstaculo(obstaculos[rand]);
                }
            }
        }
    }

    void crearObstaculo(GameObject go)
    {
        Vector3 obsPosition = new Vector3(450f + 850f * cantCreado, 0f, 0f);
        GameObject cloneobs = Object.Instantiate(go, obsPosition, Quaternion.identity);
        cantCreado++;
    }
    void crearLetra(GameObject go, string text1, string text2)
    {
        TextMeshPro hijo1 = go.gameObject.GetComponentsInChildren<TextMeshPro>()[0];
        TextMeshPro hijo2 = go.gameObject.GetComponentsInChildren<TextMeshPro>()[1];

        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            hijo1.text = text1;
            hijo2.text = text2;
        }
        else
        {
            hijo1.text = text2;
            hijo2.text = text1;
        }
        // TextMeshPro tmp = go.GetComponent<TextMeshPro>();
        // tmp.text = textNew;
        Vector3 obsPosition = new Vector3(450f + 850f * cantCreado, 0f, 0f);
        GameObject cloneobs = Object.Instantiate(go, obsPosition, Quaternion.identity);
    }
}
