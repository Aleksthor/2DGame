using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskCheckPlayerInFOV : Node
{
 
    private Transform transform;
    private Animator animator;
    private Transform playerTransform;
    private float FOV;

    public TaskCheckPlayerInFOV(Transform AgentTransform, Transform PlayerTransform, float FOVRange)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        FOV = FOVRange;
    }

    public override NodeState Evaluate()
    {
        
        object t = GetData("target");
        if (t == null)
        {
            
            if ((transform.position - playerTransform.position).magnitude < FOV)
            {
                parent.parent.SetData("target", playerTransform);
                animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
