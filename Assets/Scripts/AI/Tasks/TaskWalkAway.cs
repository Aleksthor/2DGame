using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskWalkAway : Node
{
    private Transform transform;
    private Transform playerTransform;
    private Animator animator;
    private float walkSpeed;
    public float speedMultiplier = 1f;
    private float range;
    LocalEnemyScript localEnemyScript;


    public TaskWalkAway(Transform AgentTransform, Transform PlayerTransform, float WalkSpeed, float FOV)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        walkSpeed = WalkSpeed;
        localEnemyScript = transform.GetComponent<LocalEnemyScript>();

        range = FOV;
    }

    public override NodeState Evaluate()
    {

        // If we are too close walk away. State is always Running


        Transform target = (Transform)GetData("target");

        if (!localEnemyScript.hit && (transform.position - playerTransform.position).magnitude < range)
        {
            animator.SetBool("Walking", true);
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.position - target.position), walkSpeed * Time.deltaTime * speedMultiplier);
        }
        else
        {
            animator.SetBool("Walking", false);
        }



        state = NodeState.RUNNING;
        return state;
    }
}