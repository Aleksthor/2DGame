using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class TaskAttackandStrongAttack : Node
{
    private Transform transform;
    private Animator animator;
    private EnemyWeaponCollider weaponCollider;

    private Transform playerTransform;

    float strongAttackChance = 50;
    float normalAttack;
    float strongAttack;

    public TaskAttackandStrongAttack(Transform AgentTransform, Transform PlayerTransform, float StrongAttackChance, float normal, float strong)
    {
        transform = AgentTransform;
        animator = AgentTransform.GetComponent<Animator>();
        playerTransform = PlayerTransform;
        strongAttackChance = StrongAttackChance;
        weaponCollider = transform.Find("Hand").transform.Find("Weapon").GetComponent<EnemyWeaponCollider>();
        normalAttack = normal;
        strongAttack = strong;
    }



    public override NodeState Evaluate()
    {

        float random = Random.Range(1f, 100f);
        
        if (random > strongAttackChance)
        {
            weaponCollider.damage = strongAttack;
            animator.SetTrigger("StrongAttack");
        }
        else
        {
            weaponCollider.damage = normalAttack;
            animator.SetTrigger("Attack");
        }
        
        GameEvents.current.EnemyMeleeAttack((Vector2)playerTransform.position);
        GoblinSpriteDirection gsp;
        if (transform.GetComponent<GoblinSpriteDirection>() != null)
        {
            gsp = transform.GetComponent<GoblinSpriteDirection>();
            if (playerTransform.position.x - transform.position.x < 0)
            {
                gsp.Flip(false);
            }
            else
            {
                gsp.Flip(true);
            }
        }
        EnemySpriteManager esm;
        if (transform.GetComponent<EnemySpriteManager>() != null)
        {
            esm = transform.GetComponent<EnemySpriteManager>();
            if (playerTransform.position.x - transform.position.x < 0)
            {
                esm.Flip(false);
            }
            else
            {
                esm.Flip(true);
            }
        }







        state = NodeState.RUNNING;
        return state;

    }



}
