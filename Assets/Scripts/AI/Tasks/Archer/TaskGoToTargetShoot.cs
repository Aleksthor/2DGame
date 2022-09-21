using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class TaskGoToTargetShoot : Node
{
    private Transform transform;
    private Transform playerTransform;
    private static float shootSpeed = 3f;
    private static float shootClock = 0f;
    private bool walking = true;
    private static float standStillTimer = 1f;
    private static float standStillClock = 0f;
    private Animator animator;
    private float FOV;
    private float stopRange;
    private float movementSpeed;

    public float speedMultiplier = 1f;

    EnemyBowRotation bowRotationScript;
    LocalEnemyScript localEnemyScript;

    public TaskGoToTargetShoot(Transform AgentTransform, Transform PlayerTransform, EnemyBowRotation BowRotationScript,float FOVRange, float StopRange, float MovementSpeed)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        bowRotationScript = BowRotationScript;
        FOV = FOVRange;
        stopRange = StopRange;
        movementSpeed = MovementSpeed;
        localEnemyScript = transform.GetComponent<LocalEnemyScript>();
    }

    public override NodeState Evaluate()
    {




        bowRotationScript.hasAgro = true;
        Transform target = (Transform)GetData("target");



        if (!walking)
        {
            animator.SetBool("Walking", false);

            if ((transform.position - playerTransform.position).magnitude > stopRange + 0.1f)
            {
                standStillClock += Time.deltaTime;
                if (standStillClock > standStillTimer)
                {
                    walking = true;
                    standStillClock = 0f;
                }
            }

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


        if ((transform.position - playerTransform.position).magnitude > FOV * 2f)
        {

            ClearData("target");
            bowRotationScript.hasAgro = false;
            animator.SetBool("Walking", false);
            state = NodeState.FAILURE;
            return state;
        }

        if ((transform.position - playerTransform.position).magnitude < stopRange && !localEnemyScript.hit)
        {
            animator.SetBool("Walking", true);
            walking = true;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.position - target.position), movementSpeed * Time.deltaTime * speedMultiplier);
         

            state = NodeState.SUCCESS;
            return state;
        }


        if (walking && (transform.position - playerTransform.position).magnitude > stopRange && !localEnemyScript.hit)
        {
            animator.SetBool("Walking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime * speedMultiplier);
        }

        

        state = NodeState.RUNNING;
        return state;
    }
}
