using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Fusil", menuName ="Weapon/Gun")]
public class DatoArmas : ScriptableObject
{
    [Header("Info")]
    public new string name;


    [Header("Shooting")]
    public float damage;
    public float maxDistance;


    [Header("Reloading")]
    public int nurrentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;










}
