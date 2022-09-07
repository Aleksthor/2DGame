using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskShootOrb : Node
{
    private Transform transform;
    private Animator animator;


    public TaskShootOrb(Transform AgentTransform)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();


    }



    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t != null)
        {

            animator.SetTrigger("ShootOrb");

            state = NodeState.RUNNING;
            return state;
        }
        state = NodeState.FAILURE;
        return state;

    }
}
