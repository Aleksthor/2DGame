using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Weapon")]

public class Weapon : Item
{

    public enum WeaponType
    {
        Blunt,
        Dagger,
        Sword,
        Staff, 
        Wand,
        Bow
    };

    public enum StaffAttack
    {
        SmallFireBall,
        SmallWaterBall
    };

    public enum WandAttack
    {
        MagicShard,
        EnergyShard
       
    };


    public Vector2 localPosition;



    [Header("Weapon Variables")]
    public WeaponType weaponType;
    public float damage;
    public float knockBackForce;
    public float critDamage;
    public float critRate;
    public bool isTwoHanded;
    public bool canDualWield;
    public float staminaUse;
    public List<Sprite> bowSprites;

    [Header("Magic Variables")]
    public float magicDamage;
    public float force;
    public float manaCost;
    public StaffAttack staffBA;
    public WandAttack wandBA;

    

    [Header("Debuff Info")]
    public float speedMultiplier;
    public float slowDownLength;

    [Header("Weapon Collider Points")]
    public double[] colliderPointX;
    public double[] colliderPointY;

    [Header("Weapon Abilites")]
    public Ability ability1;
    public Ability ability2;
    public Ability ability3;

    public Sprite ability1Icon;
    public Sprite ability2Icon;
    public Sprite ability3Icon;





}


