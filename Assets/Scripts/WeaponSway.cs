using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponSway : MonoBehaviour
{
    [Header("Ajustes Movimiento")]
    [SerializeField] private float suave;
    [SerializeField] private float swayMultiplicador;

    private void FixedUpdate()
    {
        // obtener el movimiento del raton
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplicador;
        float mouseY=Input.GetAxisRaw("Mouse Y")* swayMultiplicador;

        // calcular la rotacion objetivo
         Quaternion rotationX=Quaternion.AngleAxis(-mouseY,Vector3.right);

    }
}
