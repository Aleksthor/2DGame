using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponManager : MonoBehaviour
{
    [SerializeField] Weapon weapon1;
    [SerializeField] Weapon weapon2;
    [SerializeField] SpriteRenderer playerWeaponRenderer;
    [SerializeField] Transform weaponTransform;
    [SerializeField] Animator animator;
    [SerializeField] PolygonCollider2D weaponCollider;

    Vector2 clubPos = new Vector2(0.01f, 0.1f);
    Vector2 daggerPos = new Vector2(0.01f, 0.05f);

    


    // Debug Purposes

    private float swapTime = 2f;
    private float swapClock = 0f;
    private int currentWeapon = 0;


    // Debug purposes in Update
    void Update()
    {

        Debug.Log("Running");
        swapClock += Time.deltaTime;

        if(swapClock > swapTime)
        {
            swapClock = 0f;
            currentWeapon++;
            currentWeapon = currentWeapon % 2;
            switch(currentWeapon)
            {
                case 0:
                    Debug.Log("0");
                    ChangeWeapon(weapon1);
                    break;
                case 1:
                    Debug.Log("1");
                    ChangeWeapon(weapon2);
                    break;
                default:
                    break;
            }

        }
        Debug.Log(currentWeapon);
    }



    public void ChangeWeapon(Weapon weapon)
    {
        playerWeaponRenderer.sprite = weapon.itemSprite;
        
        switch ((int)weapon.weaponType)
        {
            case 0:
                weaponTransform.localPosition = clubPos;
                animator.SetInteger("WeaponType", 0);
                break;
            case 1:
                weaponTransform.localPosition = daggerPos;
                animator.SetInteger("WeaponType", 1);
                break;
            default:
                break;
        }
    }
}
