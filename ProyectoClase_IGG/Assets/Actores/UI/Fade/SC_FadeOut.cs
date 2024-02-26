using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_FadeOut : MonoBehaviour
{
    public Animator animator;

    public TMP_Text puntos;
    public TMP_Text textoPuntos;
    public TMP_Text textoVictoria;

    public GameObject btContinuar;
    public GameObject btContinuarPause;
    public GameObject btReintentar;

    private SC_Vida scVida;
    private SC_Puntuacion scPuntuacion;


        
    // Start is called before the first frame update
    void Start()
    {
        

        puntos.enabled = false;
        textoPuntos.enabled = false;
        textoVictoria.enabled = false;
        btContinuar.SetActive(false);
        btReintentar.SetActive(false);
        //Invoke("FadeOut", 1);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Finish"))
    //    {
    //        nave = GameObject.FindGameObjectWithTag("Player");
    //        colider = nave.GetComponent<Collider2D>();
    //        FadeOut(); 
    //        Debug.Log("Paso por la colision: "+ colider.name);
    //    }
    //}


    public void Fade()
    {
        //Time.timeScale = 0;

        scVida = GameObject.FindGameObjectWithTag("Player").GetComponent<SC_Vida>();
        scPuntuacion = GameObject.FindGameObjectWithTag("HUD").GetComponent<SC_Puntuacion>();

        puntos.text = scPuntuacion.puntuacion.ToString();

        if (scVida.win)
        {
            
            FadeOutWin();
        } 
        else if (scVida.muerto)
        {
            FadeOutLose();
        }
        else
        {
            FadeOut();
        }
    }

    public void Continuar()
    {
        FadeOut();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Reiniciar()
    {
        FadeOut();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FadeOut()
    {
        
        Debug.Log("Intentando...");
        animator.Play("FadeOut");
    }

    private void FadeOutWin()
    {
        btContinuar.SetActive(true);
        btReintentar.SetActive(true);
        btContinuar.GetComponent<Button>().interactable = true;

        animator.Play("FadeOutWin");
        textoVictoria.text = "Victoria!!";

        puntos.enabled = true;
        textoPuntos.enabled = true;
        textoVictoria.enabled = true;

    }

    private void FadeOutPausa()
    {
        animator.Play("FadeOutPause");
        textoVictoria.text = "Pause";


        btContinuarPause.SetActive(true);
        btReintentar.SetActive(true);


        puntos.enabled = true;
        textoPuntos.enabled = true;
        textoVictoria.enabled = true;

    }

    private void FadeOutLose()
    {
        btReintentar.SetActive(true);
        btContinuar.SetActive(true);
        btContinuar.GetComponent<Button>().interactable = false;

        animator.Play("FadeOutLose");
        textoVictoria.text = "Te estampaste!";

        puntos.enabled = true;
        textoPuntos.enabled = true;
        textoVictoria.enabled = true;
    }

    public void AbreMenuPausa()
    {
        scPuntuacion = GameObject.FindGameObjectWithTag("HUD").GetComponent<SC_Puntuacion>();
        puntos.text = scPuntuacion.puntuacion.ToString();

        FadeOutPausa();

        Time.timeScale = 0f;

    }
    public void CierraMenuPausa()
    {
        btContinuarPause.SetActive(false);
        btReintentar.SetActive(false);
        puntos.enabled = false;
        textoPuntos.enabled = false;
        textoVictoria.enabled = false;

        Time.timeScale = 1.0f;
    }

}
