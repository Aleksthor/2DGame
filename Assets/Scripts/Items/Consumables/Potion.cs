using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Potion : Consumable
{
    public override void Activate(GameObject parent)
    {

        PlayerManager.Instance.SetHealthValue(hpHealing);
        PlayerManager.Instance.SetHealthValue(manaHealing);

    }


    public override void DeActivate(GameObject parent)
    {
        
    }


    public override void Trigger(GameObject parent)
    {
    }
}
