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
        // Calcula el desplazamiento inicial entre la cámara y el personaje
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if (player.transform.position.x >= 960 || mj.pierde || mj.gana)
        {
            // Obtiene la posición actualizada de la cámara
            Vector3 targetPosition = player.position + offset;

            // Limita la posición de la cámara dentro de los rangos máximos
            targetPosition.x = Mathf.Clamp(targetPosition.x, -maxRangeX, maxRangeX);

            targetPosition = player.position;
            targetPosition.y = 540f;
            targetPosition.z = -540f;

            if (mj.pierde || mj.gana)
            {
                targetPosition.x = player.position.x;
                targetPosition.y = player.position.y - 50f;
            }
            // Mueve suavemente la cámara hacia la posición actualizada
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
