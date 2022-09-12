using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{

    public enum ItemType
    {
        Weapon,
        Consumable,
        Material

    };

    public enum ItemRarity
    {
        Common,
        UnCommon,
        Rare,
        Epic,
        Legendary

    };

    [Header("Item Variables")]
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;

    public ItemType itemType;
    public ItemRarity itemRarity;
    public float itemWeight = 0f;

    public bool isStackable = false;
    public int stackAmount = 1;
}

