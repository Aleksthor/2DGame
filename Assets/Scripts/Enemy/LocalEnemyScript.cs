using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEnemyScript : MonoBehaviour
{
    [Header("Enemy Health")]
    public float health = 1;

    [Header("Enemy Weapon Collider")]
    public PolygonCollider2D weaponCollider;

    [Header("Private Variables")]
    public bool hit = false;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rigidBody;


    void Start()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Hit(float Damage, Vector2 ImpactDirection, float Force)
    {
        animator.SetTrigger("Hit");
        hit = true;
        health = health - Damage;
        rigidBody.AddForce(ImpactDirection * Force);

    }

    public void Destroy()
    {

        hit = false;
        if (health <= 0)
        {
            Destroy(gameObject);
        }   
    }





    public void ColliderOn()
    {
        weaponCollider.enabled = true;
    }
    public void ColliderOff()
    {
        weaponCollider.enabled = false;
    }
}
