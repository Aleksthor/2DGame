using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    // Start of a player attack 

    public event Action<float, float, bool, bool> OnPlayerAttack;
    public void PlayerAttackStart(float x, float y, bool attack, bool turn)
    {
        if(OnPlayerAttack != null)
        {
            OnPlayerAttack(x, y, attack, turn);
        }
    }

    // End of a player attack

    public event Action<bool, bool> EndPlayerAttack;
    public void PlayerAttackEnd(bool attack, bool turn)
    {
        if (EndPlayerAttack != null)
        {
            EndPlayerAttack(attack, turn);
        }
    }

    // When our weapon hits something

    public event Action<GameObject, float, float, float, float, Vector2> OnWeaponCollission;
    public void WeaponCollission(GameObject gameObject, float damage, float knockbackForce, float speedMultiplier, float slowDownLength, Vector2 playerPosition)
    {
        if (OnWeaponCollission != null)
        {
            OnWeaponCollission(gameObject, damage, knockbackForce, speedMultiplier, slowDownLength, playerPosition);
        }
    }


    // When the enemy hits us

    public event Action<float, float> OnEnemyWeaponCollission;
    public void EnemyWeaponCollission(float damage, float knockbackForce)
    {
        if (OnEnemyWeaponCollission != null)
        {
            OnEnemyWeaponCollission(damage, knockbackForce);
        }
    }


    // Change Weapon Sprite on the player


    public event Action<Weapon> OnChangeWeapon;
    public void ChangeWeapon(Weapon weapon)
    {
        if (OnChangeWeapon != null)
        {
            OnChangeWeapon(weapon);
        }
    }


    // When we want to change the stats in player. Make a similar event if you cant pass in all variables


    public event Action<float, float, float, float, float, float, Vector2> OnChangeStats;
    public void ChangeStats(float damage, float knockbackForce, float speedMultiplier, float slowDownLength, float manaCost, float force, Vector2 localPosition)
    {
        if (OnChangeStats != null)
        {
            OnChangeStats(damage, knockbackForce, speedMultiplier, slowDownLength, manaCost, force, localPosition);
        }
    }

    // Change the weapon Collider

    public event Action<double[], double[], int> OnChangeWeaponCollider;
    public void ChangeWeaponCollider(double[] x , double[] y, int weaponType)
    {
        if (OnChangeWeaponCollider != null)
        {
            OnChangeWeaponCollider(x, y, weaponType);
        }
    }








    // Change Current Weapon in inventory on the player


    public event Action<Weapon> OnChangeCurrentWeapon;
    public void ChangeCurrentWeapon(Weapon weapon)
    {
        if (OnChangeCurrentWeapon != null)
        {
            OnChangeCurrentWeapon(weapon);
        }
    }












    public event Action<Weapon> OnChangeSecondaryWeapon;
    public void ChangeSecondaryWeapon(Weapon weapon)
    {
        if (OnChangeSecondaryWeapon != null)
        {
            OnChangeSecondaryWeapon(weapon);
        }
    }

    public event Action OnSwapWeapon;
    public void SwapWeapon()
    {
        if (OnSwapWeapon != null)
        {
            OnSwapWeapon();
        }
    }


    public event Action<Weapon, Weapon> OnInventoryRefresh;
    public void InventoryRefresh(Weapon current, Weapon secondary)
    {
        if (OnInventoryRefresh != null)
        {
            OnInventoryRefresh(current, secondary);
        }
    }

    


}
