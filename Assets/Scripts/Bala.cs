using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pared") || other.CompareTag("Suelo"))
        {
            Destroy(this.gameObject);
        }
    }
}
