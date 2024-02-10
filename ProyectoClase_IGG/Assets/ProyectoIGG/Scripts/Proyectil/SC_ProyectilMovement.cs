using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilMovement : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento
    public float rotationSpeed = 50.0f; // Velocidad de rotación

    
    void Update()
    {
        // Rotar el asteroide sobre el eje Y
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }

}

