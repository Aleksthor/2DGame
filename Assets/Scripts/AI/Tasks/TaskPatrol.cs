using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{

    private Transform transform;
    private Transform[] waypoints;
    private Animator animator;
    private float walkSpeed;

    public TaskPatrol(Transform agenttransform, Transform[] agentwaypoints, float WalkSpeed)
    {
        animator = agenttransform.GetComponent<Animator>();
        transform = agenttransform;
        waypoints = agentwaypoints;
        animator.SetBool("Walking", true);
        walkSpeed = WalkSpeed;
    }

    private float waitTime = 3f;
    private float waitCounter = 0f;
    private bool waiting = false;

    private int currentWaypointIndex = 0;


    public override NodeState Evaluate()
    {
        
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter > waitTime)
            {
                waiting = false;
                animator.SetBool("Walking", true);
            }
            
        }
        else
        {
            animator.SetBool("Walking", true);
            Transform wp = waypoints[currentWaypointIndex];
            if (Vector2.Distance(transform.position, wp.position) < 0.2f)
            {

                transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                // Genious Code under here
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                animator.SetBool("Walking", false);
                
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, wp.position, walkSpeed * Time.deltaTime);
                
            }


        }
        state = NodeState.RUNNING;
        return state;

    }
}
