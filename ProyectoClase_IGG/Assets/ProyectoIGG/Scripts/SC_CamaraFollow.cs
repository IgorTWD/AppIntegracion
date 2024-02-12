using UnityEngine;
using Cinemachine;

public class SC_CamaraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara Virtual de Cinemachine
    public string playerTag = "Player"; // El tag asignado a tu nave/jugador

    void Update()
    {
        if (virtualCamera.Follow == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player != null)
            {
                virtualCamera.Follow = player.transform;
            }
        }
    }

}
