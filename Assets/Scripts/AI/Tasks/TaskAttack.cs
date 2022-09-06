using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Transform transform;
    private Transform playerTransform;
    private Animator animator;
    public float attackSpeed;
    public float attackClock = 0f;
    private float attackRange;
    private bool attack = false;

    public TaskAttack(Transform AgentTransform, Transform PlayerTransform, float AttackRange, float AttackSpeed)
    {
        transform = AgentTransform; 
        animator = AgentTransform.GetComponent<Animator>();
        attackSpeed = AttackSpeed;
        attackClock = AttackSpeed;
        attackRange = AttackRange;
        playerTransform = PlayerTransform;
    }

    void FixedUpdate()
    {
        if (attack)
        {
            attackClock += Time.fixedDeltaTime;
            if (attackClock > attackSpeed)
            {
                attack = false;
                attackClock = 0f;
            }
        }
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t != null)
        {



            if (!attack && (transform.position - playerTransform.position).magnitude < attackRange)
            {
                animator.SetTrigger("Attack");
                attackClock = 0f;
                attack = true;
            }



            state = NodeState.RUNNING;
            return state;
        }
        attackClock = attackSpeed;
        state = NodeState.FAILURE;
        return state;

    }
}
