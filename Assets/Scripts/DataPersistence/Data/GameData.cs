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

    public int currentLevel;
    public float currentXPAmount;
    public float neededXpToLevel;

    /**  Inventory  */
    public List<InventorySlot> itemInventory;
    public List<Weapon> weaponInventory;
    public List<Equipment> equipmentInventory;
    public List<Shield> shieldInventory;   
    public List<ConsumableInventorySlot> consumableInventory;

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

    /**  Active Consumable */
    public Consumable consumable1;
    public int consumableStackAmount1;
    public Consumable consumable2;
    public int consumableStackAmount2;
    public Consumable consumable3;
    public int consumableStackAmount3;

    /**   Bosses   */
    public bool goblinSmallBossDead;
    public bool goblinArcMageDead;

    public string currentScene;
    /** Trees */
    public List<TreeData> trees = new List<TreeData>();


    // What ever we pass into the constructor will be our initial values when we start a new game

    public GameData()
    {
        position = new Vector2(3, -20);
        goblinSmallBossDead = false;
        goblinArcMageDead = false;
        health = 50;
        maxHealth = 50;
        stamina = 100;
        maxStamina = 100;
        mana = 75;
        maxMana = 75;
        currentLevel = 1;
        currentXPAmount = 0;
        neededXpToLevel = 500f;
        currentScene = "FirstArena";

    }


}
