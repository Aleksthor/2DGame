using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWarrior : MonoBehaviour
{
    [SerializeField] SpriteRenderer Body;
    [SerializeField] SpriteRenderer Head;
    [SerializeField] SpriteRenderer Hat;
    [SerializeField] SpriteRenderer FacialHair;
    [SerializeField] SpriteRenderer HandRenderer;
    [SerializeField] SpriteRenderer WeaponRenderer;
    [SerializeField] SpriteRenderer EffectRenderer;

    [SerializeField] Transform Hand;
    [SerializeField] Transform Weapon;
    [SerializeField] Transform Effect;


    private Vector2 Frame1;
    private Vector2 Frame2;
    private Vector2 direction;
    private bool flipState = false;
    private bool flipLastDirection = false;



    void Start()
    {
        Frame1 = transform.position;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (flipState)
        {
            Frame1 = transform.position;
            direction = Frame1 - Frame2;
            flipState = false;
        }
        else
        {
            Frame2 = transform.position;
            direction = Frame2 - Frame1;
            flipState = true;
        }

        if (direction.x > 0f || flipLastDirection)
        {
            Body.flipX = true;
            Head.flipX = true;
            Hat.flipX = true;
            FacialHair.flipX = true;
            HandRenderer.flipX = true;
            WeaponRenderer.flipX = true;
            EffectRenderer.flipX = true;
            Weapon.transform.localPosition = new Vector2(Weapon.transform.localPosition.x * -1f, Weapon.transform.localPosition.y);
            Hand.transform.localPosition = new Vector2(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y);
            Effect.transform.localPosition = new Vector2(Effect.transform.localPosition.x * -1f, Effect.transform.localPosition.y);
            Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);

            flipLastDirection = true;
        }
        if (direction.x < 0f || !flipLastDirection)
        {
            Body.flipX = false;
            Head.flipX = false;
            FacialHair.flipX = false;
            Hat.flipX = false;
            HandRenderer.flipX = false;
            WeaponRenderer.flipX = false;
            EffectRenderer.flipX = false;
            flipLastDirection = false;
        }
    }


}
