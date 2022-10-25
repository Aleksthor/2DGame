using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInvisible : Ability
{
    public override void Activate(GameObject parent)
    {
        GameEvents.current.LowerPlayerOpacity();
        GameEvents.current.PlayerInvisible(30f);
    }


    public override void DeActivate(GameObject parent)
    {
        GameEvents.current.NormalPlayerOpacity();
        GameEvents.current.PlayerNotInvisible();
    }


    public override void Trigger(GameObject parent)
    {
    }
}
