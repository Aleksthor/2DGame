using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{

    /**   PLayer   */
    public Vector2 position;

    public float health;
    public float maxHealth;
    public float stamina;
    public float maxStamina;
    public float mana;
    public float maxMana;


    /**  Inventory  */
    public List<InventorySlot> inventory;


    public Weapon currentWeapon;
    public Weapon secondaryWeapon;
    public Shield currentShield;

    public Equipment currentHead;
    public Equipment currentChest;
    public Equipment currentPants;
    public Equipment currentShoes;

    public Equipment currentNecklace;
    public Equipment currentEarrings;
    public Equipment currentRing1;
    public Equipment currentRing2;



    /**   Bosses   */
    public bool goblinSmallBossDead;
    public bool goblinArcMageDead;


    // What ever we pass into the constructor will be our initial values when we start a new game

    public GameData()
    {
        goblinSmallBossDead = false;
        goblinArcMageDead = false;
        health = 50;
        maxHealth = 50;
        stamina = 100;
        maxStamina = 100;
        mana = 75;
        maxMana = 75;
    }


}
