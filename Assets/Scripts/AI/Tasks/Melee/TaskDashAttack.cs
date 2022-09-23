using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;
public class TaskDashAttack : Node
{
    private Transform transform;
    private Animator animator;
    private GameObject effect;

    private Transform playerTransform;

    public TaskDashAttack(Transform AgentTransform, Transform PlayerTransform, GameObject Effect)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        effect = Effect;
    }



    public override NodeState Evaluate()
    {

        
        Vector2 direction = transform.position - playerTransform.position;
        GameObject spawnedEffect = Object.Instantiate(effect, transform.position, transform.rotation);
        spawnedEffect.transform.right = direction;
        transform.position = (Vector2)playerTransform.position + (direction.normalized * 2f);


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
