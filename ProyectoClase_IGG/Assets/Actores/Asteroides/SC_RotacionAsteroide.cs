using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RotacionAsteroide : MonoBehaviour
{
    public float velocidad = 5.0f; // Velocidad de movimiento
    public float velocidadRotacion = 50.0f; // Velocidad de rotacion


    void Update()
    {
        // Rotar el asteroide sobre el eje Y
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
    }

}
