using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPrincipal : MonoBehaviour
{
    [SerializeField] public GameObject balaPrefab;  // Prefab de la bala
    [SerializeField] public Transform puntoDeDisparo;  // Referencia al punto de disparo en la boca del arma
    [SerializeField] public float velocidadDeDisparo = 20f;  // Velocidad con la que la bala se disparar�
    [SerializeField] private float tiempoVidaBala = 3f;  // Tiempo en segundos antes de destruir la bala
    [SerializeField] private float fuerzaRetroceso = 0.08f;  // Fuerza del retroceso (ajustable)
    [SerializeField] private float duracionRetroceso = 0.03f;  // Duraci�n del retroceso (ajustable)
    [SerializeField] private Transform brazoPadre;  // Referencia al GameObject padre (brazos)

    private Vector3 posicionInicialBrazo;  // Posici�n inicial de los brazos
    private bool enRetroceso = false;  // Para controlar si el retroceso est� activo

    private void Start()
    {
        // Guardamos la posici�n inicial de los brazos
        posicionInicialBrazo = brazoPadre.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Detectamos clic izquierdo para disparar
        {
            Disparar();
        }

        // Si estamos en retroceso, movemos el brazo de vuelta a la posici�n inicial gradualmente
        if (enRetroceso)
        {
            brazoPadre.localPosition = Vector3.Lerp(brazoPadre.localPosition, posicionInicialBrazo, Time.deltaTime / duracionRetroceso);
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

            // Destruir la bala despu�s de un tiempo
            Destroy(bala, tiempoVidaBala);  // Destruye la bala despu�s de 'tiempoVidaBala' segundos

            // Activar retroceso
            StartCoroutine(Retroceso());
        }
    }

    // Coroutine para manejar el retroceso
    private IEnumerator Retroceso()
    {
        enRetroceso = true;

        // Mover los brazos hacia atr�s por la cantidad de 'fuerzaRetroceso'
        // Aplicamos el retroceso en la direcci�n local de los brazos, asegur�ndonos de que no sea afectado por la orientaci�n global.
        brazoPadre.localPosition -= brazoPadre.transform.localRotation * Vector3.forward * fuerzaRetroceso;

        // Esperar un poco antes de devolver el brazo a su posici�n inicial
        yield return new WaitForSeconds(duracionRetroceso);

        // Finalizamos el retroceso
        enRetroceso = false;
    }
}
