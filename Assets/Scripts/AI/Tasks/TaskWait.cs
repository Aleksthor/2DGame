using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskWait : Node
{
    private float waitTime;
    private float waitClock = 0f;

    public TaskWait(float Seconds)
    {
        waitTime = Seconds;
    }

    public override NodeState Evaluate()
    {
        waitClock += Time.deltaTime;
        if (waitClock < waitTime)
        {
            state = NodeState.FAILURE;
            return state;
        }
        waitClock = 0f;
        state = NodeState.SUCCESS; 
        return state;

    }
}