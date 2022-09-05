using System.Collections.Generic;
using BehaviorTree;



public class ArcherBT : Tree
{

    public float FOVRange = 10f;
    public float StopRange = 2.5f;
    public float MovementSpeed = 2f;


    public UnityEngine.Transform[] waypoints;
    private UnityEngine.Transform playerTransform;
    private EnemyBowRotation enemyBowRotationScript;

    private EnemyStats enemyStats;
    private TaskGoToTargetShoot taskGoToTargetShoot;

    public void Awake()
    {
        UnityEngine.Debug.Log("Awake Start");
        UnityEngine.GameObject player = UnityEngine.GameObject.Find("Player");
        playerTransform = player.transform;
        enemyStats = gameObject.GetComponent<EnemyStats>();
        enemyBowRotationScript = gameObject.transform.Find("Hand").GetComponent<EnemyBowRotation>();
        UnityEngine.Debug.Log("Awake End");

    }

    public void FixedUpdate()
    {
        if (taskGoToTargetShoot == null)
        {
            taskGoToTargetShoot = (TaskGoToTargetShoot)Root.GetChild(0).GetChild(1);
        }

    }


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
