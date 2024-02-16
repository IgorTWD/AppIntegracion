
using UnityEngine;

public class SC_SpawnAsteroide : MonoBehaviour
{
    // Declaracion de variables publicas para poderlas editar en el viewport directamente.
    public GameObject asteroidePrefab;
    public float spawnTiempo = 5.0f; // Intervalo en segundos entre spawns
    public float alturaMin = 17.0f; // Altura minima de spawn
    public float alturaMax = 33.0f; // Altura maxima de spawn
    public float spawnXPosition = -99.0f; // Posicion X para spawn (negativo para izquierda, positivo para derecha)
    public float spawnZPosition = -99.0f; // Posicion Z para spawn 

    private void Start()
    {
        // Invoca repetidamente el metodo Spawn en el intervalo indicado.
        InvokeRepeating("SpawnAsteroide", spawnTiempo, spawnTiempo);
    }

    // Metodo spawnea asteroide en el rando 2D indicado.
    private void SpawnAsteroide()
    {
        float randomHeight = Random.Range(alturaMin, alturaMax);
        Vector3 spawnPosition = new Vector3(spawnXPosition, randomHeight, spawnZPosition);
        Instantiate(asteroidePrefab, spawnPosition, Quaternion.identity);
    }
}
