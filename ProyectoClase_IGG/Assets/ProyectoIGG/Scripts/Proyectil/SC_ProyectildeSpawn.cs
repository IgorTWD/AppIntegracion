
using UnityEngine;

public class ProyectilSpawn : MonoBehaviour
{
    // Declaracion de variables publicas para poderlas editar en el viewport directamente.
    public GameObject proyectilPrefab;
    public float spawnRate = 5.0f; // Intervalo en segundos entre spawns

    private void Start()
    {
        // Invoca repetidamente el metodo Spawn en el intervalo indicado.
        InvokeRepeating("SpawnAsteroid", spawnRate, spawnRate);
    }

    // Metodo spawnea asteroide en la posición del spawner.
    private void SpawnAsteroid()
    {
        // Usa la posición del objeto actual (Spawner) para el spawn
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
        Instantiate(proyectilPrefab, spawnPosition, spawnRotation);
    }
}
