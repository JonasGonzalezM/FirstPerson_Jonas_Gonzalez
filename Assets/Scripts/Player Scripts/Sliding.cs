using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    private CharacterController controller;  // Referencia al CharacterController
    public float slideSpeed = 30f;  // Velocidad del deslizamiento
    public float slideDuration = 1f;  // Duraci�n del deslizamiento
    private float slideTimer;  // Temporizador para controlar la duraci�n del deslizamiento
    private bool isSliding = false;  // Estado para saber si est� desliz�ndose o no

    // Par�metros para controlar la altura durante el deslizamiento
    public float crouchHeight = 0.3f;  // Altura durante el deslizamiento (ajustada)
    public float standingHeight = 2f;  // Altura normal cuando est� de pie

    private float originalHeight;  // Guardar la altura original del CharacterController
    private Vector3 originalCenter;  // Guardar el centro original del CharacterController

    public Transform playerTransform;  // Transform para mover al jugador
    public Transform groundCheck;  // Punto para verificar si est� tocando el suelo
    public float groundDistance = 0.4f;  // Distancia para verificar si est� en el suelo
    public LayerMask groundMask;  // Capa de suelo

    private bool isGrounded;  // Estado para saber si el jugador est� tocando el suelo

    // Start se llama al principio del juego
    void Start()
    {
        controller = GetComponent<CharacterController>();  // Obtiene el CharacterController del jugador
        originalHeight = controller.height;  // Guarda la altura original del CharacterController
        originalCenter = controller.center;  // Guarda el centro original del CharacterController
        controller.skinWidth = 0.01f;  // Reducir el tama�o de la "piel" para evitar problemas con la colisi�n
    }

    // Update se llama cada fotograma
    void Update()
    {
        // Verifica si el jugador est� tocando el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Inicia el deslizamiento si el jugador presiona la tecla Ctrl y est� en el suelo
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded && !isSliding)
        {
            StartSlide();
        }

        // Si est� desliz�ndose, mueve al jugador hacia adelante
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;  // Reduce el temporizador

            // Si el temporizador llega a 0, termina el deslizamiento
            if (slideTimer <= 0f)
            {
                EndSlide();
            }
            else
            {
                // Aqu� obtenemos la direcci�n de movimiento, usando las entradas del teclado
                float horizontal = Input.GetAxis("Horizontal");  // Movimiento en el eje X (Izquierda/Derecha)
                float vertical = Input.GetAxis("Vertical");  // Movimiento en el eje Z (Adelante/Atr�s)

                // Calculamos la direcci�n de movimiento en base a la orientaci�n del jugador
                Vector3 moveDirection = (playerTransform.right * horizontal + playerTransform.forward * vertical).normalized;

                // Mueve al jugador en la direcci�n de movimiento calculada durante el deslizamiento
                controller.Move(moveDirection * slideSpeed * Time.deltaTime);
            }
        }
    }

    // Funci�n para iniciar el deslizamiento
    private void StartSlide()
    {
        isSliding = true;  // Marca que el jugador est� desliz�ndose
        slideTimer = slideDuration;  // Inicializa el temporizador del deslizamiento

        // Reduce la altura del CharacterController para simular que el jugador se agacha
        controller.height = crouchHeight;
        controller.center = new Vector3(0f, crouchHeight / 2f, 0f);  // Aseguramos que el centro tambi�n se ajusta
    }

    // Funci�n para terminar el deslizamiento
    private void EndSlide()
    {
        isSliding = false;  // Marca que el jugador ha dejado de deslizarse

        // Restaura la altura original del CharacterController despu�s de un peque�o retraso
        controller.height = originalHeight;
        controller.center = originalCenter;  // Restaura el centro original
    }
}
