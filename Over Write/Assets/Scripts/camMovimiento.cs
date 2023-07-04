using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovimiento : MonoBehaviour
{
    public MovimientoJugador mj;
    public Transform player;
    public float maxRangeX;
    public float maxRangeY;
    public float moveSpeed;

    private Vector3 offset;

    private void Start()
    {
        // Calcula el desplazamiento inicial entre la c�mara y el personaje
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if (player.transform.position.x >= 960 || mj.pierde || mj.gana)
        {
            // Obtiene la posici�n actualizada de la c�mara
            Vector3 targetPosition = player.position + offset;

            // Limita la posici�n de la c�mara dentro de los rangos m�ximos
            targetPosition.x = Mathf.Clamp(targetPosition.x, -maxRangeX, maxRangeX);

            targetPosition = player.position;
            targetPosition.y = 540f;
            targetPosition.z = -540f;

            if (mj.pierde || mj.gana)
            {
                targetPosition.x = player.position.x;
                targetPosition.y = player.position.y - 50f;
            }
            // Mueve suavemente la c�mara hacia la posici�n actualizada
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            float addSpeed = 1.75f;

            Vector3 targetPosition;
            targetPosition.x = 960f;
            targetPosition.y = 540f;
            targetPosition.z = -14f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime * addSpeed);
        }
    }
}
