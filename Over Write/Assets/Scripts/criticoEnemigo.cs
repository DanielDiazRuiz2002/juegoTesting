using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class criticoEnemigo : MonoBehaviour
{
    public Transform jugador;
    public GameObject ayuda;
    public GameObject ataque;
    public GameObject consejo;
    public float speed = 1f;
    public float attackInterval = 1f;

    private Collider2D ataqueColl;
    private RectTransform ataqueRT;
    private RawImage ataqueRI;
    private RectTransform ayudaRT;
    private RawImage ayudaRI;
    private RectTransform rectTransform;
    private float startY = 250f;
    private float endY = -335f;
    private float timer = 0f;
    private float timerAttack = 0f;
    private float timerAttackCooldown = 0f;
    private bool goingToEnd = true;
    public bool attack = false;
    public bool atackElect = true;

    // Start is called before the first frame update
    void Start()
    {
        ataqueColl = ataque.GetComponent<BoxCollider2D>();
        ataqueRT = ataque.GetComponent<RectTransform>();
        ataqueRI = ataque.GetComponent<RawImage>();
        ayudaRT = ayuda.GetComponent<RectTransform>();
        ayudaRI = ayuda.GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
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
            ataqueRT.anchoredPosition = new Vector2(ataqueRT.anchoredPosition.x, newY);
            ayudaRT.anchoredPosition = new Vector2(ayudaRT.anchoredPosition.x, newY);
        }

        // Detener el movimiento y atacar cada cierto intervalo de tiempo
        if(!attack && timerAttack >= attackInterval)
        {
            System.Random random = new System.Random();
            attackInterval = (float)Math.Round(random.NextDouble() * (4.0 - 2.0) + 2.0, 1);
            timerAttack = 0f;
            attack = true;
            atackElect = random.Next(3) == 1;
        }

        if (attack)
        {
            Attack(atackElect);
        }
        else
        {
            float alpha = ataqueRI.color.a;
            alpha -= Time.deltaTime * 1f;
            // Limitamos el alpha a un máximo de 121
            alpha = Mathf.Clamp(alpha, 0f, 1f);
            // Asignamos el nuevo valor de alpha a la imagen
            ataqueRI.color = new Color(ataqueRI.color.r, ataqueRI.color.g, ataqueRI.color.b, alpha);
        }
        
        if (newY == startY || newY == endY)
        {
            timer = 0f;
            goingToEnd = !goingToEnd;
        }
    }
    void Attack(bool atackElect)
    {
        if (atackElect)
        {
            float alpha = ataqueRI.color.a;
            if (alpha < 0.3f)
            {
                alpha += Time.deltaTime * 0.3f;
                // Limitamos el alpha a un máximo de 121
                alpha = Mathf.Clamp(alpha, 0f, 1f);
                // Asignamos el nuevo valor de alpha a la imagen
                ataqueRI.color = new Color(ataqueRI.color.r, ataqueRI.color.g, ataqueRI.color.b, alpha);
            }
            else
            {
                ataqueRI.color = new Color(ataqueRI.color.r, ataqueRI.color.g, ataqueRI.color.b, 1);
                timerAttackCooldown += Time.deltaTime * speed;
                ataqueColl.isTrigger = false;
            }
            if(timerAttackCooldown > 0.4f)
            {
                ataqueColl.isTrigger = true;
                timerAttackCooldown = 0;
                ataqueRI.color = new Color(ataqueRI.color.r, ataqueRI.color.g, ataqueRI.color.b, 0);
                attack = false;
            }
        }
        else
        {
            float alpha = ayudaRI.color.a;
            if (alpha < 0.3f)
            {
                alpha += Time.deltaTime * 0.3f;
                // Limitamos el alpha a un máximo de 121
                alpha = Mathf.Clamp(alpha, 0f, 1f);
                // Asignamos el nuevo valor de alpha a la imagen
                ayudaRI.color = new Color(ayudaRI.color.r, ayudaRI.color.g, ayudaRI.color.b, alpha);
            }
            else
            {
                ayudaRI.color = new Color(ayudaRI.color.r, ayudaRI.color.g, ayudaRI.color.b, 1);
                timerAttackCooldown += Time.deltaTime * speed;
            }
            if (timerAttackCooldown > 0.2f)
            {
                timerAttackCooldown = 0;
                ayudaRI.color = new Color(ayudaRI.color.r, ayudaRI.color.g, ayudaRI.color.b, 0);
                Vector3 obsPosition = new Vector3(jugador.position.x + 600f, transform.position.y, 0f);
                GameObject cloneobs = UnityEngine.Object.Instantiate(consejo, obsPosition, Quaternion.identity);
                attack = false;
            }
        }
        // Código para realizar el ataque aquí
    }
}
