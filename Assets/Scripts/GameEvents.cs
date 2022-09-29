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
        if (current != null && current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
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

    // use mana
    public event Action<float> OnUseMana;
    public void UseMana(float stamina)
    {
        if (OnUseMana != null)
        {
            OnUseMana(stamina);
        }
    }

    public event Action<GameObject> OnDestroyObject;
    public void DestroyObject(GameObject gameObject)
    {
        if (OnDestroyObject != null)
        {
            OnDestroyObject(gameObject);
        }
    }

    

    // When our weapon hits something

    public event Action<GameObject, float, float, float, float, Vector2, bool, WeaponCollider.DamageType, int> OnWeaponCollission;
    public void WeaponCollission(GameObject gameObject, float damage, float knockbackForce, float speedMultiplier, float slowDownLength, Vector2 playerPosition, bool crit, WeaponCollider.DamageType damageType, int poise)
    {
        if (OnWeaponCollission != null)
        {
            OnWeaponCollission(gameObject, damage, knockbackForce, speedMultiplier, slowDownLength, playerPosition, crit, damageType, poise);
        }
    }


    // When the enemy hits us

    public event Action<float, float, WeaponCollider.DamageType> OnEnemyWeaponCollission;
    public void EnemyWeaponCollission(float damage, float knockbackForce, WeaponCollider.DamageType damageType)
    {
        if (OnEnemyWeaponCollission != null)
        {
            OnEnemyWeaponCollission(damage, knockbackForce, damageType);
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


    public event Action OnShowShield;
    public void ShowShield()
    {
        if (OnShowShield != null)
        {
            OnShowShield();
        }

    }


    public event Action OnHideShield;
    public void HideShield()
    {
        if (OnHideShield != null)
        {
            OnHideShield();
        }

    }


    // When we want to change the stats in player. Make a similar event if you cant pass in all variables


    public event Action<float, float, float, float, float, float, float, float, float, Vector2, int > OnChangeStats;
    public void ChangeStats(float damage, float magicFamage, float knockbackForce, float speedMultiplier, float slowDownLength, float manaCost, float force, float critRate, float critDamage, Vector2 localPosition, int poise)
    {
        if (OnChangeStats != null)
        {
            OnChangeStats(damage, magicFamage, knockbackForce, speedMultiplier, slowDownLength, manaCost, force, critRate, critDamage, localPosition, poise);
        }
    }

    public event Action<Weapon> OnUpdateSecondaryWeapon;
    public void UpdateSecondaryWeapon(Weapon weapon)
    {
        if(OnUpdateSecondaryWeapon != null)
        {
            OnUpdateSecondaryWeapon(weapon);
        }
    }

    public event Action<float, float, float, float, float, float, float, float, float, Vector2, int > OnUpdateInventoryStats;
    public void UpdateInventoryStats(float damage, float magicFamage, float knockbackForce, float speedMultiplier, float slowDownLength, float manaCost, float force, float critRate, float critDamage, Vector2 localPosition, int poise)
    {
        if (OnUpdateInventoryStats != null)
        {
            OnUpdateInventoryStats(damage, magicFamage, knockbackForce, speedMultiplier, slowDownLength, manaCost, force, critRate, critDamage, localPosition, poise);
        }
    }





    #endregion


    public event Action<float> OnAbilityBuffDefense;
    public void AbilityBuffDefense(float defenseBoost)
    {
        if (OnAbilityBuffDefense != null)
        {
            OnAbilityBuffDefense(defenseBoost);
        }
    }

    public event Action<float> OnAbilityRemoveBuffDefense;
    public void AbilityRemoveBuffDefense(float defenseBoost)
    {
        if (OnAbilityRemoveBuffDefense != null)
        {
            OnAbilityRemoveBuffDefense(defenseBoost);
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

    public event Action<Weapon.AbilityType, Weapon.AbilityType, Weapon.AbilityType> OnChangeWeaponAbility;
    public void ChangeWeaponAbility(Weapon.AbilityType ability1, Weapon.AbilityType ability2, Weapon.AbilityType ability3) 
    {
        if (OnChangeWeaponAbility != null)
        {
            OnChangeWeaponAbility(ability1, ability2, ability3);
        }
    }

    // Set Agro lets the enemy see the player again

    public event Action<GameObject> OnSetAgro;
    public void SetAgro(GameObject gameObject)
    {
        if (OnSetAgro != null)
        {
            OnSetAgro(gameObject);
        }
    }

    // Remove agro makes the player invisible to all enemies with the task (

    public event Action<GameObject> OnRemoveAgro;
    public void RemoveAgro(GameObject gameObject)
    {
        if (OnRemoveAgro != null)
        {
            OnRemoveAgro(gameObject);
        }
    }


    public event Action OnCharacterCreation;
    public void CharacterCreation()
    {
        if (OnCharacterCreation != null)
        {
            OnCharacterCreation();
        }
    }


    public event Action OnCharacterCreationEnd;
    public void CharacterCreationOver()
    {
        if (OnCharacterCreationEnd != null)
        {
            OnCharacterCreationEnd();
        }
    }


    public event Action<float, float> OnBuffDefense;
    public void BuffDefense(float defenseBoost, float time)
    {
        if (OnBuffDefense != null)
        {
            OnBuffDefense(defenseBoost, time);
        }
    }



    public event Action<Weapon> OnShowSecondary;
    public void ShowSecondary(Weapon weapon)
    {
        if(OnShowSecondary != null)
        {
            OnShowSecondary(weapon);
        }
    }

    public event Action OnRemoveSecondary;
    public void RemoveSecondary()
    {
        if (OnRemoveSecondary != null)
        {
            OnRemoveSecondary();
        }
    }


    public event Action OnSpawnCurrentEquipment;
    public void SpawnCurrentEquipment()
    {
        if (OnSpawnCurrentEquipment != null)
        {
            OnSpawnCurrentEquipment();
        }
    }

    public event Action<WeaponCollider.DamageType> OnUpdateDamageType;
    public void UpdateDamageType(WeaponCollider.DamageType damageType)
    {
        if (OnUpdateDamageType != null)
        {
            OnUpdateDamageType(damageType);
        }
    }

    //
}
