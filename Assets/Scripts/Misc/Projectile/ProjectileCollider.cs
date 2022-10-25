using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    [SerializeField] public float damage = 5f;
    [SerializeField] public WeaponCollider.DamageType damageType;
    [SerializeField] public float critRate = 0f;
    [SerializeField] public float critDamage = 2f;
    [SerializeField] public float knockbackForce = 40f;
    [SerializeField] public float speedMultiplier = 1f;
    [SerializeField] public float slowDownLength = 0f;
    [SerializeField] public int poise = 0;

    public bool hasDeathAnimation = false;

    private bool didCrit;
    float timeTolive = 10f;

    private void Update()
    {
        timeTolive -= Time.deltaTime;
        if (timeTolive < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {

            if (other.GetType() == typeof(PolygonCollider2D))
            {

                float random = Random.Range(1, 100);
                didCrit = false;


                if (random < critRate)
                {
                    Debug.Log("Critical Hit");

                    damage *= critDamage;
                    didCrit = true;
                }

                Debug.Log("Damage : " + damage);
                GameEvents.current.WeaponCollission(other.gameObject, damage, knockbackForce, speedMultiplier, slowDownLength, gameObject.transform.position, didCrit, damageType, poise);

                didCrit = false;

                if (hasDeathAnimation)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Hit");
                }
                else
                {
                    Destroy(gameObject);
                }

            }


        }
    }

}