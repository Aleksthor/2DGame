using System.Collections.Generic;
using BehaviorTree;



public class LearningBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    public UnityEngine.Transform playerTransform;

    public static float speed = 2f;
    public static float FOVRange = 10f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskCheckPlayerInFOV(transform, playerTransform),
                new TaskGoToTargetShoot(transform, playerTransform),
            }),
            new TaskPatrol(transform, waypoints),
        }) ;

        return root;


    }
}
