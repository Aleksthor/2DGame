using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteDirection : MonoBehaviour
{
    [Header("Enemy Sprite & Transform References")]
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

    [Header("Private Varaibles")]
    [SerializeField]
    private Vector2 Frame1;
    [SerializeField]
    private Vector2 Frame2;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private bool flipState = false;

    [SerializeField] private bool flipped = false;
    [SerializeField] private bool stayFlipped = false;
    [SerializeField] private float flipClock = 0f;
    [SerializeField] private float flipTimer = 1f;

    [SerializeField]
    private bool flipLastDirection = false;
    [SerializeField]
    private Vector2 left = new Vector2(0.02f, 0.12f);
    [SerializeField]
    private Vector2 right = new Vector2(-0.02f, 0.12f);
    [SerializeField]
    private LocalEnemyScript localEnemyScript;


    void Start()
    {
        Frame1 = transform.position;

        localEnemyScript = gameObject.GetComponent<LocalEnemyScript>();

    }


    // Update is called once per frame
    void LateUpdate()
    {

        // Just a toggle for swapping place in the calculation every other frame
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

        // flip mechanism

        if (!flipped)
        {
            if ((direction.x > 0f || flipLastDirection) && !localEnemyScript.hit)
            {
                Body.flipX = true;
                Head.flipX = true;
                Hat.flipX = true;
                FacialHair.flipX = true;
                HandRenderer.flipX = true;
                WeaponRenderer.flipX = true;
                EffectRenderer.flipX = true;
                Weapon.transform.localPosition = right;
                Hand.transform.localPosition = new Vector2(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y);
                Effect.transform.localPosition = new Vector2(Effect.transform.localPosition.x * -1f, Effect.transform.localPosition.y);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);


                if (flipLastDirection == false)
                {
                    flipped = true;
                    stayFlipped = true;
                }
                flipLastDirection = true;

            }
            if ((direction.x < 0f || !flipLastDirection) && !localEnemyScript.hit)
            {
                Weapon.transform.localPosition = left;
                Body.flipX = false;
                Head.flipX = false;
                FacialHair.flipX = false;
                Hat.flipX = false;
                HandRenderer.flipX = false;
                WeaponRenderer.flipX = false;
                EffectRenderer.flipX = false;


                if (flipLastDirection == true)
                {
                    flipped = true;
                    stayFlipped = false;
                }
                flipLastDirection = false;
            }
        }
        else
        {
            flipClock += Time.deltaTime;

            if (flipClock > flipTimer)
            {
                flipped = false;
            }

            if (stayFlipped)
            {
                Body.flipX = true;
                Head.flipX = true;
                Hat.flipX = true;
                FacialHair.flipX = true;
                HandRenderer.flipX = true;
                WeaponRenderer.flipX = true;
                EffectRenderer.flipX = true;
                Weapon.transform.localPosition = right;
                Hand.transform.localPosition = new Vector2(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y);
                Effect.transform.localPosition = new Vector2(Effect.transform.localPosition.x * -1f, Effect.transform.localPosition.y);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);
            }
            if (!stayFlipped)
            {
                Weapon.transform.localPosition = left;
                Body.flipX = false;
                Head.flipX = false;
                FacialHair.flipX = false;
                Hat.flipX = false;
                HandRenderer.flipX = false;
                WeaponRenderer.flipX = false;
                EffectRenderer.flipX = false;

            }


        }

    }


}
