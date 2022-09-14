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
    private bool justHit = false;
    private float hitClock = 0f;
    private float hitTimer = 0.2f;


    void Start()
    {
        GameEvents.current.OnChangeStats += ChangeStats;
        damageBefore = damage;
    }


    private void Update()
    {
        if (justHit)
        {
            hitClock += Time.deltaTime;
            if (hitClock > hitTimer)
            {
                justHit = false;
                hitClock = 0f;
            }
        }
    }

    private void ChangeStats(float Damage, float KnockBackForce, float SpeedMultiplier, float SlowDownLength, float ManaCost, float Force, float CritRate, float CritDamage, Vector2 localPosition)
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





    private void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.tag == "Enemy" && !justHit)
        {
            float random = Random.Range(1, 100);
            bool didCrit = false;

            if (random < critRate)
            {
                
                damage *= critDamage;
                Mathf.Clamp(damage, 0f, damageBefore * critDamage);
                didCrit = true;
            }
            

            GameEvents.current.WeaponCollission(other.gameObject, damage, knockbackForce, speedMultiplier, slowDownLength, gameObject.transform.position, didCrit);
            damage = damageBefore;
            
            justHit = true;

        }
    }

}
