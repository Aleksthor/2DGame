using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class TaskOnGuard : Node
{
    private Transform transform;
    private Animator animator;
    private Transform playerTransform;
    private float onGuardTime;
    private float onGuardClock = 10f;
    private bool onGuard;


    public TaskOnGuard(Transform AgentTransform, Transform PlayerTransform, float Time)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        onGuardTime = Time;

        GameEvents.current.OnPlayerInvisible += PlayerInvisible;
        GameEvents.current.OnPlayerNotInvisible += PlayerNotInvisible;

    }

    public override NodeState Evaluate()
    {

        if (onGuard)
        {
            animator.SetBool("Walking", false);

            if (onGuardClock > onGuardTime)
            {
                onGuard = false;
            }


            parent.ClearData("target");
            onGuardClock += Time.deltaTime;
            state = NodeState.SUCCESS;
            return state;
        }
        onGuard = false;

        state = NodeState.FAILURE;
        return state;

    }


    private void PlayerInvisible(float time)
    {
        onGuard = true;
        onGuardClock = 0f;
        onGuardTime = time;
        parent.ClearData("target");
        
    }
    private void PlayerNotInvisible()
    {
        onGuard = false;
    }
}