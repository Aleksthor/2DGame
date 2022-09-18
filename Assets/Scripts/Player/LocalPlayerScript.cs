using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalPlayerScript : SingletonMonoBehaviour<LocalPlayerScript>
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
    public GameObject SmallFireBall;
    public GameObject SmallWaterBall;
    public GameObject EnergyShard;
    public GameObject MagicShard;

    private float manaCost = 3;
    private float force = 10;
    private float magicDamage = 5f;
    private float speedMultiplier = 1f;
    private float slowDownLength = 1f;

    public Vector2 localPosition;






    void Start()
    {
        weaponCollider = transform.Find("Hand").transform.Find("Weapon").GetComponent<PolygonCollider2D>();
        player = PlayerManager.Instance;
        weaponCollider.enabled = false;
        mainCam = CameraSingleton.instance.transform.Find("Main Camera").transform.GetComponent<Camera>();

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

    public void CanMoveOff()
    {
        canMove = false;
    }




    public void ColliderOn()
    {
        weaponCollider.enabled = true;
    }
    public void ColliderOff()
    {
        weaponCollider.enabled = false;
    }



    #region Staff Attacks

    public void SpawnSmallFireBall()
    {
        if (player.GetManaValue() < manaCost)
        {

        }
        else
        {
            player.SetManaValue(-manaCost);


            Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

            GameObject smallFireBall = Instantiate(SmallFireBall, ShotPoint.position, ShotPoint.rotation);
            smallFireBall.transform.right = direction;
            smallFireBall.GetComponent<ProjectileCollider>().speedMultiplier = speedMultiplier;
            smallFireBall.GetComponent<ProjectileCollider>().slowDownLength = slowDownLength;
            smallFireBall.GetComponent<ProjectileCollider>().damage = magicDamage;
            smallFireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * force;
        }


    }


    public void SpawnSmallWaterBall()
    {
        if (player.GetManaValue() < manaCost)
        {

        }
        else
        {
            player.SetManaValue(-manaCost);


            Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

            GameObject smallWaterBall = Instantiate(SmallWaterBall, ShotPoint.position, ShotPoint.rotation);
            smallWaterBall.transform.right = direction;
            smallWaterBall.GetComponent<ProjectileCollider>().speedMultiplier = speedMultiplier;
            smallWaterBall.GetComponent<ProjectileCollider>().slowDownLength = slowDownLength;
            smallWaterBall.GetComponent<ProjectileCollider>().damage = magicDamage;
            smallWaterBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * force;
        }

    }

    #endregion


    #region Wand Attacks
    public void SpawnEnergyShard()
    {
        if(player.GetManaValue() < manaCost)
        {

        }
        else
        {
            player.SetManaValue(-manaCost);


            Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

            GameObject NewEnergyBall = Instantiate(EnergyShard, ShotPoint.position, ShotPoint.rotation);
            NewEnergyBall.GetComponent<ProjectileCollider>().speedMultiplier = speedMultiplier;
            NewEnergyBall.GetComponent<ProjectileCollider>().slowDownLength = slowDownLength;
            NewEnergyBall.transform.right = direction * -1f;
            NewEnergyBall.GetComponent<ProjectileCollider>().damage = magicDamage;
            NewEnergyBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * force;
        }


    }
    public void SpawnMagicShard()
    {
        if (player.GetManaValue() < manaCost)
        {

        }
        else
        {
            player.SetManaValue(-manaCost);

            Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

            GameObject NewMagicBall = Instantiate(MagicShard, ShotPoint.position, ShotPoint.rotation);
            NewMagicBall.GetComponent<ProjectileCollider>().speedMultiplier = speedMultiplier;
            NewMagicBall.GetComponent<ProjectileCollider>().slowDownLength = slowDownLength;
            NewMagicBall.transform.right = direction * -1f;
            NewMagicBall.GetComponent<ProjectileCollider>().damage = magicDamage;
            NewMagicBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * force;

        }
    }

    #endregion
    private void ChangeStats(float Damage, float MagicDamage, float KnockBackForce, float SpeedMultiplier, float SlowDownLength, float ManaCost, float Force, float critRate, float critDamage, Vector2 LocalPosition)
    {
        manaCost = ManaCost;
        force = Force;
        magicDamage = MagicDamage;
        localPosition = LocalPosition;
        speedMultiplier = SpeedMultiplier;
        slowDownLength = SlowDownLength;
    }


    private void LateUpdate()
    {
        transform.Find("Hand").transform.Find("Weapon").transform.localPosition = localPosition;
        transform.Find("Hand2").transform.Find("Weapon2").transform.localPosition = localPosition;
    }


}