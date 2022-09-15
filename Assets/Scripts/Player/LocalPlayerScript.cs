using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalPlayerScript : MonoBehaviour
{

    public int StartingWeaponType;              // temporary int so we can initalize the animator at start

    // private variables
    private bool canTurn = true;
    private bool canMove = true;
    private bool attack = false;


    public Transform ShotPoint;                 // Here we spawn our projectiles
    private Camera mainCam;                     // They will move towards camera.mousePosition
    private PolygonCollider2D weaponCollider;   // Turn collider on with from animator

    private PlayerManager player;
    private Animator animator;

    [Header("Projectiles")]
    public GameObject EnergyBall;
    public GameObject MagicBall;

    private float manaCost = 3;
    private float force = 10;
    private float magicDamage = 5f;





    void Awake()
    {
        weaponCollider = transform.Find("Hand").transform.Find("Weapon").GetComponent<PolygonCollider2D>();
    }


    void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        weaponCollider.enabled = false;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // TEMPORARY
        animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("WeaponType", StartingWeaponType);

        GameEvents.current.OnChangeStats += ChangeStats;
        GameEvents.current.OnChangeWeaponCollider += ChangeCollider;

    }



    void ChangeCollider(double[] x, double[] y, int weaponType)
    {
        var weaponPoints = weaponCollider.points;
        int totalPoints = weaponPoints.Length;

        // Change my weapon points
        for (int i = 0; i < totalPoints; i++)
        {
            weaponPoints[i].x = (float)x[i];
            weaponPoints[i].y = (float)y[i];
        }

        weaponCollider.points = weaponPoints;


        switch (weaponType)
        {
            case 0: // Blunt
                animator.SetInteger("WeaponType", 0);
                break;
            case 1: // Dagger
                animator.SetInteger("WeaponType", 1);
                break;
            case 2: // Sword
                animator.SetInteger("WeaponType", 2);
                break;
            case 3: // Staff
                animator.SetInteger("WeaponType", 3);
                break;
            case 4: // Wand
                animator.SetInteger("WeaponType", 4);
                break;
            default:
                break;
        }
    }



    public bool GetAttack()
    {
        return attack;
    }
    
    public void StartAttack()
    {
        attack = true;
        canMove = false;
        canTurn = false;
        GameEvents.current.PlayerAttackStart(
            (mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position).x, 
            (mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position).y, 
            true,
            false
            );


    }


    public void StopAttack()
    {
        attack = false;
        canMove = true;
        canTurn = true;
        GameEvents.current.PlayerAttackEnd(false, true);

    }

    public void StartOnlyAttack()
    {
        attack = true;
    }


    public bool GetCanMove()
    {
        return canMove;
    }

    public bool GetCanTurn()
    {
        return canTurn;
    }


    public void CanTurnOff()
    {
        canTurn = false;
    }

    public void CanTurnOn()
    {
        canTurn = true;
    }

    public void ColliderOn()
    {
        weaponCollider.enabled = true;
    }
    public void ColliderOff()
    {
        weaponCollider.enabled = false;
    }

    public void SpawnEnergyBall()
    {
        if(player.GetManaValue() < manaCost)
        {

        }
        else
        {
            player.SetManaValue(-manaCost);


            Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

            GameObject NewEnergyBall = Instantiate(EnergyBall, ShotPoint.position, ShotPoint.rotation);
            NewEnergyBall.transform.right = direction * -1f;
            NewEnergyBall.GetComponent<ProjectileCollider>().damage = magicDamage;
            NewEnergyBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * force;
        }


    }
    public void SpawnMagicBall()
    {
        if (player.GetManaValue() < manaCost)
        {

        }
        else
        {
            player.SetManaValue(-manaCost);

            Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

            GameObject NewMagicBall = Instantiate(MagicBall, ShotPoint.position, ShotPoint.rotation);
            NewMagicBall.transform.right = direction * -1f;
            NewMagicBall.GetComponent<ProjectileCollider>().damage = magicDamage;
            NewMagicBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * force;

        }
    }


    private void ChangeStats(float Damage, float MagicDamage, float KnockBackForce, float SpeedMultiplier, float SlowDownLength, float ManaCost, float Force, float critRate, float critDamage, Vector2 localPosition)
    {
        manaCost = ManaCost;
        force = Force;
        magicDamage = MagicDamage;
        transform.Find("Hand").transform.Find("Weapon").transform.localPosition = localPosition;


    }

}