using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskMaxRange : Node
{
    private Transform transform;
    private Animator animator;
    private Transform playerTransform;
    private float maxRange;

    public TaskMaxRange(Transform AgentTransform, Transform PlayerTransform, float Range)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        maxRange = Range;
    }

    public override NodeState Evaluate()
    {
        // If player is close then the specified range

        if ((transform.position - playerTransform.position).magnitude < maxRange)
        {
            parent.parent.SetData("target", playerTransform);
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }
}