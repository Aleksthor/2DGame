using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTargetShoot : Node
{
    private Transform transform;
    private Transform playerTransform;
    private static float shootSpeed = 2f;
    private static float shootClock = 0f;
    private bool standStill = false;
    private static float standStillTimer = 1f;
    private static float standStillClock = 0f;
    private Animator animator;

    public TaskGoToTargetShoot(Transform AgentTransform, Transform PlayerTransform)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Go To Target");
        Transform target = (Transform)GetData("target");


        if((transform.position - playerTransform.position).magnitude > LearningBT.FOVRange)
        {
            Debug.Log("StateFailure");
            ClearData("target");
            animator.SetBool("Walking", false);
            state = NodeState.FAILURE;
            return state;
        }

        if(Vector2.Distance(transform.position, playerTransform.position) > 1)
        {
            if (!standStill)
            {
                shootClock += Time.deltaTime;
                if (shootClock > shootSpeed)
                {
                    animator.SetTrigger("Attack");
                    standStill = true;
                    shootClock = 0f;
                    state = NodeState.SUCCESS;
                    return state;
                }
            }

            if (!standStill && (transform.position - playerTransform.position).magnitude > 5f)
            {
                animator.SetBool("Walking", true);
                transform.position = Vector2.MoveTowards(transform.position, target.position, LearningBT.speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walking", false);

                standStillClock += Time.deltaTime;
                if (standStillClock > standStillTimer)
                {
                    standStill = false;
                    standStillClock = 0f;
                }
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
