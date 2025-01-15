using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParteSuperiorTorreta : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] private float velocidadGiro = 5f; // Velocidad de giro de la torreta

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        EnfocarObjetivo();
    }

    private void EnfocarObjetivo()
    {
        // Calculo vector unitario que mira hacia el player desde nuestra posición
        Vector3 direccionAObjetivo = (player.transform.position - transform.position).normalized;

        // Modifico la y del vector para prevenir que el enemigo se incline
        //direccionAObjetivo.y = 0;

        // Calculo la rotación para conseguir dicha dirección
        Quaternion rotacionAObjetivo = Quaternion.LookRotation(direccionAObjetivo);

        // Interpolo suavemente entre la rotación actual y la deseada
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionAObjetivo, velocidadGiro * Time.deltaTime);
    }
}
