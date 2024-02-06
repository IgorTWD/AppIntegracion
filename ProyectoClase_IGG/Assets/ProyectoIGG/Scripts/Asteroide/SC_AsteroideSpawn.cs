
using UnityEngine;

public class AsteroideSpawn : MonoBehaviour
{
    // Declaracion de variables publicas para poderlas editar en el viewport directamente.
    public GameObject asteroidePrefab;
    public float spawnRate = 5.0f; // Intervalo en segundos entre spawns
    public float minHeight = 7.0f; // Altura mínima de spawn
    public float maxHeight = 20.0f; // Altura máxima de spawn
    public float spawnXPosition = -15.0f; // Posición X para spawn (negativo para izquierda, positivo para derecha)

    private void Start()
    {
        // Invoca repetidamente el metodo Spawn en el intervalo indicado. 
        InvokeRepeating("SpawnAsteroid", spawnRate, spawnRate);
    }

    // Metodo spawnea asteroide en el rando 2D indicado.
    private void SpawnAsteroid()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        Vector2 spawnPosition = new Vector2(spawnXPosition, randomHeight);
        Instantiate(asteroidePrefab, spawnPosition, Quaternion.identity);
    }
}
