using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class TaskShootShard : Node
{
    private Transform transform;
    private Animator animator;


    public TaskShootShard(Transform AgentTransform)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();


    }



    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t != null)
        {

            animator.SetTrigger("ShootShard");

            state = NodeState.RUNNING;
            return state;
        }
        state = NodeState.FAILURE;
        return state;

    }
}