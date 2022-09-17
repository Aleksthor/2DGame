using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;
    public float manaCost;


    [Header("Buff")]
    public bool hasBuff;
    public Sprite buffIcon;


    public virtual void Activate(GameObject parent) { }

    public virtual void Trigger(GameObject parent) { }
    public virtual void DeActivate(GameObject parent) { }
}
