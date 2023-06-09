using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la entrada del teclado solo si se presionan los botones direccionales
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
            moveX = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            moveX = -1f;

        if (Input.GetKey(KeyCode.UpArrow))
            moveY = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            moveY = -1f;

        // Calcular el vector de movimiento
        Vector2 movement = new Vector2(moveX * 2, moveY * 5).normalized * speed * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + movement;

        rb.MovePosition(newPosition);
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
    }
}
