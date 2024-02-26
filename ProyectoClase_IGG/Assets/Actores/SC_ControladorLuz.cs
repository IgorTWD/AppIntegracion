using UnityEngine;
using System.Collections;

public class SC_ControladorLuz : MonoBehaviour
{
    private Light luzSpotlight; 
    public float duracionCambio = 3.0f; 

    // Rango de intensidad
    private float intensidadMinima = 1.0f;
    private float intensidadMaxima = 40.0f;

    void Start()
    {
        // Encuentra el componente Light
        luzSpotlight = GetComponentInChildren<Light>();

        if (luzSpotlight != null)
        {
            // Inicia la corutina
            StartCoroutine(CambiarIntensidadLuz());
        }
        else
        {
            Debug.LogError("No se encontro Light.");
        }
    }

    IEnumerator CambiarIntensidadLuz()
    {
        // Bucle infinito
        while (true)
        {
            // Gradualmente aumenta la intensidad de la luz
            for (float i = intensidadMinima; i < intensidadMaxima; i += Time.deltaTime * (intensidadMaxima - intensidadMinima) / duracionCambio)
            {
                luzSpotlight.intensity = i;
                yield return null; // Espera un frame antes de continuar
            }

            // Espera 1 segundo antes de empezar a disminuir la intensidad
            yield return new WaitForSeconds(1);

            // Gradualmente disminuye la intensidad de la luz
            for (float i = intensidadMaxima; i > intensidadMinima; i -= Time.deltaTime * (intensidadMaxima - intensidadMinima) / duracionCambio)
            {
                luzSpotlight.intensity = i;
                yield return null; // Espera un frame antes de continuar
            }

            // Espera 1 segundo antes de repetir el ciclo
            yield return new WaitForSeconds(1);
        }
    }
}
