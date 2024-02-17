using UnityEngine;


public class SC_ControladorNave2D : MonoBehaviour
{
    // Declaramos las variables principales publicas para poderlas editar en el viewport.
    public float impulso = 5.0f; // Fuerza empuje impulso de la nave (W,S,Arriba,Abajo).
    public float impulsoLateral = 5.0f; // Fuerza empuje impulso lateral de la nave (Izquierda,Derecha).
    public float rotacionVelocidad = 5.0f; // Velocidad de giro de la nave(A,D).

    // Variable de las fisicas 2d para el movimiento de la nave
    private Rigidbody2D rb2d;
    // Variables para insertar audios de los motores de la nave
    private AudioSource audioImpulso; // Audio para cuando se activen los motores traseros.
    private AudioSource audioImpulsoLateral; // Audio para cuando se activen los motores delanteros.

    // Variable para reproducir los sonidos de los motores
    public AudioClip sonidoImpulso; // Audio para cuando se activen los motores traseros.
    public AudioClip sonidoImpulsoLateral; // Audio para cuando se activen los motores delanteros.

    // Variables para ver las particulas de los motores.
    public ParticleSystem particulasMotor_R; // Particulas de motor trasero derecho
    public ParticleSystem particulasMotor_L; // Particulas de motor trasero izquiero
    public ParticleSystem particulasMotorFrontal_R; // Particulas de motor delantero derecho
    public ParticleSystem particulasMotorFrontal_L; // Particulas de motor delantero izquierdo

    // Referencia a otro script.
    private SC_Combustible SC_Combustible;

    public bool botonArribaPulsado = false;
    public bool botonAbajoPulsado = false;
    public bool botonDerechaPulsado = false;
    public bool botonIzquierdaPulsado = false;
    public bool botonLaterialIzquierdo = false;
    public bool botonLaterialDerecho = false;

    // Inicializacion del componente Rigidbody2D y configuracion de los componentes de audio.
    void Start()
    {
        // Inicializacion de componentes y scripts.
        rb2d = GetComponent<Rigidbody2D>();
        SC_Combustible = FindObjectOfType<SC_Combustible>(); // Encuentra el script de combustible en la nave.

        // Configuracion de los componentes de audio para los motores.
        audioImpulso = gameObject.AddComponent<AudioSource>();
        audioImpulso.clip = sonidoImpulso;
        audioImpulso.loop = true;

        audioImpulsoLateral = gameObject.AddComponent<AudioSource>();
        audioImpulsoLateral.clip = sonidoImpulsoLateral;
        audioImpulsoLateral.loop = true;
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR

        // Control del audio y del sistema de particulas.
        // Arriba
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && SC_Combustible.combustibleActual > 0)
        {
            audioImpulso.Play();
            particulasMotor_R.Play();
            particulasMotor_L.Play();

        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            audioImpulso.Stop();
            particulasMotor_R.Stop();
            particulasMotor_L.Stop();
        }

        // Izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && SC_Combustible.combustibleActual > 0)
        {
            particulasMotorFrontal_R.Play();
            audioImpulsoLateral.Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            particulasMotorFrontal_R.Stop();
            audioImpulsoLateral.Stop();
        }

        // Derecha
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && SC_Combustible.combustibleActual > 0)
        {
            particulasMotorFrontal_L.Play();
            audioImpulsoLateral.Play();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            particulasMotorFrontal_L.Stop();
            audioImpulsoLateral.Stop();
        }

        // Abajo
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) && SC_Combustible.combustibleActual > 0)
        {
            particulasMotorFrontal_R.Play();
            particulasMotorFrontal_L.Play();
            audioImpulsoLateral.Play();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            particulasMotorFrontal_R.Stop();
            particulasMotorFrontal_L.Stop();
            audioImpulsoLateral.Stop();
        }
#endif

#if UNITY_ANDROID

        // Logica para control con botones de UI en Android
        // Arriba
        if (botonArribaPulsado && SC_Combustible.combustibleActual > 0)
        {
            audioImpulso.Play();
            particulasMotor_R.Play();
            particulasMotor_L.Play();
        }
        else if (!botonArribaPulsado)
        {
            audioImpulso.Stop();
            particulasMotor_R.Stop();
            particulasMotor_L.Stop();
        }
        // Izquierda
        if (botonIzquierdaPulsado && !botonAbajoPulsado && !botonLaterialIzquierdo && SC_Combustible.combustibleActual > 0)
        {
            particulasMotorFrontal_R.Play();
            audioImpulsoLateral.Play();
            Debug.Log("Izuquierda entra");
        }
        else if (!botonIzquierdaPulsado && !botonLaterialIzquierdo && !botonAbajoPulsado && !botonLaterialIzquierdo) 
        {
            particulasMotorFrontal_R.Stop();
            audioImpulsoLateral.Stop();
            Debug.Log("Izuquierda sale");
        }
        // Derecha
        if (botonDerechaPulsado && !botonAbajoPulsado && !botonLaterialDerecho && SC_Combustible.combustibleActual > 0)
        {
            particulasMotorFrontal_L.Play();
            audioImpulsoLateral.Play();
        }
        else if (!botonDerechaPulsado && !botonLaterialDerecho && !botonAbajoPulsado && !botonLaterialDerecho)
        {
            particulasMotorFrontal_L.Stop();
            audioImpulsoLateral.Stop();
        }
        // Abajo
        if (botonAbajoPulsado && !botonIzquierdaPulsado && !botonLaterialIzquierdo && !botonDerechaPulsado && !botonLaterialDerecho && SC_Combustible.combustibleActual > 0)
        {
            particulasMotorFrontal_R.Play();
            particulasMotorFrontal_L.Play();
            audioImpulsoLateral.Play();
        }
        else if (!botonAbajoPulsado && !botonIzquierdaPulsado && !botonLaterialIzquierdo && !botonDerechaPulsado && !botonLaterialDerecho)
        {
            particulasMotorFrontal_R.Stop();
            particulasMotorFrontal_L.Stop();
            audioImpulsoLateral.Stop();
        }
