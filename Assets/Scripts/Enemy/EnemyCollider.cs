using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public float health = 1;


    public PolygonCollider2D weaponCollider;


    void Start()
    {
        if(weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        
    }

    public void Hit(float Damage)
    {
        health -= Damage;
        if (health <= 0f)
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
