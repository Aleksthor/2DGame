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


    public event Action<Weapon> OnChangeWeapon;
    public void ChangeWeapon(Weapon weapon)
    {
        if (OnChangeWeapon != null)
        {
            OnChangeWeapon(weapon);
        }
    }

    public event Action<float, float> OnEnemyWeaponCollission;
    public void EnemyWeaponCollission(float damage, float knockbackForce)
    {
        if (OnEnemyWeaponCollission != null)
        {
            OnEnemyWeaponCollission(damage, knockbackForce);
        }
    }

}
