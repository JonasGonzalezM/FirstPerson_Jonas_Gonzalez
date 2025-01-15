using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    // M�todo para cambiar a la escena del juego
    public void IniciarJuego()
    {
        SceneManager.LoadScene("SampleScene"); // Cambia al nombre de la escena deseada
    }

    // M�todo para cerrar el juego
    public void SalirJuego()
    {
        Application.Quit(); // Cierra la aplicaci�n
        Debug.Log("Juego cerrado."); // �til para probar en el editor
    }
}
