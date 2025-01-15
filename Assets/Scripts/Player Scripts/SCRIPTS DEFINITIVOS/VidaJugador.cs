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
    public GameObject canvasMuerte; // Asignar el Canvas de muerte en el Inspector
    public GameObject canvasHUD; // Asignar el Canvas HUD en el Inspector
    public GameObject canvasFinJuego; // Asignar el Canvas de fin de juego en el Inspector
    public Transform spawnPoint; // Punto de reaparición del jugador

    void Start()
    {
        vidaActual = vidaJugador;
        canvasMuerte.SetActive(false); // Asegúrate de que el Canvas de muerte esté desactivado al inicio
        canvasFinJuego.SetActive(false); // Asegúrate de que el Canvas de fin de juego esté desactivado al inicio
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

    // Mostrar el Canvas de fin de juego
    private void MostrarFinJuego()
    {
        Time.timeScale = 0; // Pausar el juego
        canvasHUD.SetActive(false); // Desactivar el Canvas HUD
        canvasFinJuego.SetActive(true); // Activar el Canvas de fin de juego
    }

    // Método para el botón de reiniciar
    public void ReiniciarJuego()
    {
        Time.timeScale = 1; // Reactiva el tiempo del juego
        canvasMuerte.SetActive(false); // Oculta el Canvas de muerte
        canvasFinJuego.SetActive(false); // Oculta el Canvas de fin de juego
        canvasHUD.SetActive(true); // Activa el Canvas HUD
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
