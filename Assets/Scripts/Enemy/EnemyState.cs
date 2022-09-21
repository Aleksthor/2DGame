using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTrees;

public class EnemyState : MonoBehaviour
{

    public BehaviorTree behaviourTree;

    public enum StatusEffect
    {
        None,
        Stun
    }


    public StatusEffect statusEffect;

    private float StunClock = 0f;
    public float StunTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        statusEffect = StatusEffect.None;
    }

    // Update is called once per frame
    void Update()
    {
        switch(statusEffect)
        {

            case StatusEffect.None:
                behaviourTree.enabled = true;
                break;

            case StatusEffect.Stun:
                StunClock -= Time.deltaTime;
                if (StunClock < 0f)
                {
                    statusEffect = StatusEffect.None;
                }
                behaviourTree.enabled = false;
                break;

            default:
                break;

        }
    }
}
