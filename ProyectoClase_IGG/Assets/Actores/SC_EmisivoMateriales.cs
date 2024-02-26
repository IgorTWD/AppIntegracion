using UnityEngine;
using System.Collections; 

public class SC_EmisivoMateriales : MonoBehaviour
{
    public Material materialObjetivo; 
    public Color colorEmisivo = Color.red; 
    public float duracionParpadeo = 2.0f; 

    private Color colorOriginal; 
    private bool estaParpadeando = false; 

    void Start()
    {
        // Guardamos el color emisivo base
        colorOriginal = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        Debug.Log("Color inicial: " + colorOriginal);
    }

    void Update()
    {
        if (!estaParpadeando)
        {
            StartCoroutine(Parpadear());
        }
    }

    // Coroutina con delays.
    IEnumerator Parpadear()
    {
        Debug.Log("Color inicial: " + colorOriginal + " Color Emisivo: " + colorEmisivo);
        estaParpadeando = true; // Indica que el parpadeo ha iniciado

        // Cambia el color emisivo
        materialObjetivo.SetColor("_EmissionColor", colorEmisivo);
        // Espera la mitad de la duracion del parpadeo antes de cambiar el color de nuevo
        yield return new WaitForSeconds(duracionParpadeo / 2);

        // Reestablece el color emisivo
        materialObjetivo.SetColor("_EmissionColor", colorOriginal);
        // Espera la otra mitad de la duracion del parpadeo
        yield return new WaitForSeconds(duracionParpadeo / 2);

        estaParpadeando = false; // El parpadeo ha terminado
    }
}
