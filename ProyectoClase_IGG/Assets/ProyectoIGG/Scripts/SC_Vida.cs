using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public GameObject[] corazon;
    private int vida;
    private bool muerto;

    private void Start()
    {
        vida = corazon.Length;
    }

    void update()
    {
        if (muerto = true)
        {
            //Debug.Log("Perdiste");
        }

        //if (vida < 1)
        //{
        //    Destroy(corazon[0].gameObject);

        //}
        
        //if(vida <2)
        //{
        //    Destoy(corazon[1].gameObject);
        //}
        
        //if(vida < 3)
        //{
        //    Destroy(corazon[2].gameObject);
        //}
    }

    public void RecibeDaño(int d)
    {
        if (vida >= 1) 
        {
            vida -= d;
            Destroy(corazon[vida].gameObject);
            
            if (vida <1)
            {
                muerto = true;
            }
        }
    }
}
