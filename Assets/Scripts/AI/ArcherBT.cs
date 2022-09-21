using System.Collections.Generic;
using BehaviorTrees;



public class ArcherBT : BehaviorTrees.BehaviorTree
{

    public float FOVRange = 10f;
    public float StopRange = 2.5f;
    public float MovementSpeed = 2f;



    private UnityEngine.Transform playerTransform;
    private EnemyBowRotation enemyBowRotationScript;

    private LocalEnemyScript localEnemyScript;
    private TaskGoToTargetShoot taskGoToTargetShoot;

    public void Awake()
    {

        UnityEngine.GameObject player = UnityEngine.GameObject.Find("Player");
        playerTransform = player.transform;
        localEnemyScript = gameObject.GetComponent<LocalEnemyScript>();
        enemyBowRotationScript = gameObject.transform.Find("Hand").GetComponent<EnemyBowRotation>();


    }

    public void FixedUpdate()
    {
        if (taskGoToTargetShoot == null)
        {
            taskGoToTargetShoot = (TaskGoToTargetShoot)Root.GetChild(2).GetChild(1);
            taskGoToTargetShoot.speedMultiplier = localEnemyScript.GetSpeedMultiplier();
        }

    }


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new TaskOnGuard(transform, playerTransform, 3f),
            new TaskSetAgro(transform, playerTransform),
            new Sequence(new List<Node>
            {
                new TaskCheckPlayerInFOV(transform, playerTransform, FOVRange),
                new TaskGoToTargetShoot(transform, playerTransform, enemyBowRotationScript, FOVRange, StopRange, MovementSpeed),
            }),
            new TaskPatrol(transform, MovementSpeed),
        });

        return root;


    }
}
