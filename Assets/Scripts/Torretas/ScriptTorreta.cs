using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTorreta : MonoBehaviour
{
    [SerializeField] public GameObject player;

    [SerializeField] private float velocidadGiro = 1f; // Velocidad de giro de la torreta

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        EnfocarObjetivo();
    }

    private void EnfocarObjetivo()
    {
        // Calculo vector unitario que mira hacia el player desde nuestra posici�n
        Vector3 direccionAObjetivo = (player.transform.position - transform.position).normalized;

        // Modifico la y del vector para prevenir que la torreta se incline
        direccionAObjetivo.y = 0;

        // Calculo la rotaci�n hacia la direcci�n deseada
        Quaternion rotacionAObjetivo = Quaternion.LookRotation(direccionAObjetivo);

        // Interpolo suavemente entre la rotaci�n actual y la deseada seg�n la velocidad
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionAObjetivo, velocidadGiro * Time.deltaTime);
    }

}
