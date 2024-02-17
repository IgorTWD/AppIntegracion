
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class SC_SpawnProyectil : MonoBehaviour
{
    // Declaracion de variables publicas para poderlas editar en el viewport directamente.
    public GameObject proyectilPrefab;
    public float spawnTiempo = 5.0f; // Intervalo en segundos entre spawns
    public bool right = true;

    private GameObject player;
    public float distanciaActivacion = 20f;
    //private bool estaActivo = false;

    private void Start()
    {
        
        // Invoca repetidamente el metodo Spawn en el intervalo indicado.
        InvokeRepeating("IntentarSpawnAsteroide", spawnTiempo, spawnTiempo);
    }

    private void IntentarSpawnAsteroide()
    {
        player = GameObject.FindWithTag("Player");
        // Solo intenta spawnear si el jugador está dentro de la distancia de activación
        if (Vector3.Distance(player.transform.position, transform.position) <= distanciaActivacion)
        {
            SpawnAsteroide();
        }
    }

    // Metodo spawnea asteroide en la posicion del spawner.
    private void SpawnAsteroide()
    {

        // Usa la posicion del objeto actual (Spawner) para el spawn
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation;
        if (right)
        {
            // Si 'right' es verdadero, rota el prefab para que mire hacia la derecha
            spawnRotation = Quaternion.Euler(0, -90, 0); // Ajusta estos valores segun sea necesario
        }
        else
        {
            // Si 'right' es falso, rota el prefab para que mire hacia la izquierda
            spawnRotation = Quaternion.Euler(0, 90, 0); // Ajusta estos valores segun sea necesario
        }


        // Instancia el proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, spawnPosition, spawnRotation);

        // Asigna la direccion de movimiento del proyectil instanciado basada en la variable 'right'
        SC_MovimientoProyectil proyectilMovement = proyectil.GetComponent<SC_MovimientoProyectil>();
        proyectilMovement.right = right;
        

    }
}
