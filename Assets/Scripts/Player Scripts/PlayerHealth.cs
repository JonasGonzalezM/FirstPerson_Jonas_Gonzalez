using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;


    private void Start()
    {
        health = maxHealth;

    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthUI();

        if(Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(Random.Range(5, 10));
        }


    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;

    }






}
