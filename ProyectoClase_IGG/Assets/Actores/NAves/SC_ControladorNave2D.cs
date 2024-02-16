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

    private bool botonArribaPulsado = false;
    private bool botonAbajoPulsado = false;
    private bool botonDerechaPulsado = false;
    private bool botonIzquierdaPulsado = false;
    public bool impulsado = false;

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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && SC_Combustible.combustibleActual > 0)
        {
            particulasMotorFrontal_R.Play();
            audioImpulsoLateral.Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) )
        {
            particulasMotorFrontal_R.Stop();
            audioImpulsoLateral.Stop();
        }

        // Derecha
        if (Input.GetKeyDown(KeyCode.RightArrow) && SC_Combustible.combustibleActual > 0)
        {
            particulasMotorFrontal_L.Play();
            audioImpulsoLateral.Play();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
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
    }

    void FixedUpdate()
    {
        Impulso(); // Metodo para actualizar el impulso  segun la W o S o Arriba o Abajo pulsada.
        ImpulsoLateral(); // Metodo para actualizar el  segun la A o D pulsada.
        Rotacion(); // Metodo para actualizar la rotacion segun la flecha Izquierda o Derecha pulsada.
    }

    void Impulso()
    {
        // Verifica la entrada del usuario y el combustible antes de aplicar la fuerza.
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && SC_Combustible.combustibleActual > 0 )
        {
            // Aplica fuerza de impulso hacia adelante si hay combustible suficiente.
            Vector2 forwardDirection = transform.up;
            rb2d.AddForce(forwardDirection * impulso * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
            Debug.Log("Impulso hacia adelante activado.");
        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && SC_Combustible.combustibleActual > 0)
        {
            // Aplica fuerza de impulso hacia atras si hay combustible suficiente.
            Vector2 backwardDirection = -transform.up;
            rb2d.AddForce(backwardDirection * impulsoLateral * SC_Combustible.ObtenerModificadorDeImpulso());
            SC_Combustible.ConsumirCombustible(Time.fixedDeltaTime);
        }

    }

    void ImpulsoLateral()
    {
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

    }

    void Rotacion()
    {
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
    }

    public void Impulsado()
    {
        impulsado = true;
        Debug.Log("Impulsado ACtivado.");
    }

    public void BtArribaPulsado()
    {
        botonArribaPulsado = true;
        Debug.Log("Impulsado ACtivado.");
    }

    public void BtArribaLiberado()
    {
        botonArribaPulsado = false;
    }

    public void BtAbajoPulsado()
    {
        botonAbajoPulsado = true;
    }

    public void BtAbajoLiberado()
    {
        botonAbajoPulsado = false;
    }

    public void BtDerechaPulsado()
    {
        botonDerechaPulsado = true;
    }

    public void BtDerechaLiberado()
    {
        botonDerechaPulsado = false;
    }

    public void BtIzquierdaPulsado()
    {
        botonIzquierdaPulsado = true;
    }

    public void BtIzquierdaLiberado()
    {
        botonIzquierdaPulsado = false;
    }
}
