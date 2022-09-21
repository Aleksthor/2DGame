using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTrees;

public class TaskCheckIfPlayerInAttackRange : Node
{
    private Transform transform;
    private Animator animator;
    private Transform playerTransform;
    private float attackRange;
    private TaskWait taskWait;
   

    public TaskCheckIfPlayerInAttackRange(Transform AgentTransform, Transform PlayerTransform, float AttackRange)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        attackRange = AttackRange;

    }
   

    public override NodeState Evaluate()
    {
        
        if (taskWait == null)
        {
            taskWait = (TaskWait)parent.GetChild(1);
        }
        
        if ((transform.position - playerTransform.position).magnitude < attackRange)
        {
            parent.parent.SetData("target", playerTransform);
            animator.SetBool("Walking", false);
            state = NodeState.SUCCESS;
            return state;
        }
        if (taskWait.waitClock < taskWait.waitTime)
        {
            
            taskWait.waitClock += Time.deltaTime;
        }

        state = NodeState.FAILURE;
        return state;

    }
}
