using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment")]
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
    public float bonusDamage;

}
