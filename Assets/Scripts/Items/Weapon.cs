using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Weapon : Item
{

    public enum WeaponType
    {
        Club,
        Dagger,
        Sword,
        Staff
    };
    [Header("Weapon Variables")]
    public WeaponType weaponType;
    [Header("Weapon Collider Points")]
    public double[] colliderPointX;
    public double[] colliderPointY;




}


