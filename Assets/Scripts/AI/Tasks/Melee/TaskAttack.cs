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
        GoblinSpriteDirection gsp;
        if (transform.GetComponent<GoblinSpriteDirection>() != null)
        {
            gsp = transform.GetComponent<GoblinSpriteDirection>();
            if (playerTransform.position.x - transform.position.x < 0)
            {
                gsp.Flip(false);
            }
            else
            {
                gsp.Flip(true);
            }
        }
        EnemySpriteManager esm;
        if (transform.GetComponent<EnemySpriteManager>() != null)
        {
            esm = transform.GetComponent<EnemySpriteManager>();
            if (playerTransform.position.x - transform.position.x < 0)
            {
                esm.Flip(false);
            }
            else
            {
                esm.Flip(true);
            }
        }







        state = NodeState.RUNNING;
        return state;

    }
   

    
}
