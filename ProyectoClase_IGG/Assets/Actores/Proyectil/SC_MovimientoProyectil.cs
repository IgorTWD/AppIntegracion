using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MovimientoProyectil : MonoBehaviour
{
    // Declaramos la variable de elocidad de movimiento publica para editarla en el viwport
    public float velocidad = 5.0f;
    public bool right = true;

    void Start()
    {
        // Destruye el asteroide despues de 15 segundos
        Destroy(gameObject, 15f);
    }

    void Update()
    {
        // Mueve particulas hacia adelante a lo largo del eje X
        if (right == false)
        {
            Quaternion spawnRotation = Quaternion.Euler(0, -90, 0);
            transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);
        } else if (right == true)
        {
            Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
            transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
        }
        
    }
}

