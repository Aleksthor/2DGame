using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{

    public enum ItemType
    {
        Weapon,
        Consumable,
        Material,
        Equipment,
        Shield

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
    public string spriteAtlasPath;
    public int spriteIndex;
    public bool isActive;
    public string itemName;
    public string itemDescription;

    public ItemType itemType;
    public ItemRarity itemRarity;
    public float itemWeight = 0f;

    public bool isStackable = false;

}

