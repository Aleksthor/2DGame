using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Transform transform;
    private Transform playerTransform;
    private Animator animator;
    private float attackSpeed;
    private float attackClock = 0f;
    private float attackRange;

    public TaskAttack(Transform AgentTransform, Transform PlayerTransform, float AttackRange, float AttackSpeed)
    {
        transform = AgentTransform; 
        animator = AgentTransform.GetComponent<Animator>();
        attackSpeed = AttackSpeed;
        attackClock = AttackSpeed;
        attackRange = AttackRange;
        playerTransform = PlayerTransform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t != null)
        {
            if ((transform.position - playerTransform.position).magnitude > attackRange)
            {
                attackClock = attackSpeed;
            }
            else
            {
                attackClock += Time.deltaTime;

                if (attackClock > attackSpeed && (transform.position - playerTransform.position).magnitude < attackRange)
                {
                    animator.SetTrigger("Attack");
                    attackClock = 0f;
                }


                state = NodeState.RUNNING;
                return state;
            }  

            state = NodeState.RUNNING;
            return state;
        }
        attackClock = attackSpeed;
        state = NodeState.FAILURE;
        return state;

    }
}
