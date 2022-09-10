using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Slots")]
    [SerializeField] Weapon weapon1;
    [SerializeField] Weapon weapon2;
    [SerializeField] Weapon weapon3;

    public Weapon currentWeapon;

    // Player components
    private Animator animator;
    private Player player;

    Vector2 clubPos = new Vector2(0.01f, 0.1f);
    Vector2 daggerPos = new Vector2(0.01f, 0.05f);
    Vector2 swordPos = new Vector2(0.01f, 0.1f);
    Vector2 staffPos = new Vector2(0.01f, 0.1f);
    Vector2 wandPos = new Vector2(0.01f, 0.05f);


    [Header("Current Weapon Info")]
    public float damage = 1f;
    public float knockBackForce;

    [Header("Magic Info")]
    public float force;
    public float manaCost;

    [Header("Debuff Info")]
    public float speedMultiplier;
    public float slowDownLength;

    void Awake()
    {
        // Player Manager
        player = FindObjectOfType<Player>();

    }

    void Start()
    {
        animator = player.GetPlayer().GetComponent<Animator>();
    }




    // Debug purposes in Update
    void Update()
    {

        if(Input.GetButton("1"))
        {
            ChangeWeapon(weapon1);
        }


        if (Input.GetButton("2"))
        {
            ChangeWeapon(weapon2);
        }

        if (Input.GetButton("3"))
        {
            ChangeWeapon(weapon3);
        }

    }



    public void ChangeWeapon(Weapon weapon)
    {
        GameEvents.current.ChangeWeapon(weapon);
        GameEvents.current.ChangeStats(weapon.damage, weapon.knockBackForce, weapon.speedMultiplier, weapon.slowDownLength, weapon.manaCost, weapon.force, weapon.localPosition);
        GameEvents.current.ChangeCollider(weapon.colliderPointX, weapon.colliderPointY);


        damage = weapon.damage;
        knockBackForce = weapon.knockBackForce;
        force = weapon.force;
        manaCost = weapon.manaCost;
        speedMultiplier = weapon.speedMultiplier;
        slowDownLength = weapon.slowDownLength;

        currentWeapon = weapon;

        switch ((int)weapon.weaponType)
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
}
