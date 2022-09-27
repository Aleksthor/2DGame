using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{


    [Header("Buff")]
    public bool hasBuff;
    public Sprite buffIcon;

    public void Activate(float hp, float mana)
    {

        PlayerManager.Instance.SetHealthValue(hp);
        PlayerManager.Instance.SetHealthValue(mana);

    }


    public void DeActivate(GameObject parent)
    {
        
    }


    public void Trigger(GameObject parent)
    { 

    }
}
