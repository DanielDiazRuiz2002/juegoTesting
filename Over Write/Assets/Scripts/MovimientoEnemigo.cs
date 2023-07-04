using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float speed = 5f; // Velocidad de avance del objeto
    public MovimientoJugador mj;

    public bool cambiarMovimiento;
    public GameObject Enemigo2;

    // Start is called before the first frame update
    void Start()
    {
        cambiarMovimiento = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mj.nivel == 5 && !cambiarMovimiento && transform.position.x >= 14000f)
        {
            cambiarMovimiento = true;
            Enemigo2.SetActive(true);
        }
        if (!mj.pierde && !cambiarMovimiento)
        {
            // Obt�n la posici�n actual del objeto
            Vector3 currentPosition = transform.position;

            // Calcula la nueva posici�n sumando el desplazamiento en el eje x
            float newX = currentPosition.x + speed * Time.deltaTime;
            Vector3 newPosition = new Vector3(newX, currentPosition.y, currentPosition.z);

            // Establece la nueva posici�n del objeto
            transform.position = newPosition;
        }
        if (cambiarMovimiento)
        {
            Vector3 currentPosition = transform.position;

            float newX = currentPosition.x - speed * Time.deltaTime;
            Vector3 newPosition = new Vector3(newX, currentPosition.y, currentPosition.z);

            transform.position = newPosition;
        }
        if (mj.gana)
        {
            // Obt�n la posici�n actual del objeto
            Vector3 currentPosition = transform.position;

            // Calcula la nueva posici�n sumando el desplazamiento en el eje x
            float newX = currentPosition.x - speed * 3 * Time.deltaTime;
            Vector3 newPosition = new Vector3(newX, currentPosition.y, currentPosition.z);

            // Establece la nueva posici�n del objeto
            transform.position = newPosition;
        }
    }
}
