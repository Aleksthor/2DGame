using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public float health = 1;


    public PolygonCollider2D weaponCollider;

    private Animator animator;
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

    public void Hit(float Damage)
    {
        animator.SetTrigger("Hit");

        health = health - Damage;

    }

    public void Destroy()
    {
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
