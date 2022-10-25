using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;
public class TaskDashAttack : Node
{
    private Transform transform;
    private Animator animator;


    private Transform playerTransform;

    public TaskDashAttack(Transform AgentTransform, Transform PlayerTransform)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;

    }



    public override NodeState Evaluate()
    {

        animator.SetTrigger("Charge");


        state = NodeState.RUNNING;
        return state;

    }
}
