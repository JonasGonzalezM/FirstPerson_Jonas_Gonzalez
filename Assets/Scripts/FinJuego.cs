using UnityEngine;
using UnityEngine.SceneManagement;

public class FinJuego : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    // M�todo para cambiar a la escena del juego
    public void Reiniciar()
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
