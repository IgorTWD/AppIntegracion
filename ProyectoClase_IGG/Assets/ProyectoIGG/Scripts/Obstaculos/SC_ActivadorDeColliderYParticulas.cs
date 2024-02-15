using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ActivadorDeColliderYParticulas : MonoBehaviour
{
    [SerializeField] private Collider2D colliderEspecifico; // 
    [SerializeField] private ParticleSystem sistemaDeParticulas; // 
    [SerializeField] private ParticleSystem sistemaDeParticulas2; //

    private void Start()
    {
        // Inicia la coroutine para alternar el estado del collider y las partículas
        StartCoroutine(AlternarEstado());
    }

    private IEnumerator AlternarEstado()
    {
        // Bucle infinito
        while (true)
        {
            // Espera 5 segundos
            yield return new WaitForSeconds(5f);

            // Alternar el estado del collider
            colliderEspecifico.enabled = !colliderEspecifico.enabled;

            // Alternar el estado de las partículas (reproducir si están detenidas, detener si están reproduciéndose)
            if (sistemaDeParticulas.isPlaying)
            {
                sistemaDeParticulas.Stop();
                sistemaDeParticulas2.Stop();
            }
            else
            {
                sistemaDeParticulas.Play();
                sistemaDeParticulas2.Play();
            }
        }
    }
}
