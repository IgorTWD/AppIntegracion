using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadreProyectilMovement : MonoBehaviour
{
    // Declaramos la variable de elocidad de movimiento publica para editarla en el viwport
    public float speed = 5.0f;

    void Start()
    {
        // Destruye el asteroide después de 15 segundos
        Destroy(gameObject, 15f);
    }

    void Update()
    {
        // Mueve particulas hacia adelante a lo largo del eje X
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}

