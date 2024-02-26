using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_FadeIn : MonoBehaviour
{
    public Animator animator;
    public GameObject fadePrefab;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Invoke("FadeIn", 0.1f);
        } 
        else
        {
            Invoke("FadeIn", 0f);
        }
        
        

    }

    public void FadeIn()
    {
        fadePrefab.SetActive(true);
        animator.Play("FadeIn");
        //Time.timeScale = 1.0f;
    }
}
