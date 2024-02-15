using UnityEngine;
using UnityEngine.UI;

public class SC_Combustible : MonoBehaviour
{
    // Variables para controlar el combustible.
    public float combustibleMaximo = 100f; // El máximo de combustible que puede tener.
    public float combustibleActual; // Cuánto combustible queda actualmente.
    public float impulsoBase = 1f; // Valor base de impulso que proporciona el combustible.

    private int tipoCombustible; // Identifica el tipo de combustible seleccionado.

    private Slider sliderCombustible; // Referencia al slider que muestra el combustible.
    private Image fondoSlider; // Referencia a la imagen de fondo del slider para cambiar su color.

    void Start()
    {
        // Busca y asigna el slider de combustible y su imagen de fondo.
        GameObject sliderObj = GameObject.Find("SliderCombustible");
        if (sliderObj != null)
        {
            sliderCombustible = sliderObj.GetComponent<Slider>();
            Transform fillTransform = sliderCombustible.transform.Find("Fill Area/Fill");
            fondoSlider = fillTransform.GetComponent<Image>();
        }

        // Inicializa el combustible y configura el slider.
        tipoCombustible = PlayerPrefs.GetInt("TipoCombustibleSeleccionado", 0); // Usa 0 como valor por defecto.
        combustibleActual = combustibleMaximo;
        sliderCombustible.maxValue = combustibleMaximo;
        ActualizarSliderCombustible(); // Actualiza la UI del slider al inicio.
    }

    void ActualizarSliderCombustible()
    {
        // Establece el valor del slider y actualiza el color de fondo según el porcentaje de combustible restante.
        sliderCombustible.value = combustibleActual;
        ActualizarColorFondo();
    }

    void ActualizarColorFondo()
    {
        // Cambia el color del fondo del slider basado en el combustible restante.
        float porcentajeCombustible = combustibleActual / combustibleMaximo;
        fondoSlider.color = ObtenerColorPorPorcentaje(porcentajeCombustible);
    }

    Color ObtenerColorPorPorcentaje(float porcentaje)
    {
        // Define el color del slider basado en el porcentaje de combustible restante.
        if (porcentaje <= 0) return Color.gray;
        if (porcentaje <= 0.2f) return Color.red;
        if (porcentaje <= 0.4f) return new Color(1f, 0.64f, 0); // Naranja
        if (porcentaje <= 0.7f) return Color.yellow;
        return Color.green; // Más del 70% de combustible restante.
    }

    public void ConsumirCombustible(float tiempoAceleracion)
    {
        // Calcula y aplica el consumo de combustible basado en el tiempo de aceleración y el tipo de combustible.
        float consumo = ObtenerModificadorDeConsumo();
        combustibleActual -= consumo * tiempoAceleracion;
        combustibleActual = Mathf.Max(0, combustibleActual); // Asegura que el combustible no sea negativo.
        ActualizarSliderCombustible(); // Refleja los cambios en la UI.
    }

    float ObtenerModificadorDeConsumo()
    {
        // Devuelve un valor de consumo específico para cada tipo de combustible.
        switch (tipoCombustible)
        {
            case 0: return 4f; // Normal
            case 1: return 8f; // Premium
            case 2: return 0.4f; // Eco
            case 3: return 0.9f; // Super
            default: return 1f; // Por defecto
        }
    }

    public float ObtenerModificadorDeImpulso()
    {
        // Devuelve un valor de impulso específico para cada tipo de combustible.
        switch (tipoCombustible)
        {
            case 0: return 1f; // Normal
            case 1: return 0.8f; // Premium
            case 2: return 0.2f; // Eco
            case 3: return 4f; // Super
            default: return 1f; // Por defecto
        }
    }
}
