using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Método para cambiar a la escena del juego
    public void IniciarJuego()
    {
        SceneManager.LoadScene("Reiniciar"); // Cambia al nombre de la escena deseada
    }

    // Método para cerrar el juego
    public void SalirJuego()
    {
        Application.Quit(); // Cierra la aplicación
        Debug.Log("Juego cerrado."); // Útil para probar en el editor
    }
}
