using System.Collections.Generic;
using BehaviorTrees;



public class WarriorBT : BehaviorTrees.BehaviorTree
{

    public float attackSpeed = 1.2f;
    public float attackRange = 2f;
    public float FOV = 6f;
    public float MovementSpeed = 2f;


    public UnityEngine.Transform playerTransform;
    private LocalEnemyScript localEnemyScript;
    private TaskGoToPlayer taskGoToPlayer;

    public void Awake()
    {

        UnityEngine.GameObject player = UnityEngine.GameObject.Find("Player");
        playerTransform = player.transform;
        localEnemyScript = gameObject.GetComponent<LocalEnemyScript>();

    }

    public void FixedUpdate()
    {
        if (taskGoToPlayer == null)
        {
            taskGoToPlayer = (TaskGoToPlayer)Root.GetChild(3).GetChild(1);
        }
        if (taskGoToPlayer != null)
        {
            taskGoToPlayer.speedMultiplier = localEnemyScript.GetSpeedMultiplier();

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
                new TaskCheckIfPlayerInAttackRange(transform, playerTransform, attackRange),
                new TaskWait(attackSpeed, 10f),
                new TaskAttack(transform, playerTransform),
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