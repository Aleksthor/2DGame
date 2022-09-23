using System.Collections.Generic;
using BehaviorTrees;



public class GoblinSmallBossBT : BehaviorTrees.BehaviorTree
{
    public float attackDamage = 10f;
    public float strongAttackDamage = 15f;
    public float attackSpeed = 1.2f;
    public float attackRange = 2f;
    public float FOV = 6f;
    public float MovementSpeed = 2f;
    public float strongAttackChance = 50;
    public float waitToDash = 5f;
    public UnityEngine.GameObject effect;


    public UnityEngine.Transform playerTransform;
    private LocalEnemyScript localEnemyScript;
    private TaskGoToPlayer taskGoToPlayer;

    public void Awake()
    {

        UnityEngine.GameObject player = UnityEngine.GameObject.Find("Player");
        playerTransform = player.transform;
        localEnemyScript = gameObject.GetComponent<LocalEnemyScript>();

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
                new TaskAttackandStrongAttack(transform, playerTransform, strongAttackChance, attackDamage, strongAttackDamage),
            }),
            new Sequence(new List<Node>
                {
                    new TaskCheckPlayerInFOV(transform, playerTransform, FOV),
                    new TaskMinRange(transform, playerTransform, 4f),
                    new TaskWait(waitToDash, 0f),
                    new TaskDashAttack(transform, playerTransform, effect),
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