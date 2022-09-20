using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    [SerializeField] public float damage = 5f;
    [SerializeField] public float critRate = 0f;
    [SerializeField] public float critDamage = 2f;
    [SerializeField] public float knockbackForce = 40f;
    [SerializeField] public float speedMultiplier = 1f;
    [SerializeField] public float slowDownLength = 0f;

    public bool hasDeathAnimation = false;




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            GameEvents.current.WeaponCollission(other.gameObject, damage, knockbackForce, speedMultiplier, slowDownLength, gameObject.transform.position, false);
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