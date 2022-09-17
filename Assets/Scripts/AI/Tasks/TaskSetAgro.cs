using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskSetAgro : Node
{

    private Transform transform;
    private Transform playerTransform;

    public TaskSetAgro(Transform AgentTransform, Transform PlayerTransform)
    {
        transform = AgentTransform;
        playerTransform = PlayerTransform;
    }



    public override NodeState Evaluate()
    {
       
        if (transform.GetComponent<LocalEnemyScript>().health != transform.GetComponent<LocalEnemyScript>().maxHealth)
        {
            //GameEvents.current.SetAgro(transform.gameObject); 
            parent.SetData("target", playerTransform);
        }


        state = NodeState.FAILURE;
        return state;
    }


}
