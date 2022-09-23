using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] public float damage = 5f;
    [SerializeField] public float critRate = 0f;
    [SerializeField] public float critDamage = 2f;
    [SerializeField] public float knockbackForce = 40f;
    [SerializeField] public float speedMultiplier = 1f;
    [SerializeField] public float slowDownLength = 0f;



    private float damageBefore;
    private bool boostNextAttack = false;
    private float damageBoost;
    private bool didCrit;

    public int weapon;







    void Start()
    {
        GameEvents.current.OnChangeStats += ChangeStats;
        GameEvents.current.OnBoostNextAttack += BoostNextAttack;
        GameEvents.current.OnDontBoostNextAttack += DontBoostNextAttack;
        GameEvents.current.OnUpdateSecondaryWeapon += UpdateSecondaryWeapon;
        damageBefore = damage;
    }

    private void Update()
    {

    }

    private void ChangeStats(float Damage, float magicDamage, float KnockBackForce, float SpeedMultiplier, float SlowDownLength, float ManaCost, float Force, float CritRate, float CritDamage, Vector2 localPosition)
    {
        if (weapon == 0)
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


    }


    private void UpdateSecondaryWeapon(Weapon Weapon)
    {
        if (weapon == 1)
        {
            damage = Weapon.damage;
            knockbackForce = Weapon.knockBackForce;
            speedMultiplier = Weapon.speedMultiplier;
            slowDownLength = Weapon.slowDownLength;
            critRate = Weapon.critRate;
            critRate = Mathf.Clamp(critRate, 0, 70);

            critDamage = Weapon.critDamage;

            damageBefore = Weapon.damage;
        }
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

        if (other.tag == "Enemy")
        {



            if (other.GetType() == typeof(PolygonCollider2D))
            {

                float random = Random.Range(1, 100);
                didCrit = false;
                if (boostNextAttack)
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

                Debug.Log("Damage : " + damage);
                GameEvents.current.WeaponCollission(other.gameObject, damage, knockbackForce, speedMultiplier, slowDownLength, gameObject.transform.position, didCrit);
                damage = damageBefore;
                didCrit = false;
                boostNextAttack = false;

            }






        }
    }


}
