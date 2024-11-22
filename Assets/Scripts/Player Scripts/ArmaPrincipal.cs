using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPrincipal : MonoBehaviour
{
    [SerializeField] public GameObject balaPrefab;
    


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bala = Instantiate(balaPrefab, transform);
        }
    }
}
