using System.Collections;
using UnityEngine;


public class SC_ActivadorDeColliderYParticulas : MonoBehaviour
{
    [SerializeField] private BoxCollider2D colliderEspecifico;
    [SerializeField] private ParticleSystem sistemaDeParticulas;
    [SerializeField] private ParticleSystem sistemaDeParticulas2;
    public float tiempoApagado;
    public float tiempoEncendido;
    public float retardoActivacionCollider; 

    private Vector2 posicionOriginal; // La original
    public Vector2 posicionInicial;  // Desde donde se mueve, hasta la original

    private void Start()
    {
        // Almacena la posicion original del collider
        posicionOriginal = colliderEspecifico.offset;

        StartCoroutine(AlternarEstado());
    }

    private IEnumerator AlternarEstado()
    {
        while (true)
        {
            // Activa las particulas
            sistemaDeParticulas.Play();
            if (sistemaDeParticulas2 != null)
            {
                sistemaDeParticulas2.Play();
            }

            //yield return new WaitForSeconds(retardoActivacionCollider);

            // Mueve el collider progresivamente a su posicion original
            StartCoroutine(MoverColliderPosicionOriginal());

            // Espera un tiempo para el estado "encendido"
            yield return new WaitForSeconds(tiempoEncendido);

            // Desactiva el collider y detiene las particulas
            colliderEspecifico.enabled = false;
            sistemaDeParticulas.Stop();
            if (sistemaDeParticulas2 != null)
            {
                sistemaDeParticulas2.Stop();
            }

            // Espera un tiempo para el estado "apagado"
            yield return new WaitForSeconds(tiempoApagado);
        }
    }

    private IEnumerator MoverColliderPosicionOriginal()
    {
        colliderEspecifico.enabled = true;
        float tiempo = 0;
        while (tiempo < retardoActivacionCollider)
        {
            // Interpola la posicion del collider
            colliderEspecifico.offset = Vector2.Lerp(posicionInicial, posicionOriginal, tiempo / retardoActivacionCollider);
            tiempo += Time.deltaTime;
            yield return null;
        }

        // Asegura que el collider este exactamente en su posicion original al finalizar
        colliderEspecifico.offset = posicionOriginal;
        colliderEspecifico.enabled = true;
    }
}

