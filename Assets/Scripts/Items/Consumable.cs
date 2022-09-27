using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Consumable : Item 
{
    public enum ConsumableType
    {
        Potion
    };

    public ConsumableType consumableType; 
    public string consumableName;
    public float cooldownTime;
    public float activeTime;

    public float hpHealing;
    public float manaHealing;


    


}
