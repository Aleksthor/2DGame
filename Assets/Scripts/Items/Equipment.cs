using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : Item
{
       

    public enum EquipmentType
    {
        Head,
        Chest,
        Pants,
        Shoes,

        Necklace,
        Earring,
        Ring

    };

    public EquipmentType equipmentType;
    public float armor;
    public float bonusMeleeDamage;
    public float bonusMagicDamage;

}
