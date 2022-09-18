using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Consumable : Item 
{
    public string consumableName;
    public float cooldownTime;
    public float activeTime;

    public float hpHealing;
    public float manaHealing;



    [Header("Buff")]
    public bool hasBuff;
    public Sprite buffIcon;


    public virtual void Activate(GameObject parent) { }
    public virtual void Trigger(GameObject parent) { }
    public virtual void DeActivate(GameObject parent) { }
}
