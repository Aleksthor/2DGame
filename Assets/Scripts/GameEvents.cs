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

    public event Action<GameObject, float, float, float, float, Vector2, bool> OnWeaponCollission;
    public void WeaponCollission(GameObject gameObject, float damage, float knockbackForce, float speedMultiplier, float slowDownLength, Vector2 playerPosition, bool crit)
    {
        if (OnWeaponCollission != null)
        {
            OnWeaponCollission(gameObject, damage, knockbackForce, speedMultiplier, slowDownLength, playerPosition, crit);
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


    public event Action<float, float, float, float, float, float, float, float, Vector2> OnChangeStats;
    public void ChangeStats(float damage, float knockbackForce, float speedMultiplier, float slowDownLength, float manaCost, float force, float critRate, float critDamage, Vector2 localPosition)
    {
        if (OnChangeStats != null)
        {
            OnChangeStats(damage, knockbackForce, speedMultiplier, slowDownLength, manaCost, force, critRate, critDamage, localPosition);
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

    public event Action<Equipment, int> OnRemoveCurrentEquipment;
    public void RemoveCurrentEquipment(Equipment equipment, int slotIndex)
    {
        if (OnRemoveCurrentEquipment != null)
        {
            OnRemoveCurrentEquipment(equipment, slotIndex);
        }
    }

    // Change Secondary Weapon in inventory on the player

    public event Action<Weapon> OnChangeSecondaryWeapon;
    public void ChangeSecondaryWeapon(Weapon weapon)
    {
        if (OnChangeSecondaryWeapon != null)
        {
            OnChangeSecondaryWeapon(weapon);
        }
    }

    public event Action<Equipment, int> OnChangeCurrentEquipment;
    public void ChangeCurrentEquipment(Equipment equipment, int slotIndex)
    {
        if (OnChangeCurrentEquipment != null)
        {
            OnChangeCurrentEquipment(equipment, slotIndex);
        }
    }

    // Swap sprites in inventory when we swap weapons

    public event Action OnSwapWeapon;
    public void SwapWeapon()
    {
        if (OnSwapWeapon != null)
        {
            OnSwapWeapon();
        }
    }

    // Add Item To Inventory

    public event Action<Item> OnAddItem;
    public void AddItem(Item item)
    {
        if (OnAddItem != null)
        {
            OnAddItem(item);
        }
    }

    // Remove Item from Inventory
    public event Action<Item> OnRemoveItem;
    public void RemoveItem(Item item)
    {
        if (OnRemoveItem != null)
        {
            OnRemoveItem(item);
        }
    }
    // Remove Current Secondary Item
    public event Action<Weapon> OnRemoveCurrentSecondaryItem;
    public void RemoveCurrentSecondaryItem(Weapon weapon)
    {
        if (OnRemoveCurrentSecondaryItem != null)
        {
            OnRemoveCurrentSecondaryItem(weapon);
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



    // Change Player Sprites
    public event Action<Sprite, Sprite, Sprite, Sprite, Sprite, Sprite, Sprite, Sprite, Sprite, Sprite, Color, Color, Color, Color, Color, Color> OnPlayerSpriteChange;
    public void PlayerSpriteChange(Sprite head_top, Sprite head_bottom, Sprite head_ear, Sprite head_hand, Sprite head_hair, Sprite head_facialhair, Sprite head_eye, Sprite head_eyebrow, Sprite head_mouth, Sprite head_nose,
                                            Color headTop, Color hair, Color facialhair, Color eye, Color eyebrow, Color mouth)
    {
        if (OnPlayerSpriteChange != null)
        {
            OnPlayerSpriteChange(head_top, head_bottom, head_ear, head_hand, head_hair, head_facialhair, head_eye, head_eyebrow, head_mouth, head_nose,
                                    headTop, hair, facialhair, eye, eyebrow, mouth);

        }
    }

    // Update Armor Stat
    public event Action<float> OnUpdateArmor;
    public void UpdateArmorStat(float armor)
    {
        if (OnUpdateArmor != null)
        {
            OnUpdateArmor(armor);
        }
    }


}
