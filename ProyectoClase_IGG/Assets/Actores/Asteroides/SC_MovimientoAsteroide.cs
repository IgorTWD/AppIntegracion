using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MovimientoAsteroide : MonoBehaviour
{
    // Declaramos la variable de elocidad de movimiento publica para editarla en el viwport
    public float velocidad = 5.0f;

    void Start()
    {
        // Destruye el asteroide despues de 15 segundos
        Destroy(gameObject, 15f);
    }

    void Update()
    {
        // Mueve particulas hacia adelante a lo largo del eje X
        transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
    }
}

