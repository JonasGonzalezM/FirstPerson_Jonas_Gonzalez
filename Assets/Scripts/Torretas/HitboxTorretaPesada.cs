using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxTorretaPesada : MonoBehaviour
{
    [SerializeField] private GameObject torreta;

    private void Start()
    {
        if (torreta == null)
        {
            torreta = transform.parent.gameObject;

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaPlayer"))
        {
            VidaTorretaPesada vidaScript = torreta.GetComponent<VidaTorretaPesada>();

            if (vidaScript != null)
            {
                vidaScript.DanoRecibido(10);
            }
            Destroy(other.gameObject);

        }
    }
}
