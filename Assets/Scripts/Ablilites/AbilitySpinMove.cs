using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilitySpinMove : Ability
{




    public override void Activate(GameObject parent)
    {

        Animator animator = parent.GetComponent<Animator>();

        animator.SetTrigger("SpinMove");

    }


    public override void DeActivate(GameObject parent)
    {

    }


    public override void Trigger(GameObject parent)
    {
    }
}
