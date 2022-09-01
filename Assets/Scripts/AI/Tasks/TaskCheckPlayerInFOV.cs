using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskCheckPlayerInFOV : Node
{
 
    private Transform transform;
    private Animator animator;
    private Transform playerTransform;

    public TaskCheckPlayerInFOV(Transform AgentTransform, Transform PlayerTransform)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
    }

    public override NodeState Evaluate()
    {
        
        object t = GetData("target");
        if (t == null)
        {
            

            if ((transform.position - playerTransform.position).magnitude < 6f)
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
