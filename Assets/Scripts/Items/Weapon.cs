using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Weapon : Item
{
    public enum WeaponType
    {
        Club,
        Dagger,
        Sword
    };

    public WeaponType weaponType;

    public PolygonCollider2D weaponCollider;



}


