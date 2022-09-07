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
    private TaskAttack taskAttack;

    public TaskCheckIfPlayerInAttackRange(Transform AgentTransform, Transform PlayerTransform, float AttackRange)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        attackRange = AttackRange;

    }

    public override NodeState Evaluate()
    {
        if(taskAttack == null)
        {
            taskAttack = (TaskAttack)parent.GetChild(1);

        }

        if (taskAttack != null)
        {
            if (taskAttack.attack)
            {

                taskAttack.attackClock += Time.deltaTime;
                if (taskAttack.attackClock > taskAttack.attackSpeed)
                {
                    taskAttack.attack = false;
                    taskAttack.attackClock = 0f;
                }

            }
        }

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
