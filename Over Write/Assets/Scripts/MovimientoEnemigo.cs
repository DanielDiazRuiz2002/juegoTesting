using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float speed = 5f; // Velocidad de avance del objeto

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Obt�n la posici�n actual del objeto
        Vector3 currentPosition = transform.position;

        // Calcula la nueva posici�n sumando el desplazamiento en el eje x
        float newX = currentPosition.x + speed * Time.deltaTime;
        Vector3 newPosition = new Vector3(newX, currentPosition.y, currentPosition.z);

        // Establece la nueva posici�n del objeto
        transform.position = newPosition;
    }
}
