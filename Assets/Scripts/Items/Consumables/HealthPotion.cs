using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Consumable
{
    public override void Activate(GameObject parent)
    {

        PlayerManager.Instance.SetHealthValue(hpHealing);

    }


    public override void DeActivate(GameObject parent)
    {
        
    }


    public override void Trigger(GameObject parent)
    {
    }
}
