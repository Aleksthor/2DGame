using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerScript : MonoBehaviour
{
    
    
    private PolygonCollider2D weaponCollider;
    private Rigidbody2D Rigidbody;
    private Camera mainCam;
    private WeaponManager weaponManager;


    bool CanTurn = true;
    #pragma warning disable 414
    bool CanMove = true;
    #pragma warning restore 414

    private bool Attack = false;
    public Vector2 MovementVector;
    public int StartingWeaponType;


    // Link up where we spawn our shots and our projectiles

    public Transform ShotPoint;

    [Header("Projectiles")]
    public GameObject EnergyBall;



    void Start()
    {

        weaponCollider = transform.Find("Hand").transform.Find("Weapon").GetComponent<PolygonCollider2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        weaponManager = FindObjectOfType<WeaponManager>();
        Animator animator = gameObject.GetComponent<Animator>();


        weaponCollider.enabled = false;
        animator.SetInteger("WeaponType", StartingWeaponType);


    }


    void Update()
    {
        MovementVector.x = Input.GetAxis("Horizontal");
        MovementVector.y = Input.GetAxis("Vertical");


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

        Debug.Log(ShotPoint.position);
        Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

        GameObject NewEnergyBall = Instantiate(EnergyBall, ShotPoint.position, Quaternion.Euler(0f, 0f, Vector2.Angle((Vector2)ShotPoint.position, (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition))));

        NewEnergyBall.GetComponent<WeaponCollider>().damage = weaponManager.damage;


        NewEnergyBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * weaponManager.force;

    }


}