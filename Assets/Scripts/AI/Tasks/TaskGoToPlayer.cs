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

    bool canWalk = true;
   

    public TaskGoToPlayer(Transform AgentTransform, Transform PlayerTransform, float WalkSpeed)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        walkSpeed = WalkSpeed;
        localEnemyScript = transform.GetComponent<LocalEnemyScript>();

        GameEvents.current.OnEnemyStartAttack += StartAttack;
        GameEvents.current.OnEnemyStopAttack += StopAttack;
  
        GameEvents.current.OnDestroyObject += OnDestroy;
        
    }

    public override NodeState Evaluate()
    {

        Transform target = (Transform)GetData("target");

        
        if (!localEnemyScript.hit && canWalk)
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

    private void StartAttack(GameObject gameObject)
    {
        if (gameObject != null && transform != null)
        {
            if (gameObject == transform.gameObject)
            {
                canWalk = false;
                animator.SetBool("Walking", false);
            }
        }


    }


    private void StopAttack(GameObject gameObject)
    {
        if (gameObject != null && transform != null)
        {
            if (gameObject == transform.gameObject)
            {
                canWalk = true;
                animator.SetBool("Walking", true);
            }
        }

    }


    public void OnDestroy()
    {
        GameEvents.current.OnEnemyStartAttack -= StartAttack;
        GameEvents.current.OnEnemyStopAttack -= StopAttack;

        GameEvents.current.OnDestroyObject -= OnDestroy;
    }
}

