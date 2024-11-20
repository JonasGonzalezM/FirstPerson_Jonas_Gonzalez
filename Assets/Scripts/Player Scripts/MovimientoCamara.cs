using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float sensiblilidadRaton = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float botClamp = 90f;


    // Start is called before the first frame update
    void Start()
    {
        //Centra el raton en la escena y lo esconde.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Añadimos los inputs del raton.
        float mouseX = Input.GetAxis("Mouse X") * sensiblilidadRaton*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensiblilidadRaton*Time.deltaTime;

        //Rotacion a través del eje X que será mirar arriba y abajo
        xRotation-= mouseY;

        //Clampear la rotacion
        xRotation = Mathf.Clamp(xRotation, topClamp, botClamp);


        //Rotacion a través del eje Y que será mirar de derecha a izquierda
        yRotation += mouseX;

        //Aplicar las rotaciones a nuestro transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
