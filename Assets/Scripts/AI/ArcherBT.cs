using System.Collections.Generic;
using BehaviorTree;



public class ArcherBT : Tree
{

    public float FOVRange = 10f;
    public float StopRange = 2.5f;
    public float MovementSpeed = 2f;


    public UnityEngine.Transform[] waypoints;


    public UnityEngine.Transform playerTransform;


    public EnemyBowRotation enemyBowRotationScript;


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskCheckPlayerInFOV(transform, playerTransform, FOVRange),
                new TaskGoToTargetShoot(transform, playerTransform, enemyBowRotationScript, FOVRange, StopRange, MovementSpeed),
            }),
            new TaskPatrol(transform, waypoints, MovementSpeed),
        });

        return root;


    }
}
