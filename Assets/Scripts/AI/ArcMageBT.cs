using System.Collections.Generic;
using BehaviorTrees;



public class ArcMageBT : BehaviorTrees.BehaviorTree
{

    public float FOV = 10f;
    public float MovementSpeed = 3f;
    public float WaitTimeOrb = 2f;
    public float WaitTimeShards = 3f;

    public UnityEngine.Transform playerTransform;


    public void Awake()
    {

        UnityEngine.GameObject player = UnityEngine.GameObject.Find("Player");
        playerTransform = player.transform;

    }


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new TaskOnGuard(transform, playerTransform, 3f),
            new TaskSetAgro(transform, playerTransform),
            new Sequence(new List<Node>
            {
                new TaskMaxRange(transform, playerTransform, FOV/2f),
                new TaskWait(WaitTimeOrb),
                new TaskShootOrb(transform),
            }),
            new Sequence(new List<Node>
            {
                new TaskMaxRange(transform, playerTransform, FOV),
                new TaskWait(WaitTimeShards),
                new TaskShootShard(transform),
            }),
            new Sequence(new List<Node>
            {
                new TaskMaxRange(transform, playerTransform, 5),
                new TaskWalkAway(transform, playerTransform, MovementSpeed, 4.9f),
            }),
            new Sequence(new List<Node>
            {
                new TaskCheckPlayerInFOV(transform, playerTransform, FOV),
                new TaskGoToPlayer(transform, playerTransform, MovementSpeed),
            }),
            new TaskPatrol(transform, MovementSpeed),

        });

        return root;


    }
}
