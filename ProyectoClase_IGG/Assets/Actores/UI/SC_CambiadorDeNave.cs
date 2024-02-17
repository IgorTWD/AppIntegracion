using UnityEngine;

public class SC_CambiadorDeNave : MonoBehaviour
{

    public GameObject[] prefabsDeNaves; // Array público de prefabs de naves que podemos asignar desde el editor de Unity
    private Transform  puntoDeSpawn; // Variable privada para almacenar la referencia al punto de spawn en la escena

    private SC_ControladorNave2D sc_ControladorNave;

    void Start()
    {
        

        // Busca en la escena el objeto llamado "PuntoSpawn" y guardamos su transform
        puntoDeSpawn = GameObject.Find("PuntoSpawn").transform;

        CambiarNave();

        sc_ControladorNave = FindObjectOfType<SC_ControladorNave2D>();
    }

    // Método para cambiar la nave basado en la selección guardada
    public void CambiarNave()
    {
        // Leemos el índice de la nave seleccionada de PlayerPrefs, 0 es el valor por defecto si no se encuentra nada
        int indiceSeleccionado = PlayerPrefs.GetInt("NaveSeleccionada", 0); 

        // Verifica si ya existe una nave y destrúyela
        if (puntoDeSpawn != null && puntoDeSpawn.gameObject != null)
        {
            Destroy(puntoDeSpawn.gameObject);
        }

        // Instanciar la nueva nave basada en la selección del dropdown
        if (indiceSeleccionado >= 0 && indiceSeleccionado < prefabsDeNaves.Length)
        {
            // Instanciamos la nueva nave en la posición y rotación del punto de spawn
            GameObject nuevaNave = Instantiate(prefabsDeNaves[indiceSeleccionado], puntoDeSpawn.position, puntoDeSpawn.rotation);
            nuevaNave.transform.SetParent(puntoDeSpawn.parent); // Hacemos que la nueva nave sea hija del punto de spawn para mantener la jerarquía organizada
        }
    }

    //public void Impulsado()
    //{
    //    sc_ControladorNave.impulsado = true;
    //    Debug.Log("Impulsado ACtivado.");
    //}

    public void BtArribaPulsado()
    {
        sc_ControladorNave.botonArribaPulsado = true;
        Debug.Log("BtArriba ACtivado." + sc_ControladorNave.botonArribaPulsado);
    }

    public void BtArribaLiberado()
    {
        sc_ControladorNave.botonArribaPulsado = false;
        Debug.Log("BtArriba Liberado." + sc_ControladorNave.botonArribaPulsado);
    }

    public void BtAbajoPulsado()
    {
        sc_ControladorNave.botonAbajoPulsado = true;
    }

    public void BtAbajoLiberado()
    {
        sc_ControladorNave.botonAbajoPulsado = false;
    }

    public void BtDerechaPulsado()
    {
        sc_ControladorNave.botonDerechaPulsado = true;
    }

    public void BtDerechaLiberado()
    {
        sc_ControladorNave.botonDerechaPulsado = false;
    }

    public void BtIzquierdaPulsado()
    {
        sc_ControladorNave.botonIzquierdaPulsado = true;
    }

    public void BtIzquierdaLiberado()
    {
        sc_ControladorNave.botonIzquierdaPulsado = false;
    }

    public void botonLaterialIzquierdoPulsado()
    {
        sc_ControladorNave.botonLaterialIzquierdo = true;
    }
    public void botonLaterialIzquierdoLiberado()
    {
        sc_ControladorNave.botonLaterialIzquierdo = false;
    }

    public void botonLaterialDerechoPulsado()
    {
        sc_ControladorNave.botonLaterialDerecho = true;
    }
    public void botonLaterialDerechoLiberado()
    {
        sc_ControladorNave.botonLaterialDerecho = false;
    }
    public bool botonLaterialDerecho = false;

}
