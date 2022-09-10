using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskWait : Node
{
    public float waitTime;
    public float waitClock = 10;

    public TaskWait(float Seconds)
    {
        waitTime = Seconds;
    }

    public override NodeState Evaluate()
    {
        // Wait for the amount of time then go on to the next step for a single frame

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