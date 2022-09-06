using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [Header("Weapon Variables")]
    public float damage = 1f;
    public float knockBackForce = 50f;
    public float speedMultiplier = 1f;


    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
    }





    private void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.tag == "Enemy")
        {
            damage = weaponManager.damage;
            knockBackForce = weaponManager.knockBackForce;

            LocalEnemyScript localEnemyScript = other.GetComponent<LocalEnemyScript>();
            if(localEnemyScript != null)
            {
                Vector2 direction = (localEnemyScript.transform.position - gameObject.transform.position).normalized;
                localEnemyScript.Hit(damage, direction , knockBackForce, speedMultiplier);
            }         
        }
    }

}
