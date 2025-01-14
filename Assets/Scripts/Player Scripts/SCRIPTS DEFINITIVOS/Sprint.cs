using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    [SerializeField] public PlayerMovementAdvanced player;
    [SerializeField] public float velocidadSprint = 45f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.walkSpeed = velocidadSprint;
        }
        else
        {
           
        }
    }

}
