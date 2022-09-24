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
    public List<Item> inventory;


    public List<Item> currentWeapon;
    public List<Item> secondaryWeapon;
    public List<Item> currentShield;

    public List<Item> currentHead;
    public List<Item> currentChest;
    public List<Item> currentPants;
    public List<Item> currentShoes;

    public List<Item> currentNecklace;
    public List<Item> currentEarrings;
    public List<Item> currentRing1;
    public List<Item> currentRing2;



    /**   Bosses   */
    public bool goblinSmallBossDead;


    // What ever we pass into the constructor will be our initial values when we start a new game

    public GameData()
    {
        goblinSmallBossDead = false;
    }


}
