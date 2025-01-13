using System.Collections;
using UnityEngine;

public class TorretaPesada : MonoBehaviour
{
    [Header("Atributos de la torreta")]
    [SerializeField] private GameObject balaPrefab; // Prefab del proyectil
    [SerializeField] private Transform puntoDisparo; // Punto de origen de los disparos (GameEmpty)
    [SerializeField] private GameObject player; // Referencia al jugador
    [SerializeField] private float velocidadDisparo = 20f; // Velocidad del proyectil
    [SerializeField] private int disparosPorR�faga = 6; // Cantidad de disparos en una r�faga
    [SerializeField] private float intervaloDisparos = 0.1f; // Tiempo entre disparos en una r�faga
    [SerializeField] private float intervaloR�fagas = 2f; // Tiempo entre r�fagas
    [SerializeField] private GameObject fogonazoPrefab; // Prefab del objeto 3D para el fogonazo
    [SerializeField] private float vidaBala = 3f;
    [SerializeField] private float rangoDisparo = 290f; // Distancia m�nima para disparar
    private float velocidadGiro = 1500f;

    private bool puedeDisparar = true; // Controla si la torreta puede disparar

    void Update()
    {
        // Verificar la distancia entre la torreta y el jugador
        float distancia = Vector3.Distance(transform.position, player.transform.position);

        // Solo disparar si el jugador est� dentro del rango
        if (puedeDisparar && distancia <= rangoDisparo)
        {
            StartCoroutine(DispararR�faga());
        }

        RotarCanon();
    }

    private void RotarCanon()
    {
        transform.Rotate(Vector3.forward, velocidadGiro * Time.deltaTime);
    }

    private IEnumerator DispararR�faga()
    {
        puedeDisparar = false;

        for (int i = 0; i < disparosPorR�faga; i++)
        {
            DispararProyectil();
            ActivarEfectoDisparo(); // Activa el fogonazo 3D al disparar
            yield return new WaitForSeconds(intervaloDisparos);
        }

        yield return new WaitForSeconds(intervaloR�fagas);
        puedeDisparar = true;
    }

    private void DispararProyectil()
    {
        // Calcular direcci�n del disparo
        Vector3 direccion = (player.transform.position - puntoDisparo.position).normalized;

        // Instanciar la bala en el punto de disparo y orientarla en la direcci�n del disparo
        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.LookRotation(direccion));

        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direccion * velocidadDisparo;
        }

        // Destruir la bala despu�s de un tiempo
        Destroy(bala, vidaBala);
    }

    private void ActivarEfectoDisparo()
    {
        if (fogonazoPrefab != null)
        {
            // Calcular la direcci�n del disparo
            Vector3 direccion = (player.transform.position - puntoDisparo.position).normalized;

            // Instanciar el fogonazo 3D en el punto de disparo y orientarlo en la direcci�n del disparo
            GameObject fogonazo = Instantiate(fogonazoPrefab, puntoDisparo.position, Quaternion.LookRotation(direccion));

            // Opcional: Hacer que el fogonazo desaparezca despu�s de un corto tiempo
            Destroy(fogonazo, 0.1f);  // El fogonazo se destruye despu�s de 0.1 segundos
        }
    }
}
