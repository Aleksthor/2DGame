using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private float damage = 5f;
    private float knockbackForce = 40f;
    private float speedMultiplier = 1f;
    private float slowDownLength = 0f;

    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();

        GameEvents.current.OnChangeStats += ChangeStats;
    }

    private void ChangeStats(float Damage, float KnockBackForce, float SpeedMultiplier, float SlowDownLength, float ManaCost, float Force)
    {
        damage = Damage;
        knockbackForce = KnockBackForce;
        speedMultiplier = SpeedMultiplier;
        slowDownLength = SlowDownLength;

    }





    private void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.tag == "Enemy")
        {
            GameEvents.current.WeaponCollission(
                other.gameObject,
                damage,
                knockbackForce,
                speedMultiplier,
                slowDownLength,
                gameObject.transform.position
                ) ;       
        }
    }

}
