using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Transform transform;
    private Animator animator;

    private Transform playerTransform;

    public TaskAttack(Transform AgentTransform, Transform PlayerTransform)
    {
        transform = AgentTransform; 
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
    }



    public override NodeState Evaluate()
    {



        animator.SetTrigger("Attack");
        GameEvents.current.EnemyMeleeAttack((Vector2)playerTransform.position);
        GoblinSpriteDirection esp = transform.GetComponent<GoblinSpriteDirection>();
        if (playerTransform.position.x - transform.position.x < 0)
        {
            esp.Flip(false);
        }
        else
        {
            esp.Flip(true);
        }





        state = NodeState.RUNNING;
        return state;

    }
   

    
}
