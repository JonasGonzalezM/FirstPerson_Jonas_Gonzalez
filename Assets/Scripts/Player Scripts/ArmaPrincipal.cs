using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;  // Asegúrate de agregar esta línea para usar VisualEffect

public class ArmaPrincipal : MonoBehaviour
{
    [SerializeField] public GameObject balaPrefab;  // Prefab de la bala
    [SerializeField] public Transform puntoDeDisparo;  // Referencia al punto de disparo en la boca del arma
    [SerializeField] public float velocidadDeDisparo = 20f;  // Velocidad con la que la bala se disparará
    [SerializeField] private float tiempoVidaBala = 3f;  // Tiempo en segundos antes de destruir la bala
    [SerializeField] private float fuerzaRetroceso = 0.08f;  // Fuerza del retroceso (ajustable)
    [SerializeField] private float duracionRetroceso = 0.03f;  // Duración del retroceso (ajustable)
    [SerializeField] private Transform brazoPadre;  // Referencia al GameObject padre (brazos)

    private Vector3 posicionInicialBrazo;  // Posición inicial de los brazos
    private bool enRetroceso = false;  // Para controlar si el retroceso está activo

    [SerializeField] public int maxCargador = 35;  // Número máximo de balas en el cargador
    public int municionActual;  // Número actual de balas en el cargador
    private bool puedeDisparar = true;  // Para controlar el disparo rápido

    [SerializeField] private float tiempoEntreDisparos = 0.1f;  // Tiempo entre disparos consecutivos (disparo rápido)

    [SerializeField] private AudioSource audioSource;  // Referencia al AudioSource del arma
    [SerializeField] private AudioClip sonidoDisparo;  // El sonido que se reproducirá al disparar

    [SerializeField] private float tiempoDeRecarga = 2f;  // Tiempo de recarga (en segundos)
    private bool recargando = false;  // Estado para saber si se está recargando

    [SerializeField] private VisualEffect vfxDisparo;  // El VFX que queremos activar/desactivar

    private void Start()
    {
        municionActual = maxCargador;
        posicionInicialBrazo = brazoPadre.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && puedeDisparar && municionActual > 0 && !recargando)
        {
            Disparar();
        }
        else
        {
            // No hacer nada con el VFX si no estamos disparando
        }

        if (Input.GetKeyDown(KeyCode.R) && !recargando && municionActual < maxCargador)
        {
            Recargar();
        }

        if (enRetroceso)
        {
            brazoPadre.localPosition = Vector3.Lerp(brazoPadre.localPosition, posicionInicialBrazo, Time.deltaTime / duracionRetroceso);
        }
    }

    void Disparar()
    {
        if (balaPrefab != null && puntoDeDisparo != null && municionActual > 0)
        {
            GameObject bala = Instantiate(balaPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);

            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.velocity = transform.forward * velocidadDeDisparo;
            }

            Destroy(bala, tiempoVidaBala);

            municionActual--;
            StartCoroutine(Retroceso());
            ReproducirSonidoDisparo();

            // Usar la corutina para activar y desactivar el VFX
            StartCoroutine(ActivarYDesactivarVFX());

            StartCoroutine(ControlarDisparoRapido());
        }
        else if (municionActual <= 0 && !recargando)
        {
            Debug.Log("¡Recarga! No hay más balas.");
        }
    }

    private void ReproducirSonidoDisparo()
    {
        if (audioSource != null && sonidoDisparo != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(sonidoDisparo);
        }
    }

    // Corutina para activar y desactivar el VFX
    private IEnumerator ActivarYDesactivarVFX()
    {
        if (vfxDisparo != null)
        {
            vfxDisparo.Play(); // Activar el VFX
            yield return new WaitForSeconds(0.1f); // Esperar un breve momento (ajusta este tiempo según sea necesario)
            vfxDisparo.Stop(); // Desactivar el VFX
        }
    }

    private IEnumerator Retroceso()
    {
        enRetroceso = true;
        brazoPadre.localPosition -= brazoPadre.transform.localRotation * Vector3.forward * fuerzaRetroceso;
        yield return new WaitForSeconds(duracionRetroceso);
        enRetroceso = false;
    }

    private IEnumerator ControlarDisparoRapido()
    {
        puedeDisparar = false;
        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true;
    }

    private void Recargar()
    {
        if (municionActual < maxCargador && !recargando)
        {
            recargando = true;
            Debug.Log("Recargando...");
            StartCoroutine(RecargaCoroutine());
        }
    }

    private IEnumerator RecargaCoroutine()
    {
        yield return new WaitForSeconds(tiempoDeRecarga);
        municionActual = maxCargador;
        recargando = false;
        Debug.Log("Recarga completada. Munición llena.");
    }
}
