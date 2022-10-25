using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStrongShot : Ability
{
    public override void Activate(GameObject parent)
    {

        Animator animator = parent.GetComponent<Animator>();

        animator.SetTrigger("StrongShot");

    }


    public override void DeActivate(GameObject parent)
    {

    }


    public override void Trigger(GameObject parent)
    {
    }
}
