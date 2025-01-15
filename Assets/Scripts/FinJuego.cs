using UnityEngine;
using UnityEngine.SceneManagement;

public class FinJuego : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    // Método para cambiar a la escena del juego
    public void Reiniciar()
    {
        SceneManager.LoadScene("SampleScene"); // Cambia al nombre de la escena deseada
    }

    // Método para cerrar el juego
    public void SalirJuego()
    {
        Application.Quit(); // Cierra la aplicación
        Debug.Log("Juego cerrado."); // Útil para probar en el editor
    }

}
