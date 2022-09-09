using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerScript : MonoBehaviour
{
    
    
    private PolygonCollider2D weaponCollider;
    private Rigidbody2D Rigidbody;
    private Camera mainCam;
    private WeaponManager weaponManager;
    private Player player;

    bool CanTurn = true;
    bool CanMove = true;

    private bool Attack = false;
    public int StartingWeaponType;


    // Link up where we spawn our shots and our projectiles

    public Transform ShotPoint;

    [Header("Projectiles")]
    public GameObject EnergyBall;
    public GameObject MagicBall;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }


    void Start()
    {

        weaponCollider = transform.Find("Hand").transform.Find("Weapon").GetComponent<PolygonCollider2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        weaponManager = FindObjectOfType<WeaponManager>();
        Animator animator = gameObject.GetComponent<Animator>();


        weaponCollider.enabled = false;
        animator.SetInteger("WeaponType", StartingWeaponType);


    }




    public bool GetAttack()
    {
        return Attack;
    }
    


    public void StartAttack()
    {
        Attack = true;
        CanMove = false;
        CanTurn = false;
    }


    public void StopAttack()
    {
        Attack = false;
        CanMove = true;
        CanTurn = true;
    }

    public bool GetCanMove()
    {
        return CanMove;
    }

    public bool GetCanTurn()
    {
        return CanTurn;
    }

    public void CanTurnOff()
    {
        CanTurn = false;
    }

    public void CanTurnOn()
    {
        CanTurn = true;
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

            NewEnergyBall.GetComponent<WeaponCollider>().damage = weaponManager.damage;


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

            NewMagicBall.GetComponent<WeaponCollider>().damage = weaponManager.damage;


            NewMagicBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * weaponManager.force;
        }
    }


    }