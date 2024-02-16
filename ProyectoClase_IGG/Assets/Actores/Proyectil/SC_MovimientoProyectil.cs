using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MovimientoProyectil : MonoBehaviour
{
    // Declaramos la variable de elocidad de movimiento publica para editarla en el viwport
    public float velocidad = 5.0f;
    public bool right = true;

    public GameObject particulasExplosion;
    public GameObject particulasPolvoExplosion;

    private int contadorColision = 0;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SC_MusicController musicController = FindObjectOfType<SC_MusicController>();

        if (collision.gameObject.tag == "Muerte")
        {
            // Obtén el punto de contacto
            Vector2 puntoDeContacto = collision.contacts[0].point;

            // Instancia el sistema de partículas en el punto de contacto
            Instantiate(particulasExplosion, puntoDeContacto, Quaternion.identity);
            musicController.PlayDamangeHielo();

            Destroy(gameObject); // Destruye el asteroide
            Debug.Log("Boom?");
        }
        else if (collision.gameObject.tag == "Player")
        {
            contadorColision++;

            // Obtén el punto de contacto
            Vector2 puntoDeContacto = collision.contacts[0].point;

            // Instancia el sistema de partículas en el punto de contacto
            Instantiate(particulasPolvoExplosion, puntoDeContacto, Quaternion.identity);

            Debug.Log("Boom player?");
            if (contadorColision >= 2)
            {
                Destroy(gameObject);
            }
        }
    }
}

