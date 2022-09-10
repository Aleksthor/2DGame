using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{


    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
    }





    private void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.tag == "Enemy")
        {
            GameEvents.current.WeaponCollission(
                other.gameObject,
                weaponManager.damage,
                weaponManager.knockBackForce,
                weaponManager.speedMultiplier,
                weaponManager.slowDownLength,
                gameObject.transform.position
                ) ;       
        }
    }

}
