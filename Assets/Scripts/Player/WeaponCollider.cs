using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private float critRate = 0f;
    [SerializeField] private float critDamage = 2f;
    [SerializeField] private float knockbackForce = 40f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float slowDownLength = 0f;

    private float damageBefore;
    private bool boostNextAttack = false;
    private float damageBoost;
    private bool didCrit;


    void Start()
    {
        GameEvents.current.OnChangeStats += ChangeStats;
        GameEvents.current.OnBoostNextAttack += BoostNextAttack;
        GameEvents.current.OnDontBoostNextAttack += DontBoostNextAttack;
        damageBefore = damage;
    }

    private void ChangeStats(float Damage, float magicDamage, float KnockBackForce, float SpeedMultiplier, float SlowDownLength, float ManaCost, float Force, float CritRate, float CritDamage, Vector2 localPosition)
    {

        damage = Damage;
        knockbackForce = KnockBackForce;
        speedMultiplier = SpeedMultiplier;
        slowDownLength = SlowDownLength;
        critRate = CritRate;
        critRate = Mathf.Clamp(critRate, 0, 70);

        critDamage = CritDamage;

        damageBefore = Damage;
        
    }



    private void BoostNextAttack(float DamageBoost)
    {
        damageBoost = DamageBoost;
        boostNextAttack = true;
    }

    private void DontBoostNextAttack()
    {
        damageBoost = 1f;
        boostNextAttack = false;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.tag == "Enemy")
        {
            float random = Random.Range(1, 100);
            didCrit = false;
            if(boostNextAttack)
            {
                
                damage *= damageBoost;
                
            }

            if (random < critRate)
            {
                Debug.Log("Critical Hit");
                
                damage *= critDamage;
                
                Mathf.Clamp(damage, 0f, damageBefore * critDamage);
                didCrit = true;
            }
            

            GameEvents.current.WeaponCollission(other.gameObject, damage, knockbackForce, speedMultiplier, slowDownLength, gameObject.transform.position, didCrit);
            damage = damageBefore;
            didCrit = false;
            boostNextAttack = false;

        }
    }

}
