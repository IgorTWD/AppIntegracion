
using UnityEngine;

public class AsteroideSpawn : MonoBehaviour
{
    // Declaracion de variables publicas para poderlas editar en el viewport directamente.
    public GameObject asteroidePrefab;
    public float spawnRate = 5.0f; // Intervalo en segundos entre spawns
    public float minHeight = 17.0f; // Altura mínima de spawn
    public float maxHeight = 33.0f; // Altura máxima de spawn
    public float spawnXPosition = -99.0f; // Posición X para spawn (negativo para izquierda, positivo para derecha)
    public float spawnZPosition = -99.0f; // Posición X para spawn (negativo para izquierda, positivo para derecha)

    private void Start()
    {
        // Invoca repetidamente el metodo Spawn en el intervalo indicado. 
        InvokeRepeating("SpawnAsteroid", spawnRate, spawnRate);
    }

    // Metodo spawnea asteroide en el rando 2D indicado.
    private void SpawnAsteroid()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnXPosition, randomHeight, spawnZPosition);
        Instantiate(asteroidePrefab, spawnPosition, Quaternion.identity);
    }
}
