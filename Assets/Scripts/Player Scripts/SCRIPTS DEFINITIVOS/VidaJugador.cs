using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaJugador : MonoBehaviour
{
    [Header("Atributos de Vida")]
    public float vidaJugador = 100f;
    private float vidaActual;

    [Header("Canvases")]
    AudioSource musica;
    //public GameObject canvasMuerte; // Asignar el Canvas de muerte en el Inspector
    //public GameObject canvasHUD; // Asignar el Canvas HUD en el Inspector
    public GameObject teclaE; // Asignar el Canvas de fin de juego en el Inspector
    [SerializeField] private KitMedico kitMedico;
    
    public Transform spawnPoint; // Punto de reaparición del jugador

    void Start()
    {
        vidaActual = vidaJugador;


    }
    void Update()
    {
        Morir();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaEnemiga"))
        {
            vidaActual -= 10; // Se quitan puntos de vida 
            Destroy(other.gameObject); // Destruir la bala
        }

        if (other.CompareTag("KitMedico"))
        {
            vidaActual += 100;

            if (vidaActual >= 100)
            {
                vidaActual = 100;
            }

            Destroy(other.gameObject);
        }

        if (other.CompareTag("SueloMortal"))
        {
            vidaActual -= 100;
        }

        // Si el jugador entra en una zona de fin de juego
        if (other.CompareTag("FinDelJuego"))
        {
            MostrarFinJuego();
        }

        if (other.CompareTag("KitMedico"))
        {
           teclaE.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(other.gameObject);
                vidaActual += 100;
                teclaE.SetActive(false);

                if (vidaActual >= 100)
                {
                    vidaActual = 100;
                }
            }
        }
    }

    private void Morir()
    {
        if (vidaActual <= 0)
        {

            SceneManager.LoadScene("GameOver");
            //Cursor.lockState = CursorLockMode.None; 
        }
    }

    // Mostrar el Canvas de fin de juego
    private void MostrarFinJuego()
    {
        SceneManager.LoadScene("GameOver");
    }

    
}
