using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_Puntuacion : MonoBehaviour
{
    private SC_Vida scVida; // Referencia al script de vida en la nave
    private SC_Combustible scCombustible; 
    public TMP_Text textoPuntuacion;
    public int puntuacion;

    private float tiempoInicio;
    private float tiempoFinal;


    private void Start()
    {
        // Fija el tiempo inicial del crono.
        tiempoInicio = Time.time;
    }

    //void Update()
    //{
    //    tiempoFinal = Time.time;
    //    CalcularYMostrarPuntuacion();
    //}

    public void CalcularYMostrarPuntuacion()
    {
        //tiempoFinal = Time.time;
        MostrarPuntuacion(CalcularPuntuacion());
    }



    private int CalcularPuntuacion()
    {
        // Busca el GameObject de la nave por etiqueta
        GameObject nave = GameObject.FindGameObjectWithTag("Player");
        if (nave != null)
        {
            // Busca los componentes en el GameObject de la nave
            scVida = nave.GetComponent<SC_Vida>();
            scCombustible = nave.GetComponent<SC_Combustible>();
        }
        else
        {
            Debug.LogError("Nave no esta ");
        }


        tiempoFinal = Time.time;
        float tiempoTardado = tiempoFinal - tiempoInicio;
        float combustibleRestante = scCombustible.combustibleActual;
        int vidasRestantes = scVida.vida;
        //Debug.Log(combustibleRestante);

        //int puntosPorVida = 1000; 
        
        puntuacion = ((int)combustibleRestante  * 10) *(  vidasRestantes*10 /2) - (int)tiempoTardado *100 ; // El tiempo resta 100 por segundo.

        if (puntuacion < 0)
        {
            puntuacion = 0;
        }
        return puntuacion;
    }

    private void MostrarPuntuacion(int puntuacionFinal)
    {
        textoPuntuacion.text =  puntuacionFinal.ToString();
    }
}