using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJugador : MonoBehaviour
{
    [Header("Atributos de Vida")]
    public float vidaJugador = 100f;
    private float vidaActual;

    [Header("UI de Muerte")]
    public GameObject canvasMuerte; // Asignar el Canvas en el Inspector
    public GameObject canvasHUD; // Asignar el Canvas en el Inspector
    public Transform spawnPoint; // Punto de reaparición del jugador

    void Start()
    {
        vidaActual = vidaJugador;
        canvasMuerte.SetActive(false); // Asegúrate de que el Canvas esté desactivado al inicio
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
    }

    private void Morir()
    {
        if (vidaActual <= 0)
        {
            Time.timeScale = 0; // Detiene el tiempo del juego
            canvasHUD.SetActive(false);
            canvasMuerte.SetActive(true); // Activa el Canvas de muerte
        }
    }

    // Método para el botón de reiniciar
    public void ReiniciarJuego()
    {
        Time.timeScale = 1; // Reactiva el tiempo del juego
        canvasMuerte.SetActive(false); // Oculta el Canvas
        canvasHUD.SetActive(true);
        vidaActual = vidaJugador; // Restaura la vida
        transform.position = spawnPoint.position; // Mueve al jugador al punto de spawn
    }

    // Método para el botón de salir
    public void SalirJuego()
    {
        Application.Quit(); // Cierra la aplicación
        Debug.Log("Juego cerrado."); // Útil para probar en el editor
    }
}
