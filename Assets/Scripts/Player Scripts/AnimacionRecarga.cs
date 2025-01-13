using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionRecarga : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] public ArmaPrincipal fusil;


    private bool estaRecargando =false; // Para verificar si ya estamos en recarga


    // Start is called before the first frame update
    void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobar si el jugador presiona la R y no se esta ejecutando la animacion y que el arma pueda
        //recargar siempre y cuando la municion sea inferior a 35 que son las balas que tiene el
        //fusil, en el caso de que el fusil tenga 35 en teoría no podria recargar
        if(Input.GetKeyDown(KeyCode.R)&& !estaRecargando && fusil.municionActual>= 35f)
        {

            IniciarRecarga();
        }
    }

    //Funcion para iniciar a animacion de recarga
    private void IniciarRecarga()
    {
        estaRecargando=true;

        //Activamos el trigger para reproducir la animacion de recarga
        animator.SetTrigger("Recargar");

        //Llamamos a la corrutina para gestionar el tiempo de recarga
        StartCoroutine(Recarga());
    }

    //Esta Corrutina es para manejar el tiempo de la recarga
    private IEnumerator Recarga()
    {
        //Duracion de la animacion de la recarga
        yield return new WaitForSeconds(2f);

        //La recarga ha terminado 
        estaRecargando = false;
    }
}
