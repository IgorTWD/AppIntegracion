using UnityEngine;

public class SC_CambiadorDeNave : MonoBehaviour
{

    public GameObject[] prefabsDeNaves; // Array de prefabs de naves
    private Transform  puntoDeSpawn; // Para almacenar la referencia del punto de spawn para la nave

    private SC_ControladorNave2D sc_ControladorNave;

    public GameObject controlesAndroid;

    void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR

    controlesAndroid.SetActive(false);
#endif

#if UNITY_ANDROID
    controlesAndroid.SetActive(true);
#endif


        // Busca en la escena el objeto "PuntoSpawn" y guarda su transform
        puntoDeSpawn = GameObject.Find("PuntoSpawn").transform;

        CambiarNave();

        sc_ControladorNave = FindObjectOfType<SC_ControladorNave2D>();
    }



    // Metodo para cambiar la nave basado en la seleccion guardada
    public void CambiarNave()
    {
        // Leemos el indice de la nave seleccionada de PlayerPrefs, 0 es el valor por defecto si no se encuentra nada
        int indiceSeleccionado = PlayerPrefs.GetInt("NaveSeleccionada", 0); 

        // Verifica si ya existe una nave y la destruye
        if (puntoDeSpawn != null && puntoDeSpawn.gameObject != null)
        {
            Destroy(puntoDeSpawn.gameObject);
        }

        // Instancia la nueva nave basada en la seleccion del dropdown
        if (indiceSeleccionado >= 0 && indiceSeleccionado < prefabsDeNaves.Length)
        {
            GameObject nuevaNave = Instantiate(prefabsDeNaves[indiceSeleccionado], puntoDeSpawn.position, puntoDeSpawn.rotation);
        }
    }

    //public void Impulsado()
    //{
    //    sc_ControladorNave.impulsado = true;
    //    Debug.Log("Impulsado ACtivado.");
    //}

    // Metodos intermediarios para el control de la nave con la interfaz Android.
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
