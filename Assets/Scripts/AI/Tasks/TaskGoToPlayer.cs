using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToPlayer : Node
{
    private Transform transform;
    private Transform playerTransform;
    private Animator animator;
    private float walkSpeed;


    public float speedMultiplier = 1f;    
    LocalEnemyScript localEnemyScript;
   

    public TaskGoToPlayer(Transform AgentTransform, Transform PlayerTransform, float WalkSpeed)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        walkSpeed = WalkSpeed;
        localEnemyScript = transform.GetComponent<LocalEnemyScript>();


    }

    public override NodeState Evaluate()
    {

        Transform target = (Transform)GetData("target");

        if (!localEnemyScript.hit)
        {
            animator.SetBool("Walking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, walkSpeed * Time.deltaTime * speedMultiplier);
        }
        else
        {
            animator.SetBool("Walking", false);
        }



        state = NodeState.RUNNING;
        return state;
    }
}

