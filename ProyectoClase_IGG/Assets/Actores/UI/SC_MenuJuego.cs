using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MenuJuego : MonoBehaviour
{
    public GameObject interfazBienvenidaPC;
    public GameObject interfazTutorialPC;
    public GameObject interfazTutorialAndroid;
    public GameObject interfazBienvenidaAndroid;
    public GameObject HUD;


    // Start is called before the first frame update
    void Start()
    {
        MostrarBienvenida();
    }

    public void MostrarBienvenida()
    {
        // Pausa el juego
        Time.timeScale = 0;

    #if UNITY_STANDALONE || UNITY_EDITOR
        // Muestra la interfaz de bienvenida en PC
        interfazBienvenidaPC.SetActive(true);
        interfazTutorialPC.SetActive(false);
        interfazTutorialAndroid.SetActive(false);
        interfazBienvenidaAndroid.SetActive(false);
    #endif

    #if UNITY_ANDROID
        // Muestra la interfaz de bienvenida en Android
        interfazBienvenidaAndroid.SetActive(true);
        interfazTutorialPC.SetActive(false);
        interfazTutorialAndroid.SetActive(false);
        interfazBienvenidaPC.SetActive(false);
    #endif
    }

    public void CerrarBienvenida()
    {
    #if UNITY_STANDALONE || UNITY_EDITOR
        interfazBienvenidaPC.SetActive(false);
    #endif

    #if UNITY_ANDROID
        interfazBienvenidaAndroid.SetActive(false);
    #endif
        
        Time.timeScale = 0;
    }

    public void MostrarTutorial()
    {

    #if UNITY_STANDALONE || UNITY_EDITOR
        interfazTutorialPC.SetActive(true);
        interfazTutorialAndroid.SetActive(false);
    #endif

    #if UNITY_ANDROID
        interfazTutorialPC.SetActive(false);
        interfazTutorialAndroid.SetActive(true);
    #endif

        Time.timeScale = 0;
    }

    public void CerrarTutorial()
    {

    #if UNITY_STANDALONE || UNITY_EDITOR
        interfazTutorialPC.SetActive(false);
    #endif

    #if UNITY_ANDROID
        interfazTutorialAndroid.SetActive(false);
    #endif
        
        HUD.SetActive(true);
        Time.timeScale = 1;
    }

}
