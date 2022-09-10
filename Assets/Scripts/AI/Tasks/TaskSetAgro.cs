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

    void Awake()
    {
        GameEvents.current.OnEnemyAgro += SetAgro;
    }

    public override NodeState Evaluate()
    {
        state = NodeState.FAILURE;
        return state;
    }

    private void SetAgro(GameObject GO)
    {
        if(GameObject.ReferenceEquals(GO, transform.gameObject))
        {
            parent.SetData("target", playerTransform);
        }
        
        
    }
}
