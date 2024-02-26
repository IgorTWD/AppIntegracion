using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine.UI; 

public class MenuInicial : MonoBehaviour
{
    // Variables para manejar las selecciones del usuario en la UI
    private TMP_Dropdown DpNave; // Dropdown para elegir la nave
    private TMP_Dropdown DpCombustible; // Dropdown para elegir el tipo de combustible
    private Image imagenNave; // Imagen que muestra la nave seleccionada
    private Sprite[] spritesNaves; // Array de sprites de las naves disponibles

    // Variables para los sliders y sus colores
    private Slider sliderConsumo; // Muestra el consumo de combustible
    private Slider sliderVelocidad; // Muestra la velocidad de la nave
    private Image fillConsumo; // Color de relleno del slider de consumo
    private Image fillVelocidad; // Color de relleno del slider de velocidad

    private int tipoCombustible; // Almacena la seleccion actual del tipo de combustible

    void Start()
    {
        

        // Busca y asigna los componentes necesarios en la UI
        DpNave = GameObject.Find("DpNave").GetComponent<TMP_Dropdown>();
        DpCombustible = GameObject.Find("DpCombustible").GetComponent<TMP_Dropdown>();
        imagenNave = GameObject.Find("ImagenNave").GetComponent<Image>();

        // Carga los sprites de las naves desde la carpeta de recursos
        spritesNaves = Resources.LoadAll<Sprite>("Imagenes/nave");

        // Asigna los listeners a los dropdowns para responder a cambios
        DpNave.onValueChanged.AddListener(delegate { CambiarImagenNave(); });
        DpCombustible.onValueChanged.AddListener(delegate { ActualizarTipoCombustible(); });

        // Configura los sliders de consumo y velocidad
        ConfigurarSliders();

        // Actualiza la UI inicialmente
        CambiarImagenNave();
        ActualizarTipoCombustible();
    }

    // Configura los sliders encontrando los componentes necesarios y asignando los valores iniciales
    void ConfigurarSliders()
    {
        sliderConsumo = GameObject.Find("SliderConsumo").GetComponent<Slider>();
        fillConsumo = sliderConsumo.transform.Find("Fill Area/Fill").GetComponent<Image>();

        sliderVelocidad = GameObject.Find("SliderVelocidad").GetComponent<Slider>();
        fillVelocidad = sliderVelocidad.transform.Find("Fill Area/Fill").GetComponent<Image>();
    }

    // Cambia la imagen de la nave en la UI basada en la seleccion del dropdown
    void CambiarImagenNave()
    {
        if (imagenNave != null && spritesNaves.Length > DpNave.value)
        {
            imagenNave.sprite = spritesNaves[DpNave.value];
        }
    }

    // Actualiza el tipo de combustible seleccionado y refresca los sliders de la UI
    void ActualizarTipoCombustible()
    {
        tipoCombustible = DpCombustible.value;
        ActualizarSliders();
    }

    // Ajusta los valores de los sliders y sus colores de fondo basados en la seleccion actual
    void ActualizarSliders()
    {
        sliderConsumo.value = ObtenerModificadorDeConsumo();
        sliderVelocidad.value = ObtenerModificadorDeImpulso();
        ActualizarColorFondo();
    }

    // Ajusta el color de fondo de los sliders basado en el tipo de combustible
    void ActualizarColorFondo()
    {
        if (tipoCombustible == 0)
        {
            fillConsumo.color = Color.yellow;
            fillVelocidad.color = Color.yellow;
        }
        else if (tipoCombustible == 1)
        {
            fillConsumo.color = Color.red;
            fillVelocidad.color = new Color(1f, 0.64f, 0f); // Naranja
        }
        else if (tipoCombustible == 2)
        {
            fillConsumo.color = Color.green;
            fillVelocidad.color = Color.red;
        }
        else if (tipoCombustible == 3)
        {
            fillConsumo.color = Color.yellow;
            fillVelocidad.color = Color.green;
        }
    }

    // Calcula el modificador de consumo basado en el tipo de combustible
    float ObtenerModificadorDeConsumo()
    {
        // Retorna un valor dependiendo del tipo de combustible
        switch (tipoCombustible)
        {
            case 0: return 0.5f;
            case 1: return 1f;
            case 2: return 0.1f;
            case 3: return 0.3f;
            default: return 1f;
        }
    }

    // Calcula el modificador de velocidad basado en el tipo de combustible
    float ObtenerModificadorDeImpulso()
    {
        // Retorna un valor dependiendo del tipo de combustible
        switch (tipoCombustible)
        {
            case 0: return 0.5f;
            case 1: return 0.8f;
            case 2: return 0.1f;
            case 3: return 1f;
            default: return 1f;
        }
    }

    // Inicia el juego guardando las selecciones del jugador y cargando la proxima escena
    public void Jugar()
    {
        PlayerPrefs.SetInt("NaveSeleccionada", DpNave.value);
        PlayerPrefs.SetInt("TipoCombustibleSeleccionado", DpCombustible.value);
        PlayerPrefs.Save(); // Guarda las preferencias del usuario

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Carga la siguiente escena
    }

    // Cierra la aplicacion en editor y compilado
    public void Salir()
    {
        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }

  
}

