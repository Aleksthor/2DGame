using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Slots")]
    [SerializeField] Weapon weapon1;
    [SerializeField] Weapon weapon2;
    [SerializeField] Weapon weapon3;

    public Weapon currentWeapon;

    [Header("Current Weapon Info")]
    public float damage = 1f;
    public float knockBackForce;

    [Header("Magic Info")]
    public float force;
    public float manaCost;

    [Header("Debuff Info")]
    public float speedMultiplier;
    public float slowDownLength;


    // Debug purposes in Update
    void Update()
    {

        if(Input.GetButton("1"))
        {
            ChangeWeapon(weapon1);
        }


        if (Input.GetButton("2"))
        {
            ChangeWeapon(weapon2);
        }

        if (Input.GetButton("3"))
        {
            ChangeWeapon(weapon3);
        }

    }



    public void ChangeWeapon(Weapon weapon)
    {
        GameEvents.current.ChangeWeapon(weapon);
        GameEvents.current.ChangeStats(weapon.damage, weapon.knockBackForce, weapon.speedMultiplier, weapon.slowDownLength, weapon.manaCost, weapon.force, weapon.localPosition);
        GameEvents.current.ChangeWeaponCollider(weapon.colliderPointX, weapon.colliderPointY, (int)weapon.weaponType);


        damage = weapon.damage;
        knockBackForce = weapon.knockBackForce;
        force = weapon.force;
        manaCost = weapon.manaCost;
        speedMultiplier = weapon.speedMultiplier;
        slowDownLength = weapon.slowDownLength;

        currentWeapon = weapon;
        
    }
}
