using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParteSuperiorTorreta : MonoBehaviour
{
    [SerializeField] public GameObject player;
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
        //Calculo vector UNITARIO que mira hacia el player desde nuestra posicion
        Vector3 direccionAObjetivo = (player.transform.position - transform.position).normalized;


        // Modifico la y del vector para prevenir que el enemigo SE TUMBE.
        //direccionAObjetivo.y = 0;
        // 2. Calculo la rotacion para conseguir dicha direccion
        Quaternion rotacionAObjetivo = Quaternion.LookRotation(direccionAObjetivo);
        transform.rotation = rotacionAObjetivo;



        //Transform.forward= direccionPlayer para la parte superior de la torreta
    }
}
