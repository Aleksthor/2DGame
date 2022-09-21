using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class TaskPatrol : Node
{

    private Transform transform;
    private Animator animator;
    private float walkSpeed;

    public TaskPatrol(Transform agenttransform, float WalkSpeed)
    {
        animator = agenttransform.GetComponent<Animator>();
        transform = agenttransform;
        animator.SetBool("Walking", true);
        walkSpeed = WalkSpeed;
        startPos = transform.position; 
    }

    private float waitTime = 5f;
    private float waitCounter = 0f;
    private bool waiting = true;

    private float walkTime = 5f;
    private float walkCounter = 0f;

    private Vector2 startPos;
    private Vector2 nextwp;




    public override NodeState Evaluate()
    {
        
        if (waiting)
        {
            nextwp.x = startPos.x + Random.Range(-2.0f, 2.0f);
            nextwp.y = startPos.y + Random.Range(-2.0f, 2.0f);
            walkCounter = 0f;

            waitCounter += Time.deltaTime;
            if (waitCounter > waitTime)
            {
                waiting = false;
                waitCounter = 0f;
                animator.SetBool("Walking", true);
            }
            
        }
        else
        {
            walkCounter += UnityEngine.Time.deltaTime;


            animator.SetBool("Walking", true);
            if (Vector2.Distance(transform.position, nextwp) < 0.2f)
            {

                transform.position = nextwp;
                waitCounter = 0f;
                waiting = true;

                // Genious Code under here
                animator.SetBool("Walking", false);
                
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, nextwp, walkSpeed * Time.deltaTime);
                
            }
            if (walkCounter > walkTime)
            {
                waiting = true;
                animator.SetBool("Walking", false);
            }

        }
        state = NodeState.RUNNING;
        return state;

    }
}
