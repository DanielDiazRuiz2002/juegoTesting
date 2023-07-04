using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemigoFinal : MonoBehaviour
{
    public Puntero puntero;
    public MoverPapel moverPapel;
    public TMP_Text texto;
    public TMP_Text textoEscrito;

    public Transform jugador;
    public GameObject ataqueVert;
    public GameObject ataqueLin;
    public float speed = 1f;
    public float attackInterval = 1f;

    private Collider2D ataqueColl;
    private RectTransform ayudaLinRT;
    private RawImage ayudaLinRI;
    private RectTransform rectTransform;
    private float startY = 320f;
    private float endY = -400f;
    private float timer = 0f;
    private float timerAttack = 0f;
    private float timerAttackCooldown = 0f;
    private float [] timerAttackCooldown2 = new float[9];
    private bool goingToEnd = true;
    private bool attack = false;
    private bool cambiandoTexto = false;
    private bool ocultandoTexto = true;
    private int attackElect = 0;
    private int countAct = 0;
    private int localDiff = 0;
    // Start is called before the first frame update
    void Start()
    {
        localDiff = 0;
        ataqueColl = ataqueLin.GetComponent<BoxCollider2D>();
        ayudaLinRT = ataqueLin.GetComponent<RectTransform>();
        ayudaLinRI = ataqueLin.GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (localDiff)
        { // 19624.01
            case 0:
                if (textoEscrito.text.Equals("Finis historiae appropinquat. Post multas tribulationes"))
                    localDiff++;
                break;
            case 1:
                if (textoEscrito.text.Equals("Finis historiae appropinquat. Post multas tribulationes et pericula, heros victoriam assequitur."))
                    localDiff++;
                break;
            case 2:
                if (jugador.GetComponent<MovimientoJugador>().gana)
                {
                    jugador.GetComponent<MovimientoJugador>().gana = false;
                    texto.text += " Cum laetitia et gratitudine, populus gaudet et pacem celebrat. Pax et prosperitas regnant in terris. Vita continuatur, historia semper in corde hominum manet.";
                    localDiff++;
                }
                break;
            case 3:
                if (textoEscrito.text.Equals("Finis historiae appropinquat. Post multas tribulationes et pericula, heros victoriam assequitur. Virtus et sapientia eum ducunt ad triumphum. Cum laetitia et gratitudine, populus gaudet et pacem celebrat."))
                    localDiff++;
                break;

        }
        float newY = 0;
        if (!attack)
        {
            timer += Time.deltaTime * speed;
            timerAttack += Time.deltaTime * speed;
            if (goingToEnd)
            {
                newY = Mathf.Lerp(startY, endY, timer);
            }
            else
            {
                newY = Mathf.Lerp(endY, startY, timer);
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
            ayudaLinRT.anchoredPosition = new Vector2(ayudaLinRT.anchoredPosition.x, newY);
        }

        // Detener el movimiento y atacar cada cierto intervalo de tiempo
        if (!attack && timerAttack >= attackInterval)
        {
            System.Random random = new System.Random();
            attackInterval = (float)Math.Round(random.NextDouble() * (4.0 - 2.0) + 2.0, 1);
            timerAttack = 0f;
            attackElect = random.Next(1 + localDiff);
            //attackElect = 0;
            if (attackElect == 1)
            {
                for (int i = 1; i < 10; i++)
                {
                    Vector3 positionActual = ataqueVert.GetComponentsInChildren<RectTransform>()[i].position;
                    ataqueVert.GetComponentsInChildren<RectTransform>()[i].position = new Vector3(random.Next(3000) + jugador.position.x - 1500, positionActual.y);
                }
            }
            attack = true;
        }

        if (attack)
        {
            Attack(attackElect);
            // Attack(3);
        }
        else
        {
            float alpha = ayudaLinRI.color.a;
            alpha -= Time.deltaTime * 1f;
            // Limitamos el alpha a un máximo de 121
            alpha = Mathf.Clamp(alpha, 0f, 1f);
            // Asignamos el nuevo valor de alpha a la imagen
            ayudaLinRI.color = new Color(ayudaLinRI.color.r, ayudaLinRI.color.g, ayudaLinRI.color.b, alpha);
        }

        if (newY == startY || newY == endY)
        {
            timer = 0f;
            goingToEnd = !goingToEnd;
        }
    }
    void Attack(int atackElect)
    {
        float alpha;
        switch (atackElect) {
            case 0:
                alpha = ayudaLinRI.color.a;
                if (alpha < 0.3f)
                {
                    alpha += Time.deltaTime * 0.1f;
                    // Limitamos el alpha a un máximo de 121
                    alpha = Mathf.Clamp(alpha, 0f, 1f);
                    // Asignamos el nuevo valor de alpha a la imagen
                    ayudaLinRI.color = new Color(ayudaLinRI.color.r, ayudaLinRI.color.g, ayudaLinRI.color.b, alpha);
                }
                else
                {
                    ayudaLinRI.color = new Color(ayudaLinRI.color.r, ayudaLinRI.color.g, ayudaLinRI.color.b, 1);
                    timerAttackCooldown += Time.deltaTime * speed;
                    ataqueColl.isTrigger = false;
                }
                if (timerAttackCooldown > 3f)
                {
                    ataqueColl.isTrigger = true;
                    timerAttackCooldown = 0;
                    ayudaLinRI.color = new Color(ayudaLinRI.color.r, ayudaLinRI.color.g, ayudaLinRI.color.b, 0);
                    attack = false;
                }
                break;
            case 1:
                int auxCont = 0;
                for(int i = 0; i < 9; i++)
                {
                    if (!(ataqueVert.GetComponentsInChildren<SpriteRenderer>()[i].color.a == 0 && countAct >= 15))
                    {
                        if (animAttack(ataqueVert.GetComponentsInChildren<SpriteRenderer>()[i], ataqueVert.GetComponentsInChildren<BoxCollider2D>()[i], ataqueVert.GetComponentsInChildren<SpriteRenderer>()[i].color.a, countAct, i))
                        {
                            countAct++;
                        }
                    }

                    if(ataqueVert.GetComponentsInChildren<SpriteRenderer>()[i].color.a == 0)
                    {
                        auxCont++;
                    }
                }
                if (countAct >= 15 && auxCont == 9)
                {
                    attack = false;
                    countAct = 0;

                }
                break;
            case 2:
                if(!cambiandoTexto)
                    StartCoroutine(CambiarPosicionTexto());
                break;
            case 3:
                alpha = texto.color.a;
                if (ocultandoTexto)
                {
                    if (alpha > 0f)
                    {
                        alpha -= Time.deltaTime * 1.2f;
                        // Limitamos el alpha a un máximo de 121
                        alpha = Mathf.Clamp(alpha, 0f, 0.38f);
                        // Asignamos el nuevo valor de alpha a la imagen
                        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, alpha);
                    }
                    else
                    {
                        timerAttackCooldown += Time.deltaTime * speed;
                    }
                    if(timerAttackCooldown > 5f)
                    {
                        ocultandoTexto = false;
                        timerAttackCooldown = 0; // Cum laetitia et gratitudine, populus gaudet et pacem celebrat. Pax et prosperitas regnant in terris. Vita continuatur, historia semper in corde hominum manet.
                    }
                }
                else
                {
                    if (alpha < 0.38f)
                    {
                        alpha += Time.deltaTime * 0.2f;
                        // Limitamos el alpha a un máximo de 121
                        alpha = Mathf.Clamp(alpha, 0f, 0.38f);
                        // Asignamos el nuevo valor de alpha a la imagen
                        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, alpha);
                    }
                    else
                    {
                        ocultandoTexto = true;
                        attack = false;
                    }
                }
                break;
        }
        // Código para realizar el ataque aquí
    }

    bool animAttack(SpriteRenderer sr, BoxCollider2D coll, float alpha, int count, int i)
    {
        alpha = sr.color.a;
        if (i == 0 || ( ataqueVert.GetComponentsInChildren<SpriteRenderer>()[i - 1].color.a > 0.09f && count < 15) || alpha > 0)
        {
            if (alpha < 0.3f)
            {
                alpha += Time.deltaTime * 0.2f;
                // Limitamos el alpha a un máximo de 121
                alpha = Mathf.Clamp(alpha, 0f, 0.9f);
                // Asignamos el nuevo valor de alpha a la imagen
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            }
            else
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.9f);
                timerAttackCooldown2[i] += Time.deltaTime * speed;
                coll.isTrigger = false;
            }

            if (timerAttackCooldown2[i] > 0.6f && alpha != 0)
            {
                coll.isTrigger = true;
                timerAttackCooldown2[i] = 0;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);

                System.Random random = new System.Random();
                Vector3 positionActual = ataqueVert.GetComponentsInChildren<RectTransform>()[i + 1].position;
                ataqueVert.GetComponentsInChildren<RectTransform>()[i + 1].position = new Vector3(random.Next(2000) + jugador.position.x - 500, positionActual.y);

                return true;
            }
        }
        return false;
     }
    IEnumerator CambiarPosicionTexto()
    {
        cambiandoTexto = true;
        puntero.cambioPos = -1;
        moverPapel.cambioPos = -1;
        yield return new WaitForSeconds(8);
        puntero.cambioPos = 1;
        moverPapel.cambioPos = 1;
        cambiandoTexto = false;
        attack = false;
    }
}

