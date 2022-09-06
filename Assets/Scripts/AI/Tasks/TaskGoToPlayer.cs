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
    private float attackRange;

    public float speedMultiplier = 1f;    
    LocalEnemyScript localEnemyScript;
   

    public TaskGoToPlayer(Transform AgentTransform, Transform PlayerTransform, float WalkSpeed, float AttackRange)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        walkSpeed = WalkSpeed;
        attackRange = AttackRange;
        localEnemyScript = transform.GetComponent<LocalEnemyScript>();

    }

    public override NodeState Evaluate()
    {

        Transform target = (Transform)GetData("target");


        if ((transform.position - playerTransform.position).magnitude > 10f)
        {
            Debug.Log("StateFailure");
            ClearData("target");
            animator.SetBool("Walking", false);
            state = NodeState.FAILURE;
            return state;
        }


        if ((transform.position - playerTransform.position).magnitude < attackRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }
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

