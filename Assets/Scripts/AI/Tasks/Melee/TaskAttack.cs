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
    public float attackRange;
    public float attackClock = 0f;
    public bool attack = false;

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

            if (!attack && (transform.position - playerTransform.position).magnitude < attackRange)
            {
                animator.SetTrigger("Attack");
                attackClock = 0f;
                attack = true;
                
                EnemySpriteDirection esp = transform.GetComponent<EnemySpriteDirection>();
                if(playerTransform.position.x - transform.position.x < 0)
                {
                    esp.Flip(false);
                }
                else
                {
                    esp.Flip(true);
                }

            }



            state = NodeState.RUNNING;
            return state;
        }
        attackClock = attackSpeed;
        state = NodeState.FAILURE;
        return state;

    }
}
