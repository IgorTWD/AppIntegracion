using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MovimientoAsteroide : MonoBehaviour
{
    // Declaramos la variable de elocidad de movimiento publica para editarla en el viwport
    public float velocidad = 5.0f;

    public GameObject particulasExplosion;
    public GameObject particulasPolvoExplosion;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SC_MusicController musicController = FindObjectOfType<SC_MusicController>();
        SC_Vida scVida = FindObjectOfType<SC_Vida>();
        Debug.Log("Vida cada ves que colisiona: " + scVida.vida);

        if (collision.gameObject.tag == "Player")
        {
            // Obtén el punto de contacto
            Vector2 puntoDeContacto = collision.contacts[0].point;

            // Instancia el sistema de partículas en el punto de contacto
            Instantiate(particulasPolvoExplosion, puntoDeContacto, Quaternion.identity);

            Debug.Log("Vida actual colision PLAYER: " + scVida.vida);
           
        }
        else if (collision.gameObject.tag == "Muerte")
        {
            // Obtén el punto de contacto
            Vector2 puntoDeContacto = collision.contacts[0].point;

            // Instancia el sistema de partículas en el punto de contacto
            Instantiate(particulasExplosion, puntoDeContacto, Quaternion.identity);
            musicController.PlayDamangeAsteroide();

            Destroy(gameObject); // Destruye el asteroide
            Debug.Log("Vida cada ves que colisiona con objeto muerte: " + scVida.vida);
        }
        if (scVida.vida == 0)
        {
            Destroy(gameObject);
            Debug.Log("coñoooo");
        }
    }
}

