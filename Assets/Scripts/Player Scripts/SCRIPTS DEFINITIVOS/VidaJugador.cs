using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJugador : MonoBehaviour
{
    [Header("Atributos de Vida")]
    public float vidaJugador = 100f;
    private float vidaActual;


    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaJugador;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Morir();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balaenemiga"))
        {
            vidaActual -= 10; // Se quita puntos de vida 
            Destroy(other.gameObject); //Destruir la bala
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
            if (vidaActual <= 0)
            {
                Morir();
            }
        }


    }


    private void Morir()
    {
        if (vidaActual <= 0)
        {
            Time.timeScale = 0;
            //SceneManager.LoadScene(GameOver);




        }






    }


}
