using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{

    private CharacterController controller;
    public float velocidad = 12f;
    public float gravedad = -9.81f * 2;
    public float alturaSalto = 3f;


    public Transform checkSuelo;
    public float distanciaSuelo = 0.4f;
    public LayerMask mascaraSuelo;

    Vector3 velocity;


    bool isGrounded;
    bool isMoving;

    private Vector3 ultimaPosicion= new Vector3 (0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        // ver si esta en el suelo
        isGrounded = Physics.CheckSphere(checkSuelo.position,distanciaSuelo,mascaraSuelo);

        //Reseteo de la velocidad
        if(isGrounded&& velocity.y < 0)
        {
            velocity.y = -2f;
        }


        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector3 move = transform.right * h + transform.forward * v; //Movimiento delate y atrás
    }
}
