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
    private SpriteRenderer playerWeaponRenderer;
    private Transform weaponTransform;
    private Animator animator;
    private PolygonCollider2D playerWeaponCollider;
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

        playerWeaponRenderer = player.GetPlayer().transform.Find("Hand").transform.Find("Weapon").GetComponent<SpriteRenderer>();
        weaponTransform = player.GetPlayer().transform.Find("Hand").transform.Find("Weapon").GetComponent<Transform>();
        animator = player.GetPlayer().GetComponent<Animator>();
        playerWeaponCollider = player.GetPlayer().transform.Find("Hand").transform.Find("Weapon").GetComponent<PolygonCollider2D>();

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

        var weaponPoints = playerWeaponCollider.points;
        int totalPoints = weaponPoints.Length;

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
                weaponTransform.localPosition = clubPos;
                animator.SetInteger("WeaponType", 0);
                break;
            case 1: // Dagger
                weaponTransform.localPosition = daggerPos;
                animator.SetInteger("WeaponType", 1);
                break;
            case 2: // Sword
                weaponTransform.localPosition = swordPos;
                animator.SetInteger("WeaponType", 2);
                break;
            case 3: // Staff
                weaponTransform.localPosition = staffPos;
                animator.SetInteger("WeaponType", 3);
                break;
            case 4: // Wand
                weaponTransform.localPosition = wandPos;
                animator.SetInteger("WeaponType", 4);
                break;
            default:
                break;
        }

        // Change my weapon points
        for (int i = 0; i < totalPoints; i++)
        {
            weaponPoints[i].x = (float)weapon.colliderPointX[i];
            weaponPoints[i].y = (float)weapon.colliderPointY[i];
        }

        playerWeaponCollider.points = weaponPoints;
    }
}
