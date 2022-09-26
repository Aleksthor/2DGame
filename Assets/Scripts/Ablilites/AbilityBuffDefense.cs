using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AbilityBuffDefense : Ability
{
    [Header("This Ability")]
    public float defensebuff;
    public GameObject buffEffect;



    public override void Activate(GameObject parent)
    {
        Object.Instantiate(buffEffect, parent.transform);
        GameEvents.current.AbilityBuffDefense(defensebuff);

    }


    public override void DeActivate(GameObject parent)
    {
        GameEvents.current.AbilityRemoveBuffDefense(defensebuff);
    }


    public override void Trigger(GameObject parent)
    {
    }
}
