using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] public PlayerHealth player; // Referencia al script del jugador para obtener salud
    [SerializeField] public ArmaPrincipal fusil;   // Referencia al script del fusil para obtener munici�n

    [Header("UI Elements")]
    public TextMeshProUGUI vidaTexto;      // Texto que muestra la vida del jugador
    public TextMeshProUGUI municionTexto;  // Texto que muestra la munici�n del fusil

    private void Update()
    {
        // Actualizar el texto de la vida del jugador
        if (vidaTexto != null && player != null)
        {
            vidaTexto.text = "Vida: " + ((int)player.maxHealth).ToString();
        }

        // Actualizar el texto de la munici�n del fusil
        if (municionTexto != null && fusil != null)
        {
            municionTexto.text = "Munici�n: " + fusil.municionActual + " / " + fusil.maxCargador;
        }
    }

}
