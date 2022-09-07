using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskMinRange : Node
{
    private Transform transform;
    private Animator animator;
    private Transform playerTransform;
    private float minRange;

    public TaskMinRange(Transform AgentTransform, Transform PlayerTransform, float Range)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        minRange = Range;

    }

    public override NodeState Evaluate()
    {
        // Is the player further away than the specified Range

        if ((transform.position - playerTransform.position).magnitude > minRange)
        {
            parent.parent.SetData("target", playerTransform);
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }
}
