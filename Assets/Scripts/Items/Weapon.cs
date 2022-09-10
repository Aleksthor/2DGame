using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Weapon : Item
{

    public enum WeaponType
    {
        Blunt,
        Dagger,
        Sword,
        Staff, 
        Wand
    };


    [Header("Weapon Variables")]
    public WeaponType weaponType;
    public float damage;
    public float knockBackForce;

    [Header("Magic Variables")]
    public float force;
    public float manaCost;


    [Header("Weapon Collider Points")]
    public double[] colliderPointX;
    public double[] colliderPointY;




}


