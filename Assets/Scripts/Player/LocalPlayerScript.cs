using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalPlayerScript : MonoBehaviour
{
    
    
    private PolygonCollider2D weaponCollider;   // Turn collider on with from animator
    private WeaponManager weaponManager;        // Get weapon damage when we spawn a projectile

    public int StartingWeaponType;              // temporary int so we can initalize the animator at start

    // private variables
    private bool canTurn = true;
    private bool canMove = true;
    private bool attack = false;


    public Transform ShotPoint;                 // Here we spawn our projectiles
    private Camera mainCam;                     // They will move towards camera.mousePosition
        
    [Header("Projectiles")]
    public GameObject EnergyBall;
    public GameObject MagicBall;


    private Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }


    void Start()
    {

        weaponCollider = transform.Find("Hand").transform.Find("Weapon").GetComponent<PolygonCollider2D>();
        weaponCollider.enabled = false;

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        weaponManager = FindObjectOfType<WeaponManager>();

        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("WeaponType", StartingWeaponType);


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
        if(player.GetManaValue() < weaponManager.manaCost)
        {

        }
        else
        {
            player.SetManaValue(-weaponManager.manaCost);


            Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

            GameObject NewEnergyBall = Instantiate(EnergyBall, ShotPoint.position, ShotPoint.rotation);
            NewEnergyBall.transform.right = direction * -1f;
            NewEnergyBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * weaponManager.force;
        }


    }
    public void SpawnMagicBall()
    {
        if (player.GetManaValue() < weaponManager.manaCost)
        {

        }
        else
        {
            player.SetManaValue(-weaponManager.manaCost);

            Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

            GameObject NewMagicBall = Instantiate(MagicBall, ShotPoint.position, ShotPoint.rotation);
            NewMagicBall.transform.right = direction * -1f;
            NewMagicBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * weaponManager.force;

        }
    }


}