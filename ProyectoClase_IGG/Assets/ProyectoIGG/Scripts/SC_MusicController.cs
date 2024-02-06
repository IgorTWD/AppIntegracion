using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    // Declaracion de variables contenedoras de audio publicas para poderlas editar en el viewport directamente.
    public AudioClip backgroundMusic;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip explosionLoseSound;
    public AudioClip winSound2; // Segundo sonido a reproducir
    // La fuente de audio, quien tiene el play.
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    // Metodo que inicia musica de fondo
    public void PlayBackgroundMusic()
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Metodo que inicia musica de victoria
    public void PlayVictoryMusic()
    {
        audioSource.Stop(); // Detiene la música de fondo
        audioSource.clip = winSound; // Cambia la fuente de audio
        audioSource.loop = false; // Quita el loop
        audioSource.Play(); // Reproduce audio

        // Inicia una coroutine para pausar el hilo y reproducir el segundo sonido después de este
        StartCoroutine(PlaySecondSoundAfterDelay(winSound.length));
    }

    // Metodo que inicia 2º musica de victoria con delay para dar tiempo a la primera
    private IEnumerator PlaySecondSoundAfterDelay(float delay)
    {
        // Espera a que el primer sonido termine
        yield return new WaitForSeconds(delay);

        // Reproduce el segundo sonido despues del delay
        audioSource.clip = winSound2;
        audioSource.loop = false; 
        audioSource.Play();
    }

    // Metodo que inicia musica cuando colisionas con el escenario
    public void PlayDefeatSound()
    {
        audioSource.Stop(); // Detiene la música de fondo
        audioSource.clip = loseSound;
        audioSource.loop = false;
        audioSource.Play();

    }

    // Metodo que inicia la musica cuando colisionas con asteroides
    public void PlayExplosionSound()
    {
        audioSource.Stop(); // Detiene la música de fondo
        audioSource.clip = explosionLoseSound;
        audioSource.loop = false;
        audioSource.Play();
    }
   
    
}

