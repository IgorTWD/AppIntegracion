using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MenuPausa : MonoBehaviour
{
    private SC_FadeOut scFadeout;

    void Start()
    {
        scFadeout = GetComponent<SC_FadeOut>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            scFadeout.AbreMenuPausa();
        }
    }
}
