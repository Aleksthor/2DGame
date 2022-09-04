using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [Header("Weapon Variables")]
    public float damage = 1f;
    public float knockBackForce = 50f;

    [Header("Private Variables")]
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float attackClock = 0f;
    [SerializeField] private bool attack = false;

    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
    }

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
            damage = weaponManager.damage;
            knockBackForce = weaponManager.knockBackForce;

            LocalEnemyScript localEnemyScript = other.GetComponent<LocalEnemyScript>();
            if(localEnemyScript != null)
            {
                Vector2 direction = (localEnemyScript.transform.position - gameObject.transform.position).normalized;
                localEnemyScript.Hit(damage, direction , knockBackForce);
            }         
        }
    }

}
