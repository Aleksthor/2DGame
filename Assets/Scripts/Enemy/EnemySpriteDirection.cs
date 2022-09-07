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


    // Variables in charge of getting our direction Vector

    private Vector2 Frame1;
    private Vector2 Frame2;
    private Vector2 direction;
    private bool flipState = false;

    // When standing still we use this bool
    private bool flipLastDirection = false; 

    // Weapon Position on each direction
    private Vector2 left = new Vector2(0.01f, 0.12f);  
    private Vector2 right = new Vector2(-0.01f, 0.12f);

    // Reference to the most central enemy script
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


            flipLastDirection = false;
        }


    }


}
