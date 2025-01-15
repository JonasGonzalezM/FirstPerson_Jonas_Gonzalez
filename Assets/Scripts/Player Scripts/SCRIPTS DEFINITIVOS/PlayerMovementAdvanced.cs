using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementAdvanced : MonoBehaviour
{
    // --- Variables para controlar el movimiento del jugador ---
    [Header("Movement")]
    private float moveSpeed; // Velocidad actual del jugador
    public float walkSpeed; // Velocidad al caminar
    public float sprintSpeed; // Velocidad al correr
    public float slideSpeed; // Velocidad al deslizarse
    private float desiredMoveSpeed; // Velocidad deseada según el estado
    private float lastDesiredMoveSpeed; // Última velocidad deseada
    public float wallrunSpeed; // Velocidad al correr por la pared

    public float groundDrag; // Fricción con el suelo

    // --- Variables para saltar ---
    [Header("Jumping")]
    public float jumpForce; // Fuerza del salto
    public float jumpCooldown; // Tiempo de espera entre saltos
    public float airMultiplier; // Multiplicador de movimiento en el aire
    bool readyToJump = true; // Indica si el jugador puede saltar

    // --- Variables para agacharse ---
    [Header("Crouching")]
    public float crouchSpeed; // Velocidad al agacharse
    public float crouchYScale; // Escala en Y al agacharse
    private float startYScale; // Escala inicial en Y

    // --- Teclas para acciones ---
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space; // Tecla para saltar
    public KeyCode sprintKey = KeyCode.LeftShift; // Tecla para correr
    public KeyCode crouchKey = KeyCode.LeftControl; // Tecla para agacharse

    // --- Comprobación de suelo ---
    [Header("Ground Check")]
    public float playerHeight; // Altura del jugador
    public LayerMask whatIsGround; // Capas que se consideran suelo
    bool grounded; // Indica si el jugador está tocando el suelo

    // --- Manejo de pendientes ---
    [Header("Slope Handling")]
    public float maxSlopeAngle; // Ángulo máximo de pendiente
    private RaycastHit slopeHit; // Información sobre la pendiente actual
    private bool exitingSlope; // Indica si el jugador está dejando una pendiente

    public Transform orientation; // Dirección de orientación del jugador

    float horizontalInput; // Entrada horizontal del jugador
    float verticalInput; // Entrada vertical del jugador

    Vector3 moveDirection; // Dirección del movimiento calculada

    Rigidbody rb; // Componente Rigidbody del jugador

    // --- Estados de movimiento del jugador ---
    public MovementState state; // Estado actual del jugador
    public enum MovementState
    {
        walking,
        sprinting,
        wallrunning,
        crouching,
        sliding,
        air
    }

    public bool sliding; // Indica si el jugador está deslizando
    public bool crouching; // Indica si el jugador está agachado
    public bool wallrunning; // Indica si el jugador está corriendo por la pared

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody
        rb.freezeRotation = true; // Evitar que el jugador rote por la física

        readyToJump = true; // Inicializar listo para saltar
        startYScale = transform.localScale.y; // Guardar la escala inicial en Y
    }

    private void Update()
    {
        // Comprobar si el jugador está tocando el suelo
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput(); // Procesar entradas del jugador
        SpeedControl(); // Controlar la velocidad según el estado
        StateHandler(); // Manejar el estado del jugador

        // Ajustar la fricción dependiendo si está en el suelo
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(); // Aplicar movimiento basado en la física
    }

    private void MyInput()
    {
        // Capturar entradas de movimiento
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Manejar el salto
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false; // Prevenir saltos múltiples
            Jump(); // Ejecutar el salto o llamar al Método de Salto
            Invoke(nameof(ResetJump), jumpCooldown); // Restablecer la capacidad de saltar después de un tiempo el cual es el JumpCoolDown
            //El operador "nameof" en C# se utiliza para obtener el nombre de una variable,
            //método, propiedad o clase como una cadena de texto. Es una forma segura y fácil de trabajar con nombres,
            //ya que está vinculada al código fuente y evita errores de escritura y si hay un cambio lo actualiza
            //y así se evitan errores de compilación 
        }

        // Manejar el inicio del agachado
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z); // Reducir la escala en Y
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // Aplicar fuerza hacia abajo
        }

        // Manejar el fin del agachado
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z); // Restaurar la escala original
        }
    }

    //Para hacer que se mueva rápido se puede hacer 2 cosas, que la velocidad entre ambos o
    //la diferencia entre 2 o mas movimientos sea menor a 4 para que el efecto sea inmediato
    // de lo contrario lo que pasará es que el movimiento crecerá y decrecerá de manera lenta
    // haciendo el efecto de conservar el momento o energia cinética.


    private void StateHandler()
    {
        // Estado: Corriendo por la pared
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;
        }
        // Estado: Deslizándose
        else if (sliding)
        {
            state = MovementState.sliding;

            // Aumentar velocidad al deslizarse en pendiente
            if (OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
            }
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }
        // Estado: Agachado
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }
        // Estado: Corriendo
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        // Estado: Caminando
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        // Estado: En el aire
        else
        {
            state = MovementState.air;
        }

        // Verificar cambios drásticos en la velocidad deseada
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed()); // Ajustar suavemente la velocidad
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed; // Actualizar velocidad deseada
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // Ajustar suavemente la velocidad deseada
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference); // Interpolación lineal
            time += Time.deltaTime;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed; // Asegurarse de alcanzar la velocidad deseada
    }

    private void MovePlayer()
    {
        // Calcular dirección del movimiento
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Movimiento en pendiente
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force); // Estabilizar en pendientes
        }
        // Movimiento en el suelo
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        // Movimiento en el aire
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        // Desactivar gravedad en pendientes
        if (!wallrunning)
        {
            rb.useGravity = !OnSlope();
        }
    }

    private void SpeedControl()
    {
        // Limitar velocidad en pendientes
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        // Limitar velocidad en suelo o aire
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true; // Indicar que estamos saliendo de una pendiente

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Resetear velocidad vertical
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); // Aplicar fuerza de salto
    }

    private void ResetJump()
    {
        readyToJump = true; // Permitir saltar nuevamente
        exitingSlope = false; // Reiniciar estado de pendiente
    }

    public bool OnSlope()
    {
        // Comprobar si estamos en una pendiente
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false; // No estamos en una pendiente
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        // Calcular dirección ajustada para pendientes
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}