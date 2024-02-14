using UnityEngine;
using Cinemachine;

public class SC_CamaraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara Virtual.

    void Update()
    {
        if (virtualCamera.Follow == null)
        {
            // Busca la nave en la escena por el tag.
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                virtualCamera.Follow = player.transform;
            }
        }
    }

}
