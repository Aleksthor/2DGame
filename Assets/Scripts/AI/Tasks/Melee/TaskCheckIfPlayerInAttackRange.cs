using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskCheckIfPlayerInAttackRange : Node
{
    private Transform transform;
    private Animator animator;
    private Transform playerTransform;
    private float attackRange;
   

    public TaskCheckIfPlayerInAttackRange(Transform AgentTransform, Transform PlayerTransform, float AttackRange)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        attackRange = AttackRange;

    }

    public override NodeState Evaluate()
    {
       

        if ((transform.position - playerTransform.position).magnitude < attackRange)
        {
            parent.parent.SetData("target", playerTransform);
            animator.SetBool("Walking", false);
            state = NodeState.SUCCESS;
            return state;
        }
        
        state = NodeState.FAILURE;
        return state;

    }
}
