using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
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

    public enum AbilityType
    {
        Dash,
        DashBehindEnemy,
        DashBackwards,
        FirePit,
        WaterWaterSlam,
        BuffDefense
    };

    [Header("Weapon Abilites")]
    [SerializeReference] public AbilityType ability1;
    [SerializeReference] public AbilityType ability2;
    [SerializeReference] public AbilityType ability3;






}


