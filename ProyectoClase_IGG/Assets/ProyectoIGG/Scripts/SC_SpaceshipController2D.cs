using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using System;

public class SpaceshipController2D : MonoBehaviour
{
    // Declaramos las variables principales publicas para poderlas editar en el viewport.
    public float thrustForce = 5.0f; // Fuerza empuje impulso de la nave (W,S,Arriba,Abajo).
    public float thrustSideForce = 5.0f; // Fuerza empuje impulso lateral de la nave (Izquierda,Derecha).
    public float rotationSpeed = 5.0f; // Velocidad de giro de la nave(A,D).
    private Boolean win; // Para controlar la victoria
    private int indexScena = 1; // Para el cambiar de nivel modularmente.
    //Variable de las fisicas 2d para el movimiento 
    private Rigidbody2D rb2d;
    //Variable para reproducir los sonidos de los motores
    public AudioClip thrustSound; // Audio para cuando se activen los motores traseros.
    public AudioClip sideThrustSound; // Audio para cuando se activen los motores delanteros.
    //Variables para insertar audios de los motores
    private AudioSource audioEngineThrust; // Audio para cuando se activen los motores traseros.
    private AudioSource audioEngineSide; // Audio para cuando se activen los motores delanteros.
    //Variables para las particulas de los motores.
    public ParticleSystem thrustParticles1; // Particulas de motor trasero derecho
    public ParticleSystem thrustParticles2; // Particulas de motor trasero izquiero
    public ParticleSystem leftParticles; // Particulas de motor delantero derecho
    public ParticleSystem rightParticles; // Particulas de motor delantero izquierdo
    // Referencia al Animator del panel de fade(no funciono)
    public Animator fadeAnimator;
    
    [SerializeField] private GameObject explosionParticlesPrefab; // Variable privada pero es publica por SerializeField
    

    // Inicialización del componente Rigidbody2D y configuración de los componentes de audio.
    void Start()
    {


        rb2d = GetComponent<Rigidbody2D>();

        audioEngineThrust = gameObject.AddComponent<AudioSource>();
        audioEngineThrust.clip = thrustSound;
        audioEngineThrust.loop = true;

        
        audioEngineSide = gameObject.AddComponent<AudioSource>();
        audioEngineSide.clip = sideThrustSound;
        audioEngineSide.loop = true; 
    }

    void Update()
    {
        
        // Control del audio y del sistema de partículas flechas pulsadas.
        
        // Arriba
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
                audioEngineThrust.Play();
                thrustParticles1.Play();
                thrustParticles2.Play();
            
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            audioEngineThrust.Stop();
            thrustParticles1.Stop();
            thrustParticles2.Stop();
        }
        // Izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftParticles.Play();
            audioEngineSide.Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftParticles.Stop();
            audioEngineSide.Stop();
        }
        // Derecha
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightParticles.Play();
            audioEngineSide.Play();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightParticles.Stop();
            audioEngineSide.Stop();
        }

        // Abajo
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            leftParticles.Play();
            rightParticles.Play();
            audioEngineSide.Play();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            leftParticles.Stop();
            rightParticles.Stop();
            audioEngineSide.Stop();
        }
    }

    void FixedUpdate()
    {
        // Metodo para actualizar el impulso por las flechas pulsadas.
        Thrust();
        ThrustSideForce();
        // Metodo para actualizar la rotacion segun la A o D pulsada.
        Rotate();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) )
        {
            Vector2 forwardDirection = transform.up;
            rb2d.AddForce(forwardDirection * thrustForce);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) )
        {
            Vector2 backwardDirection = -transform.up;
            rb2d.AddForce(backwardDirection * thrustSideForce);
        }
       
    }

    void ThrustSideForce()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 rightDirection = transform.right;
            rb2d.AddForce(rightDirection * thrustSideForce);
        } 
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 leftDirection = -transform.right;
            rb2d.AddForce(leftDirection * thrustSideForce);
        }

    }

    void Rotate()
    {
        // Rotación con teclas A y D
        if (Input.GetKey(KeyCode.A))
        {
            // Rotar hacia un lado (por ejemplo, izquierda) con A
            rb2d.MoveRotation(rb2d.rotation + rotationSpeed * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Rotar hacia el otro lado (por ejemplo, derecha) con D
            rb2d.MoveRotation(rb2d.rotation - rotationSpeed * Time.fixedDeltaTime);
        }
        //float rotationInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        //rb2d.MoveRotation(rb2d.rotation - rotationInput);
    }

    // Colisiones para controlar el fin de las partidas.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Objeto clase musicController que contiene y maneja el audio del juego.
        MusicController musicController = FindObjectOfType<MusicController>();
        
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("GANASTE");
            musicController.PlayVictoryMusic();
            win = true;
            DetenerNave();
            StartCoroutine(RestartGameAfterDelay(4.0f)); // Delay de 9 segundos

        }
        if (other.gameObject.CompareTag("Muerte"))
        {
            Explota();
            Debug.Log("Explotaste");
            musicController.PlayExplosionSound();
            win = false;
            DetenerNave();
            StartCoroutine(RestartGameAfterDelay(4.0f));


        }
        else if (other.gameObject && !other.gameObject.CompareTag("Finish") && !other.gameObject.CompareTag("NON"))
        {
            Explota();
            Debug.Log("No te salgas!");
            musicController.PlayDefeatSound();
            win = false;
            DetenerNave();
            StartCoroutine(RestartGameAfterDelay(4.0f));

        }
    }

    private IEnumerator RestartGameAfterDelay(float delaySeconds)
    {
        // Espera durante el tiempo especificado
        yield return new WaitForSeconds(delaySeconds);

        // Cambia la partida
        if (win == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(win == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    


    void Explota()
    {
        // Instancia el sistema de partículas de la explosión en la posición de la nave
        GameObject explosionEffect = Instantiate(explosionParticlesPrefab, transform.position, Quaternion.identity);
        ParticleSystem particles = explosionEffect.GetComponent<ParticleSystem>();
        if (particles != null)
        {
            particles.Play();
        }
        Destroy(explosionEffect, particles.main.duration); // Asegura destruir el objeto de partículas tras la reproducción

        // Desactiva todos los hijos del prefab
        foreach (Transform child in transform)
        {
            //if (child.GetComponent<ParticleSystem>() == null) // Excluye el sistema de partículas
            //{
                child.gameObject.SetActive(false);
            //}
        }



        //// Reproduce el sistema de partículas de la explosión
        //explosionParticles.Play();

        //// Oculta la nave desactivando su componente de renderizado
        //// Para MeshRenderer (usado comúnmente en modelos 3D)
        //MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        //meshRenderer.enabled = false;

        // Oculta o desactiva la nave
        //GetComponent<SpriteRenderer>().enabled = false; // Asume que la nave usa SpriteRenderer
        // O si prefieres desactivar completamente el objeto de la nave (incluyendo todos sus componentes)
        //gameObject.SetActive(false);

        // Opcional: Destruir la nave después de un delay, si la explosión tiene una duración conocida
        // Destroy(gameObject, explosionParticles.main.duration);
    }

    public void DetenerNave()
    {
        // Detiene todo movimiento y rotación
        rb2d.velocity = Vector2.zero; //Para la nave
        rb2d.angularVelocity = 0f; // Quita velocidad
        rb2d.isKinematic = true; // Bloquea la fisica

    }

    void GanarPartida()
    {
        // Activa la animación de fade que no funciona
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeIn");
        }

    }
}



