using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private SpriteRenderer render;
    public RectTransform tinta;
    public EscribirTexto tintaET;

    private bool choqueEnemigo;
    private Vector2 currentPosition;
    private float posObj;
    private Animator animator;

    private void Start()
    {
        posObj = 0;
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        posObj = tintaET.posObj;
        // Obtener la entrada del teclado solo si se presionan los botones direccionales
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.RightArrow)) {
            moveX = 1f;
            render.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1f;
            render.flipX = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
            moveY = 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            moveY = -1f;

        Vector2 movement = new Vector2(moveX, moveY);

        // Reducir la velocidad de movimiento hacia la izquierda
        // if (moveX != 0f)
            movement *= 0.35f;

        if (moveX != 0f)
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

        // if (posObj < currentPosition.x)
        {
            //currentPosition.x = currentPosition.x - 10f;
            float moveSpeed = 5f; // Define la velocidad de movimiento
            Vector2 targetPosition = new Vector2(posObj, currentPosition.y);
            currentPosition = Vector2.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            tinta.anchoredPosition = currentPosition;
        }

        tintaET.posObj = posObj;
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

        float pushForce = 2f;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Obtener el componente Rigidbody2D del personaje

            // Aplicar una fuerza o impulso hacia X+
            Vector2 pushVector = new Vector2(pushForce, 0f);
            rb.AddForce(pushVector, ForceMode2D.Impulse);

            currentPosition = tinta.anchoredPosition;
            tintaET.posObj = currentPosition.x - 18f;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            tintaET.posObj = currentPosition.x - 0.1f;
        }
    }
}
