using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;


    // 

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

    // Use Stamina

    public event Action<float> OnUseStamina;
    public void UseStamina(float stamina)
    {
        if (OnUseStamina != null)
        {
            OnUseStamina(stamina);
        }
    }

    public event Action OnDestroyObject;
    public void DestroyObject()
    {
        if (OnDestroyObject != null)
        {
            OnDestroyObject();
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




    #region Inventory

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

    // Change Current Weapon in inventory on the player


    public event Action<Weapon> OnChangeCurrentWeapon;
    public void ChangeCurrentWeapon(Weapon weapon)
    {
        if (OnChangeCurrentWeapon != null)
        {
            OnChangeCurrentWeapon(weapon);
        }
    }

    // Change the weapon Collider

    public event Action<double[], double[], int> OnChangeWeaponCollider;
    public void ChangeWeaponCollider(double[] x, double[] y, int weaponType)
    {
        if (OnChangeWeaponCollider != null)
        {
            OnChangeWeaponCollider(x, y, weaponType);
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

    // Remove Current Secondary Item
    public event Action<Weapon> OnRemoveCurrentSecondaryItem;
    public void RemoveCurrentSecondaryItem(Weapon weapon)
    {
        if (OnRemoveCurrentSecondaryItem != null)
        {
            OnRemoveCurrentSecondaryItem(weapon);
        }
    }


    // Change Secondary Weapon in inventory on the player

    public event Action<Shield> OnChangeCurrentShield;
    public void ChangeCurrentShield(Shield shield)
    {
        if (OnChangeCurrentShield != null)
        {
            OnChangeCurrentShield(shield);
        }
    }

    // Remove Current Secondary Item
    public event Action<Shield> OnRemoveCurrentShield;
    public void RemoveCurrentShield(Shield shield)
    {
        if (OnRemoveCurrentShield != null)
        {
            OnRemoveCurrentShield(shield);
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

    public event Action<Equipment, int> OnRemoveCurrentEquipment;
    public void RemoveCurrentEquipment(Equipment equipment, int slotIndex)
    {
        if (OnRemoveCurrentEquipment != null)
        {
            OnRemoveCurrentEquipment(equipment, slotIndex);
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


    public event Action<Weapon, Weapon> OnInventoryRefresh;
    public void InventoryRefresh(Weapon current, Weapon secondary)
    {
        if (OnInventoryRefresh != null)
        {
            OnInventoryRefresh(current, secondary);
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

    // Change Shield Sprite on the player


    public event Action<Shield> OnChangeShield;
    public void ChangeShield(Shield shield)
    {
        if (OnChangeShield != null)
        {
            OnChangeShield(shield);
        }

    }


    // When we want to change the stats in player. Make a similar event if you cant pass in all variables


    public event Action<float, float, float, float, float, float, float, float, float, Vector2> OnChangeStats;
    public void ChangeStats(float damage, float magicFamage, float knockbackForce, float speedMultiplier, float slowDownLength, float manaCost, float force, float critRate, float critDamage, Vector2 localPosition)
    {
        if (OnChangeStats != null)
        {
            OnChangeStats(damage, magicFamage, knockbackForce, speedMultiplier, slowDownLength, manaCost, force, critRate, critDamage, localPosition);
        }
    }

    #endregion





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

    // Trigger in TaskAttack

    public event Action<Vector2> OnEnemyMeleeAttack;
    public void EnemyMeleeAttack(Vector2 position)
    {
        if (OnEnemyMeleeAttack != null)
        {
            OnEnemyMeleeAttack(position);
        }
    }



    // LowerPlayerOpacity

    public event Action OnLowerPlayerOpacity;
    public void LowerPlayerOpacity()
    {
        if (OnLowerPlayerOpacity != null)
        {
            OnLowerPlayerOpacity();
        }
    }

    public event Action OnNormalPlayerOpacity;
    public void NormalPlayerOpacity()
    {
        if (OnNormalPlayerOpacity != null)
        {
            OnNormalPlayerOpacity();
        }
    }

    // enemies cant see us

    public event Action OnPlayerInvisible;
    public void PlayerInvisible()
    {
        if (OnPlayerInvisible != null)
        {
            OnPlayerInvisible();
        }
    }

    public event Action OnPlayerNotInvisible;
    public void PlayerNotInvisible()
    {
        if (OnPlayerNotInvisible != null)
        {
            OnPlayerNotInvisible();
        }
    }



    public event Action<GameObject> OnEnemyStartAttack;
    public void EnemyStartAttack(GameObject gameObject)
    {
        if (OnEnemyStartAttack != null)
        {
            OnEnemyStartAttack(gameObject);
        }
    }

    public event Action<GameObject> OnEnemyStopAttack;
    public void EnemyStopAttack(GameObject gameObject)
    {
        if (OnEnemyStopAttack != null)
        {
            OnEnemyStopAttack(gameObject);
        }
    }


    // Attack Buff

    public event Action<float> OnBoostNextAttack;
    public void BoostNextAttack(float damageBoost)
    {
        if (OnBoostNextAttack != null)
        {
            OnBoostNextAttack(damageBoost);
        }
    }

    public event Action OnDontBoostNextAttack;
    public void DontBoostNextAttack()
    {
        if (OnDontBoostNextAttack != null)
        {
            OnDontBoostNextAttack();
        }
    }

    // Change Weapon Ability

    public event Action<Ability, Ability, Ability, Sprite, Sprite, Sprite> OnChangeWeaponAbility;
    public void ChangeWeaponAbility(Ability ability1, Ability ability2, Ability ability3, Sprite sprite1, Sprite sprite2, Sprite sprite3) 
    {
        if (OnChangeWeaponAbility != null)
        {
            OnChangeWeaponAbility(ability1, ability2, ability3, sprite1, sprite2, sprite3);
        }
    }

}
