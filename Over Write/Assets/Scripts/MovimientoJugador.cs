using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoJugador : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private SpriteRenderer render;
    public RectTransform tinta;
    public EscribirTexto tintaET;
    public GameObject resultados;
    public SpriteRenderer image; // Referencia a la imagen que deseas modificar
    public RawImage imageResulados; // Referencia a la imagen que deseas modificar
    public Camera cam;
    public GameObject canvas;
    public string frase;
    public bool puedeCaminar;
    public int nivel;
    public int dificultad;

    private bool choqueEnemigo;
    public bool pierde;
    public bool gana;
    private Vector2 currentPosition;
    private float posObj;
    private Animator animator;

    private void Start()
    {
        choqueEnemigo = false;
        pierde = false;
        gana = false;
        posObj = 0f;
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        puedeCaminar = true;
        if(nivel == 0)
        {
            puedeCaminar = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pierde && !gana)
        {
            posObj = tintaET.posObj;
            // Obtener la entrada del teclado solo si se presionan los botones direccionales
            float moveX = 0f;
            float moveY = 0f;
            if (puedeCaminar)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    moveX = 1f;
                    render.flipX = false;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    moveX = -1f;
                    render.flipX = true;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                    moveY = 1f;
                if (Input.GetKey(KeyCode.DownArrow))
                    moveY = -1f;
            }
            if (choqueEnemigo)
            {
                moveX = 0f;
            }

            Vector2 movement = new Vector2(moveX, moveY);

            // Reducir la velocidad de movimiento hacia la izquierda
            // if (moveX != 0f)
            movement *= 0.35f;


            if (moveX != 0f || moveY != 0f)
            {
                movement *= 0.85f;
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            movement *= speed * Time.fixedDeltaTime;

            Vector2 newPosition = rb.position + movement;

            rb.MovePosition(newPosition);

            tintaET.posObj = posObj;

            // if (posObj < currentPosition.x)
        }
        {
            //currentPosition.x = currentPosition.x - 10f;
            float moveSpeed = 5f; // Define la velocidad de movimiento
            Vector2 targetPosition = new Vector2(posObj, currentPosition.y);
            currentPosition = Vector2.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            tinta.anchoredPosition = currentPosition;

            tintaET.posObj = posObj;
        }
        if (pierde || gana)
        {
            if (!resultados.activeSelf)
            {
                resultados.SetActive(true);
                render.sortingOrder = 7;
                canvas.SetActive(false);
                if(pierde)
                    animator.SetBool("loose", true);
                if (pierde)
                    animator.SetBool("win", true);
            }

            float alpha = image.color.a; // Obtenemos el valor actual del alpha
            if (alpha < 121f)
            {
                alpha += Time.deltaTime * 0.1f; // Incrementamos el alpha gradualmente

                // Limitamos el alpha a un máximo de 121
                alpha = Mathf.Clamp(alpha, 0f, 0.65f);

                // Asignamos el nuevo valor de alpha a la imagen
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            }

            alpha = imageResulados.color.a;
            if (alpha < 255f)
            {
                alpha += Time.deltaTime * 0.1f; // Incrementamos el alpha gradualmente

                // Limitamos el alpha a un máximo de 121
                alpha = Mathf.Clamp(alpha, 0f, 1f);

                // Asignamos el nuevo valor de alpha a la imagen
                imageResulados.color = new Color(imageResulados.color.r, imageResulados.color.g, imageResulados.color.b, alpha);
            }

            float sizeCam = cam.orthographicSize; // Obtenemos el valor actual del alpha
            if (sizeCam > 150f){

                sizeCam -= Time.deltaTime * 60f; // Incrementamos el alpha gradualmente

                // Limitamos el alpha a un máximo de 121
                sizeCam = Mathf.Clamp(sizeCam, 150f, 540.5891f);

                // Asignamos el nuevo valor de alpha a la imagen
                cam.orthographicSize = sizeCam;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Evitar que el personaje pase más allá de los muros establecidos
            Vector2 currentVelocity = rb.velocity;
            currentVelocity *= -1f; // Invertir la velocidad para retroceder

            rb.velocity = currentVelocity;
        }

        float pushForce = 3f;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Obtener el componente Rigidbody2D del personaje

            // Aplicar una fuerza o impulso hacia X+
            if (tintaET.posObj <= -96f)
            {
                pierde = true;
            }
            Vector2 pushVector = new Vector2(pushForce, 0f);
            rb.AddForce(pushVector, ForceMode2D.Impulse);

            currentPosition = tinta.anchoredPosition;
            tintaET.posObj = currentPosition.x - 18f;
            choqueEnemigo = true;

            Invoke("vuelveCaminar", 1f);
        }
        if (collision.gameObject.CompareTag("AtaqueFinal"))
        {
            if (tintaET.posObj <= -96f)
            {
                pierde = true;
            }

            currentPosition = tinta.anchoredPosition;
            tintaET.posObj = currentPosition.x - 8f;
        }
        if (collision.gameObject.CompareTag("AtaqueFuerte"))
        {
            if (tintaET.posObj <= -96f)
            {
                pierde = true;
            }

            currentPosition = tinta.anchoredPosition;
            tintaET.posObj = currentPosition.x - 25f;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            tintaET.posObj = tintaET.posObj - 1f; 
            if (tintaET.posObj <= -96f)
            {
                pierde = true;
            }
        }
        if (collision.gameObject.CompareTag("AtaqueFuerte"))
        {
            tintaET.posObj = tintaET.posObj - 1f;
            if (tintaET.posObj <= -96f)
            {
                pierde = true;
            }
        }
        if (collision.gameObject.CompareTag("AtaqueFinal"))
        {
            tintaET.posObj = tintaET.posObj - 1f;
            if (tintaET.posObj <= -96f)
            {
                pierde = true;
            }
        }
    }

    void vuelveCaminar()
    {
        choqueEnemigo = false;
    }
}
