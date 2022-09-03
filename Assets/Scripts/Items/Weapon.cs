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

    public WeaponType weaponType;
    public double[] colliderPointX;
    public double[] colliderPointY;




}


