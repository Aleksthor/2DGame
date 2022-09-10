using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskSetAgro : Node
{

    private Transform transform;
    private Animator animator;
    private Transform playerTransform;

    public TaskSetAgro(Transform AgentTransform, Transform PlayerTransform)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
    }



    public override NodeState Evaluate()
    {
       
        if (transform.GetComponent<LocalEnemyScript>().health != transform.GetComponent<LocalEnemyScript>().maxHealth)
        {
            parent.SetData("target", playerTransform);
        }
        state = NodeState.FAILURE;
        return state;
    }

    private void SetAgro(GameObject GO)
    {
        Debug.Log("Check");
        if(GameObject.ReferenceEquals(GO, transform.gameObject))
        {
            parent.SetData("target", playerTransform);
        }
        
        
    }
}
