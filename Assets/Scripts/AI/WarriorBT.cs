using System.Collections.Generic;
using BehaviorTree;



public class WarriorBT : Tree
{

    public float attackSpeed = 1.2f;
    public float attackRange = 2f;
    public float FOV = 6f;
    public float MovementSpeed = 2f;

    public UnityEngine.Transform[] waypoints;

   
    public UnityEngine.Transform playerTransform;

   

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new TaskCheckIfPlayerInAttackRange(transform, playerTransform, attackRange),
                new TaskAttack(transform, playerTransform, attackRange, attackSpeed),
            }),
            new Sequence(new List<Node>
            {
                new TaskCheckPlayerInFOV(transform, playerTransform, FOV),
                new TaskGoToPlayer(transform, playerTransform, MovementSpeed, attackRange),
            }),
            new TaskPatrol(transform, waypoints, MovementSpeed),
            
        });

        return root;


    }
}