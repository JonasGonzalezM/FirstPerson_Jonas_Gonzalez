using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaTorretaPesada : MonoBehaviour
{
    public float vidaActual;
    public float danoBala;

    public void DanoRecibido(float danoBala)
    {
        vidaActual -= danoBala;



        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("El objeto ha muerto");
        Destroy(gameObject); //Destruir el objeto padre


    }
}
