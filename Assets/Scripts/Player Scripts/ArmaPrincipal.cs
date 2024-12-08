using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPrincipal : MonoBehaviour
{
    [SerializeField] public GameObject balaPrefab;  // Prefab de la bala
    [SerializeField] public Transform puntoDeDisparo;  // Referencia al punto de disparo en la boca del arma
    [SerializeField] public float velocidadDeDisparo = 20f;  // Velocidad con la que la bala se disparar�
    [SerializeField] private float tiempoVidaBala = 5f;  // Tiempo en segundos antes de destruir la bala

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Detectamos clic izquierdo para disparar
        {
            Disparar();
        }
    }

    void Disparar()
    {
        if (balaPrefab != null && puntoDeDisparo != null)
        {
            // Instanciamos la bala en el punto de disparo con la misma rotaci�n que el puntoDeDisparo
            GameObject bala = Instantiate(balaPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);

            // Obtenemos el Rigidbody de la bala (debe tener Rigidbody para poder moverla)
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aseguramos que la gravedad est� desactivada para no interferir con el movimiento
                rb.useGravity = false;

                // Aplicamos una velocidad hacia adelante
                rb.velocity = transform.forward * velocidadDeDisparo;  // Usamos la orientaci�n global del arma
            }

            // Llamamos a la funci�n que destruye la bala despu�s de un tiempo
            Destroy(bala, tiempoVidaBala);  // Destruye la bala despu�s de 'tiempoVidaBala' segundos
        }
    }
}
