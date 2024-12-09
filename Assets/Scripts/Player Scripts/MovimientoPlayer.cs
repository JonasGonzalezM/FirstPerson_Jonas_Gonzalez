using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    private CharacterController controller;
    public Transform cameraTransform; // La c�mara que controla la orientaci�n.
    public float velocidad = 12f;
    public float gravedad = -9.81f * 2;
    public float alturaSalto = 3f;
    public float distancia = 5f;
    public Transform playerTransform;  // El transform del jugador.
    public float sensibilidadRat�n = 2f;  // Sensibilidad para el movimiento del rat�n
    public float limiteRotacionVertical = 80f;  // L�mite para la rotaci�n vertical de la c�mara
    public float wallRunSpeed;
    

    public Transform checkSuelo;
    public float distanciaSuelo = 0.4f;
    public LayerMask mascaraSuelo;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 ultimaPosicion = new Vector3(0f, 0f, 0f);
    private float rotacionVertical = 0f;  // Para controlar la rotaci�n vertical de la c�mara

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // Bloquea el cursor en el centro de la pantalla
        Cursor.visible = false;  // Opcional: para esconder el cursor
    }
    public MovementState state;

    public enum MovementState
    {
        wallrunning,
    }
    public bool wallrunning;

    // Update is called once per frame
    void Update()
    {
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            velocidad = wallRunSpeed;
        }
        // Movimiento del rat�n para la rotaci�n horizontal
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadRat�n;

        // Rotaci�n horizontal del jugador (solo eje Y)
        playerTransform.Rotate(0f, mouseX, 0f);

        // Movimiento del rat�n para la rotaci�n vertical de la c�mara
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadRat�n;

        // Limitar la rotaci�n vertical de la c�mara
        rotacionVertical -= mouseY;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -limiteRotacionVertical, limiteRotacionVertical);

        // Aplicar la rotaci�n vertical solo a la c�mara, sin afectar al jugador
        cameraTransform.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);

        // Verificar si est� en el suelo
        isGrounded = Physics.CheckSphere(checkSuelo.position, distanciaSuelo, mascaraSuelo);

        // Reseteo de la velocidad cuando est� en el suelo
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movimiento del jugador (sin afectar a la rotaci�n)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Movimiento del jugador
        Vector3 movimiento = transform.right * h + transform.forward * v;
        controller.Move(movimiento * velocidad * Time.deltaTime);

        // Verificar si el jugador puede saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Realizar el salto
            velocity.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
        }

        // Aplicar la gravedad
        velocity.y += gravedad * Time.deltaTime;

        // Ejecutar la gravedad y el salto
        controller.Move(velocity * Time.deltaTime);

        // Comprobar si el jugador se est� moviendo
        if (ultimaPosicion != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        ultimaPosicion = gameObject.transform.position;
    }


    
}
