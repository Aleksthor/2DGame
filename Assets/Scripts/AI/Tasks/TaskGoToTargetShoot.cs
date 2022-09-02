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
    private bool walking = true;
    private static float standStillTimer = 1f;
    private static float standStillClock = 0f;
    private Animator animator;
    private float FOV;
    private float stopRange;
    private float movementSpeed;

    EnemyBowRotation bowRotationScript;

    public TaskGoToTargetShoot(Transform AgentTransform, Transform PlayerTransform, EnemyBowRotation BowRotationScript,float FOVRange, float StopRange, float MovementSpeed)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        bowRotationScript = BowRotationScript;
        FOV = FOVRange;
        stopRange = StopRange;
        movementSpeed = MovementSpeed;
    }

    public override NodeState Evaluate()
    {
        bowRotationScript.hasAgro = true;
        Transform target = (Transform)GetData("target");


        if ((transform.position - playerTransform.position).magnitude > FOV)
        {

            ClearData("target");
            bowRotationScript.hasAgro = false;
            animator.SetBool("Walking", false);
            state = NodeState.FAILURE;
            return state;
        }



        shootClock += Time.deltaTime;
        if (shootClock > shootSpeed)
        {
            animator.SetTrigger("Attack");
            walking = false;
            shootClock = 0f;
            state = NodeState.SUCCESS;
            return state;
        }


        if (walking && (transform.position - playerTransform.position).magnitude > stopRange)
        {
            animator.SetBool("Walking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }
        
        if(!walking)
        {
            animator.SetBool("Walking", false);

            if((transform.position - playerTransform.position).magnitude > stopRange + 0.1f)
            {
                standStillClock += Time.deltaTime;
                if (standStillClock > standStillTimer)
                {
                    walking = true;
                    standStillClock = 0f;
                }
            }

        }

        state = NodeState.RUNNING;
        return state;
    }
}
