using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEnemyScript : MonoBehaviour
{
    [Header("Enemy Health")]
    public float health = 1;

    [Header("Enemy Weapon Collider")]
    public PolygonCollider2D weaponCollider;

    [Header("Private Variables")]
    public bool hit = false;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rigidBody;
    private Player player;

    private bool damaged = false;
    private float iFramesLength = 0.5f;
    private float iFrameClock = 0f;

    private bool resetSpeed = false;
    private bool resetAgain = false;
    private float resetSpeedTime = 3f;
    private float resetSpeedClock = 0f;


    public float speedMultiplier = 1f;

    public GameObject MageOrb;
    private Transform orbSpawnPoint;
    private Transform playerTransform;

    void Start()
    {
        weaponCollider = transform.Find("Hand").transform.Find("Weapon").GetComponent<PolygonCollider2D>();
        weaponCollider.enabled = false;

        player = FindObjectOfType<Player>();


        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    

    public void Update()
    {
        if(damaged)
        {
            iFrameClock += Time.deltaTime;
            if(iFrameClock > iFramesLength)
            {
                iFrameClock = 0f;
                damaged = false;
            }
        }
        if (resetSpeed)
        {
            if (resetAgain)
            {
                resetSpeedClock = 0f;
                resetAgain = false;
            }
            resetSpeedClock += Time.deltaTime;
            if(resetSpeedTime < resetSpeedClock)
            {
                resetSpeedClock = 0f;
                resetSpeed = false;
                speedMultiplier = 1f; 
            }
          
        }


    }



    public void Hit(float Damage, Vector2 ImpactDirection, float Force, float slowdownRatio)
    {
        if (!damaged)
        {
            speedMultiplier = slowdownRatio;
            damaged = true;

            if (resetSpeed)
            {
                resetAgain = true;
            }
            resetSpeed = true;



            animator.SetTrigger("Hit");
            hit = true;
            health = health - Damage;
            rigidBody.AddForce(ImpactDirection * Force);


            
        }

    }

    public void Destroy()
    {

        hit = false;
        if (health <= 0)
        {
            Destroy(gameObject);
        }   
    }





    public void ColliderOn()
    {
        weaponCollider.enabled = true;
    }
    public void ColliderOff()
    {
        weaponCollider.enabled = false;
    }


    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }


    public void SpawnOrb()
    {
        if (orbSpawnPoint == null)
        {
            orbSpawnPoint = transform.Find("Hand").transform.Find("SpawnPoint").transform;
        }
        if (playerTransform == null)
        {
            playerTransform = player.GetPlayer().transform;
        }

        GameObject NewOrb = Instantiate(MageOrb, orbSpawnPoint.position, orbSpawnPoint.rotation);
        NewOrb.GetComponent<Rigidbody2D>().velocity = (playerTransform.position - transform.position).normalized * 10f;

    }
}
