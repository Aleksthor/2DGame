using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public float damage = 1f;

    private float attackDelay = 0.5f;
    private float attackClock = 0f;
    private bool attack = false;

    void Update()
    {
        if(attack)
        {
            attackClock += Time.deltaTime;

            if(attackClock > attackDelay)
            {
                attackClock = 0f;
                attack = false;
            }
        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        
      
        if(other.tag == "Enemy" && !attack)
        {
            attack = true;
            EnemyCollider enemyCollider = other.GetComponent<EnemyCollider>();
            if(enemyCollider != null)
                enemyCollider.Hit(damage);
 
        }
    }

}
