using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para UI

public class FuncionesPlayer : MonoBehaviour
{
    // Salud del jugador
    public float saludMaxima = 100f;
    public float saludActual;

    // Munici�n
    public int municionMaxima = 30;
    public int municionActual;

    // Referencias al UI (HUD)
    public Text saludTexto;
    public Text municionTexto;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializamos la salud y la munici�n
        saludActual = saludMaxima;
        municionActual = municionMaxima;

        // Asegurarnos de que el texto del HUD est� correctamente actualizado
        ActualizarHUD();
    }

    // Update is called once per frame
    void Update()
    {
        // Aqu� puedes simular da�os o recuperaci�n de salud, por ejemplo:
        if (Input.GetKeyDown(KeyCode.H))  // Prueba para bajar la salud
        {
            RecibirDanio(10f);
        }

        if (Input.GetKeyDown(KeyCode.R))  // Prueba para recargar munici�n
        {
            RecargarMunicion(5);
        }

        // Actualizar HUD (esto puede ir en Update si quieres que se actualice constantemente)
        ActualizarHUD();
    }

    // M�todo para recibir da�o
    public void RecibirDanio(float danio)
    {
        saludActual -= danio;
        if (saludActual <= 0) saludActual = 0;
    }

    // M�todo para recargar munici�n
    public void RecargarMunicion(int cantidad)
    {
        municionActual += cantidad;
        if (municionActual >= municionMaxima) municionActual = municionMaxima;
    }

    // Actualizamos el HUD con los valores actuales
    private void ActualizarHUD()
    {
        saludTexto.text = "Salud: " + saludActual.ToString("F0");
        municionTexto.text = "Munici�n: " + municionActual.ToString();
    }
}
