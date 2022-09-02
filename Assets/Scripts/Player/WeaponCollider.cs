using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public float damage = 1f;




    private void OnTriggerEnter2D(Collider2D other)
    {
        
      
        if(other.tag == "Enemy")
        {
            EnemyCollider enemyCollider = other.GetComponent<EnemyCollider>();
            if(enemyCollider != null)
                enemyCollider.Hit(damage);

            
        }
    }

}
