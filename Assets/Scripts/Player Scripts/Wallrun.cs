using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun : MonoBehaviour
{
    private CharacterController controller; // Controlador del movimiento del personaje
    public float normalGravity = -9.81f; // Gravedad normal
    public float wallrunGravity = -2f; // Gravedad reducida durante el wallrun
    public float detectionRadius = 1.5f; // Radio de detección para paredes
    public float wallStickForce = 2f; // Fuerza que empuja al jugador hacia la pared
    public float wallJumpForce = 10f; // Fuerza del salto desde la pared
    public LayerMask wallLayer; // Capa de las paredes

    private Vector3 velocity; // Para manejar la gravedad y el movimiento vertical
    private bool isWallRunning; // Indica si el personaje está corriendo en la pared
    private bool wallOnLeft; // Indica si hay una pared a la izquierda
    private bool wallOnRight; // Indica si hay una pared a la derecha

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Detectar paredes cercanas usando una esfera
        wallOnLeft = Physics.CheckSphere(transform.position - transform.right * detectionRadius, detectionRadius, wallLayer);
        wallOnRight = Physics.CheckSphere(transform.position + transform.right * detectionRadius, detectionRadius, wallLayer);

        // Si detecta una pared a cualquiera de los lados, inicia el Wallrun
        if (wallOnLeft || wallOnRight)
        {
            isWallRunning = true;

            // Gravedad reducida durante el Wallrun
            velocity.y = Mathf.Clamp(velocity.y + wallrunGravity * Time.deltaTime, wallrunGravity, 0f);

            // Aplicar una ligera fuerza hacia la pared
            if (wallOnLeft)
            {
                controller.Move(-transform.right * wallStickForce * Time.deltaTime);
            }
            else if (wallOnRight)
            {
                controller.Move(transform.right * wallStickForce * Time.deltaTime);
            }

            // Salto desde la pared
            if (Input.GetButtonDown("Jump"))
            {
                Vector3 jumpDirection = Vector3.up;

                if (wallOnLeft)
                {
                    jumpDirection += transform.right; // Saltar hacia la derecha
                }
                else if (wallOnRight)
                {
                    jumpDirection -= transform.right; // Saltar hacia la izquierda
                }

                velocity.y = wallJumpForce;
                controller.Move(jumpDirection * wallJumpForce * Time.deltaTime);
            }
        }
        else
        {
            isWallRunning = false;
            velocity.y += normalGravity * Time.deltaTime; // Gravedad normal
        }

        // Aplicar el movimiento al CharacterController
        controller.Move(velocity * Time.deltaTime);

        // Resetear la velocidad vertical al estar en el suelo
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pequeña fuerza para mantener al personaje pegado al suelo
        }
    }

    void OnDrawGizmos()
    {
        // Visualizar las esferas para la detección de paredes
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position - transform.right * detectionRadius, detectionRadius); // Esfera izquierda
        Gizmos.DrawWireSphere(transform.position + transform.right * detectionRadius, detectionRadius); // Esfera derecha
    }
}
