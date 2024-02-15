using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Declaracion de variables contenedoras de audio publicas para poderlas editar en el viewport directamente.
    public AudioClip backgroundMusic;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip explosionLoseSound;
    public AudioClip winSound2; // Segundo sonido a reproducir
    public AudioClip damange;
    // La fuente de audio, quien tiene el play.
    private AudioSource audioSourceBack;
    private AudioSource audioSourceEfect;

    void Start()
    {
        // Busca 2 fuentes de audio, sino crea el que falta.
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length > 1)
        {
            audioSourceBack = audioSources[0]; // Para musica de fondo
            audioSourceEfect = audioSources[1]; // Para efectos de sonido.
        }
        else
        {
            audioSourceBack = GetComponent<AudioSource>();
            // Crea dinamicamente otro AudioSource para efectos de sonido si no se encuentra
            audioSourceEfect = gameObject.AddComponent<AudioSource>();
        }
        PlayBackgroundMusic();
    }

    // Metodo que inicia musica de fondo
    public void PlayBackgroundMusic()
    {
        audioSourceBack.clip = backgroundMusic;
        audioSourceBack.loop = true;
        audioSourceBack.Play();
    }

    // Metodo que inicia musica de victoria
    public void PlayVictoryMusic()
    {
        audioSourceBack.Stop(); // Detiene la musica de fondo
        audioSourceBack.clip = winSound; // Cambia la fuente de audio
        audioSourceBack.loop = false; // Quita el loop
        audioSourceBack.Play(); // Reproduce audio

        // Inicia una coroutine para pausar el hilo y reproducir el segundo sonido despues de este
        StartCoroutine(PlaySecondSoundAfterDelay(winSound.length));
    }

    // Metodo que inicia 2 musica de victoria con delay para dar tiempo a la primera
    private IEnumerator PlaySecondSoundAfterDelay(float delay)
    {
        // Espera a que el primer sonido termine
        yield return new WaitForSeconds(delay);

        // Reproduce el segundo sonido despues del delay
        audioSourceBack.clip = winSound2;
        audioSourceBack.loop = false;
        audioSourceBack.Play();
    }

    // Metodo que inicia musica cuando colisionas con el escenario
    public void PlayDefeatSound()
    {
        audioSourceBack.Stop(); // Detiene la musica de fondo
        audioSourceBack.clip = loseSound;
        audioSourceBack.loop = false;
        audioSourceBack.Play();
    }

    // Metodo que inicia la musica cuando colisionas con asteroides
    public void PlayExplosionSound()
    {
        audioSourceBack.Stop(); // Detiene la musica de fondo
        audioSourceBack.clip = explosionLoseSound;
        audioSourceBack.loop = false;
        audioSourceBack.Play();
    }

    public void PlayDamange()
    {
        audioSourceEfect.clip = damange;
        audioSourceEfect.loop = false;
        audioSourceEfect.Play();
    }

}


