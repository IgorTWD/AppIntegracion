using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadreProyectilMovement : MonoBehaviour
{
    // Declaramos la variable de elocidad de movimiento publica para editarla en el viwport
    public float speed = 5.0f;
    public bool right = true;

    void Start()
    {
        // Destruye el proyectil después de 15 segundos
        Destroy(gameObject, 15f);
    }

    void Update()
    {
        // Mueve particulas hacia adelante a lo largo del eje X
        if (right == false)
        {
            Quaternion spawnRotation = Quaternion.Euler(0, -90, 0); // Quaternion = para rotar el objeto.
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        } else if (right == true)
        {
            Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        
    }
}

