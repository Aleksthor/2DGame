using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;
public class TaskDashAttack : Node
{
    private Transform transform;
    private Animator animator;
    public GameObject effect;

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
        animator.SetTrigger("Charge");








        state = NodeState.RUNNING;
        return state;

    }
}
