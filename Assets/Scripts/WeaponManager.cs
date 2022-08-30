using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    
    // Reference to the Player's Components
    public Transform WeaponTransform;           
    public Animator PlayerAnimator;
    public SpriteRenderer WeaponSprite;


    // Pre saved locations for each type
    Vector2 BluntWeaponPosition = new Vector2(0.01f, 0.1f);
    Vector2 DaggerWeaponPosition = new Vector2(0.01f, 0.05f);
    Vector2 SwordWeaponPosition = new Vector2(0.01f, 0.1f);



    // Flip boolean so we only Fire once
    bool Fired = false;



    // These are for testing purposes
    public Sprite Dagger;
    public Sprite Club;
    public Sprite Sword;



    // Enumeration of each weapon type
    public enum WeaponType
    {
        WT_Blunt,
        WT_Dagger,
        WT_Sword

    }

    #pragma warning disable 414
    WeaponType CurrentWeapon;
    #pragma warning restore 414

    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Fired == false)
            {
                OnFire();
                Fired = true;
            }

        }
        else
        {
            Fired = false;
        }
    }

    // Update is called once per frame
    public void WeaponChange(int Type)
    {
      
        switch ((WeaponType)Type)
        {
            case WeaponType.WT_Blunt:
                WeaponTransform.transform.localPosition = BluntWeaponPosition;
                CurrentWeapon = WeaponType.WT_Blunt;
                WeaponSprite.sprite = Club;
                break;
            case WeaponType.WT_Dagger:
                WeaponTransform.transform.localPosition = DaggerWeaponPosition;
                CurrentWeapon = WeaponType.WT_Dagger;
                WeaponSprite.sprite = Dagger;
                break;
            case WeaponType.WT_Sword:
                WeaponTransform.transform.localPosition = SwordWeaponPosition;
                CurrentWeapon = WeaponType.WT_Sword;
                WeaponSprite.sprite = Sword;
                break;
            default:
                break;
        }



    }


    void OnFire()
    {

        PlayerAnimator.SetTrigger("Attack");

    }

}
