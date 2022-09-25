using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
public class Item : ScriptableObject
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
    public string itemName;
    public string itemDescription;

    public ItemType itemType;
    public ItemRarity itemRarity;
    public float itemWeight = 0f;

    public bool isStackable = false;

}

