
using System.Diagnostics;
using UnityEngine;

public class ProyectilSpawn : MonoBehaviour
{
    // Declaracion de variables publicas para poderlas editar en el viewport directamente.
    public GameObject proyectilPrefab;
    public float spawnRate = 5.0f; // Intervalo en segundos entre spawns
    public bool right = true;

    private void Start()
    {
        // Invoca repetidamente el metodo Spawn en el intervalo indicado.
        InvokeRepeating("SpawnProyectil", spawnRate, spawnRate);
    }

    // Metodo spawnea proyectil en la posición del spawner.
    private void SpawnProyectil()
    {
        // Usa la posición del objeto actual (Spawner) para el spawn
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation; // Para rotar el objeto
        if (right)
        {
            // Si 'right' es verdadero, rota el prefab para que mire hacia la derecha
            spawnRotation = Quaternion.Euler(0, -90, 0); // Ajusta estos valores según sea necesario
        }
        else
        {
            // Si 'right' es falso, rota el prefab para que mire hacia la izquierda
            spawnRotation = Quaternion.Euler(0, 90, 0); // Ajusta estos valores según sea necesario
        }


        // Instancia el proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, spawnPosition, spawnRotation);

        // Asigna la dirección de movimiento del proyectil instanciado basada en la variable 'right'
        PadreProyectilMovement proyectilMovement = proyectil.GetComponent<PadreProyectilMovement>();
        if (proyectilMovement != null) // Asegura que el proyectil tenga el componente PadreProyectilMovement
        {
            proyectilMovement.right = right;
        }
    }
}
