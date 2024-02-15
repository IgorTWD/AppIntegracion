using UnityEngine;


public class SC_SpaceshipController2D : MonoBehaviour
{
    // Declaramos las variables principales publicas para poderlas editar en el viewport.
    public float thrustForce = 5.0f; // Fuerza empuje impulso de la nave (W,S,Arriba,Abajo).
    public float thrustSideForce = 5.0f; // Fuerza empuje impulso lateral de la nave (Izquierda,Derecha).
    public float rotationSpeed = 5.0f; // Velocidad de giro de la nave(A,D).
   
    // Variable de las fisicas 2d para el movimiento de la nave
    private Rigidbody2D rb2d;
    // Variables para insertar audios de los motores de la nave
    private AudioSource audioEngineThrust; // Audio para cuando se activen los motores traseros.
    private AudioSource audioEngineSide; // Audio para cuando se activen los motores delanteros.

    // Variable para reproducir los sonidos de los motores
    public AudioClip thrustSound; // Audio para cuando se activen los motores traseros.
    public AudioClip sideThrustSound; // Audio para cuando se activen los motores delanteros.
    
    // Variables para ver las particulas de los motores.
    public ParticleSystem thrustParticles1; // Particulas de motor trasero derecho
    public ParticleSystem thrustParticles2; // Particulas de motor trasero izquiero
    public ParticleSystem leftParticles; // Particulas de motor delantero derecho
    public ParticleSystem rightParticles; // Particulas de motor delantero izquierdo
    
    // Referencia a otro script.
    private SC_Combustible SC_Combustible;


    // Inicialización del componente Rigidbody2D y configuración de los componentes de audio.
    void Start()
    {
        // Inicialización de componentes y scripts.
        rb2d = GetComponent<Rigidbody2D>();
        SC_Combustible = FindObjectOfType<SC_Combustible>(); // Encuentra el script de combustible en la nave.

        // Configuración de los componentes de audio para los motores.
        audioEngineThrust = gameObject.AddComponent<AudioSource>();
        audioEngineThrust.clip = thrustSound;
        audioEngineThrust.loop = true;

        audioEngineSide = gameObject.AddComponent<AudioSource>();
        audioEngineSide.clip = sideThrustSound;
        audioEngineSide.loop = true;
    }

    void Update()
    {
        
        // Control del audio y del sistema de partículas.
        // Arriba
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && SC_Combustible.combustibleActual > 0)
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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && SC_Combustible.combustibleActual > 0)
        {
            leftParticles.Play();
            audioEngineSide.Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) )
        {
            leftParticles.Stop();
            audioEngineSide.Stop();
        }

        // Derecha
        if (Input.GetKeyDown(KeyCode.RightArrow) && SC_Combustible.combustibleActual > 0)
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
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) && SC_Combustible.combustibleActual > 0)
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
        
        Thrust(); // Metodo para actualizar el impulso  segun la W o S o Arriba o Abajo pulsada.
        ThrustSideForce(); // Metodo para actualizar el  segun la A o D pulsada.
        Rotate(); // Metodo para actualizar la rotacion segun la flecha Izquierda o Derecha pulsada.
    }

    void Thrust()
    {
        // Verifica la entrada del usuario y el combustible antes de aplicar la fuerza.
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && SC_Combustible.combustibleActual > 0)
        {
            // Aplica fuerza de impulso hacia adelante si hay combustible suficiente.
            Vector2 forwardDirection = transform.up;
            rb2d.AddForce(forwardDirection * thrustForce * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && SC_Combustible.combustibleActual > 0)
        {
            // Aplica fuerza de impulso hacia atrás si hay combustible suficiente.
            Vector2 backwardDirection = -transform.up;
            rb2d.AddForce(backwardDirection * thrustSideForce * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }

    }

    void ThrustSideForce()
    {
        //  Verifica antes de aplicar impulso lateral si hay combustible.
        if (Input.GetKey(KeyCode.RightArrow) && SC_Combustible.combustibleActual > 0)
        {
            Vector2 rightDirection = transform.right;
            rb2d.AddForce(rightDirection * thrustSideForce * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && SC_Combustible.combustibleActual > 0)
        {
            Vector2 leftDirection = -transform.right;
            rb2d.AddForce(leftDirection * thrustSideForce * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }

    }

    void Rotate()
    {
        // Rotación con teclas A y D
        if (Input.GetKey(KeyCode.A) && SC_Combustible.combustibleActual > 0)
        {
            // Rotar hacia un lado con A
            rb2d.MoveRotation(rb2d.rotation + rotationSpeed * Time.fixedDeltaTime);
            rb2d.angularVelocity = 0f; // Quita velocidad giro
        }
        else if (Input.GetKey(KeyCode.D) && SC_Combustible.combustibleActual > 0)
        {
            // Rotar hacia el otro lado con D
            rb2d.MoveRotation(rb2d.rotation - rotationSpeed * Time.fixedDeltaTime);
            rb2d.angularVelocity = 0f; // Quita velocidad giro
        }
    }
}