#endif
    }

    void FixedUpdate()
    {
        Impulso(); // Metodo para actualizar el impulso  segun la W o S o Arriba o Abajo pulsada.
        ImpulsoLateral(); // Metodo para actualizar el  segun la A o D pulsada.
        Rotacion(); // Metodo para actualizar la rotacion segun la flecha Izquierda o Derecha pulsada.
    }

    void Impulso()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        // Verifica la entrada del usuario y el combustible antes de aplicar la fuerza.
        // Condición modificada para incluir el control por botón
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && SC_Combustible.combustibleActual > 0)
        {
            // Aplica fuerza de impulso hacia adelante si hay combustible suficiente.
            Vector2 forwardDirection = transform.up;
            rb2d.AddForce(forwardDirection * impulso * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && SC_Combustible.combustibleActual > 0)
        {
            // Aplica fuerza de impulso hacia atras si hay combustible suficiente.
            Vector2 backwardDirection = -transform.up;
            rb2d.AddForce(backwardDirection * impulsoLateral * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }

#endif

#if UNITY_ANDROID
        if (botonArribaPulsado && SC_Combustible.combustibleActual > 0)
        {
            // Aplica fuerza de impulso hacia adelante si hay combustible suficiente.
            Vector2 forwardDirection = transform.up;
            rb2d.AddForce(forwardDirection * impulso * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
            Debug.Log("Impulso hacia adelante activado.");
        } 
        else if (botonAbajoPulsado && SC_Combustible.combustibleActual > 0)
        {
            // Aplica fuerza de impulso hacia atras si hay combustible suficiente.
            Vector2 backwardDirection = -transform.up;
            rb2d.AddForce(backwardDirection * impulsoLateral * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }
#endif

    }

    void ImpulsoLateral()
    {
#if UNITY_STANDALONE || UNITY_EDITOR

        if (Input.GetKey(KeyCode.RightArrow) && SC_Combustible.combustibleActual > 0)
        {
            Vector2 rightDirection = transform.right;
            rb2d.AddForce(rightDirection * impulsoLateral * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && SC_Combustible.combustibleActual > 0)
        {
            Vector2 leftDirection = -transform.right;
            rb2d.AddForce(leftDirection * impulsoLateral * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }
#endif

#if UNITY_ANDROID

        if (botonLaterialDerecho && SC_Combustible.combustibleActual > 0)
        {
            Vector2 rightDirection = transform.right;
            rb2d.AddForce(rightDirection * impulsoLateral * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }
        else if (botonLaterialIzquierdo && SC_Combustible.combustibleActual > 0)
        {
            Vector2 leftDirection = -transform.right;
            rb2d.AddForce(leftDirection * impulsoLateral * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }
#endif

    }

    void Rotacion()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        // Rotacion con teclas A y D
        if (Input.GetKey(KeyCode.A) && SC_Combustible.combustibleActual > 0)
        {
            // Rotar hacia un lado (por ejemplo, izquierda) con A
            rb2d.MoveRotation(rb2d.rotation + rotacionVelocidad * Time.fixedDeltaTime);
            rb2d.angularVelocity = 0f; // Quita velocidad giro
        }
        else if (Input.GetKey(KeyCode.D) && SC_Combustible.combustibleActual > 0)
        {
            // Rotar hacia el otro lado (por ejemplo, derecha) con D
            rb2d.MoveRotation(rb2d.rotation - rotacionVelocidad * Time.fixedDeltaTime);
            rb2d.angularVelocity = 0f; // Quita velocidad giro
        }
#endif

#if UNITY_ANDROID
        // Rotacion con teclas A y D
        if (botonIzquierdaPulsado && SC_Combustible.combustibleActual > 0)
        {
            // Rotar hacia un lado (por ejemplo, izquierda) con A
            rb2d.MoveRotation(rb2d.rotation + rotacionVelocidad * Time.fixedDeltaTime);
            rb2d.angularVelocity = 0f; // Quita velocidad giro
        }
        else if (botonDerechaPulsado && SC_Combustible.combustibleActual > 0)
        {
            // Rotar hacia el otro lado (por ejemplo, derecha) con D
            rb2d.MoveRotation(rb2d.rotation - rotacionVelocidad * Time.fixedDeltaTime);
            rb2d.angularVelocity = 0f; // Quita velocidad giro
        }

#endif
    }
}
