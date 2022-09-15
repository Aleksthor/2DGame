using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteDirection : MonoBehaviour
{

    // Local Components

    private SpriteRenderer Body;
    private SpriteRenderer Head;
    private SpriteRenderer Hat;
    private SpriteRenderer FacialHair;
    private SpriteRenderer HandRenderer;
    private SpriteRenderer WeaponRenderer;
    private SpriteRenderer EffectRenderer;
    private Transform Hand;
    private Transform Weapon;
    private Transform Effect;

    // Variables in charge of getting the enemies direction vector

    private Vector2 Frame1;
    private Vector2 Frame2;
    public Vector2 direction;
    private bool flipState = false;

    // When standing still we use this bool
    public bool flipLastDirection = false;
    private bool attacking = false;

    // Weapon Position on each direction
    private Vector2 left = new Vector2(0.01f, 0.12f);  
    private Vector2 right = new Vector2(-0.01f, 0.12f);

    // Reference to the most central enemy script
    private LocalEnemyScript localEnemyScript;
    
    private Vector2 playerPosition;

    void Awake()
    {
        Body = transform.Find("Body").GetComponent<SpriteRenderer>();
        Head = transform.Find("Head").GetComponent<SpriteRenderer>();
        Hat =  transform.Find("Head").transform.Find("Hat").GetComponent<SpriteRenderer>();
        FacialHair = transform.Find("Head").transform.Find("FacialHair").GetComponent<SpriteRenderer>();
        HandRenderer = transform.Find("Hand").GetComponent<SpriteRenderer>();
        WeaponRenderer = transform.Find("Hand").transform.Find("Weapon").GetComponent<SpriteRenderer>();
        EffectRenderer = transform.Find("Effects").GetComponent<SpriteRenderer>();

        Hand = transform.Find("Hand").GetComponent<Transform>();
        Weapon = transform.Find("Hand").transform.Find("Weapon").GetComponent<Transform>();
        Effect = transform.Find("Effects").GetComponent<Transform>();

        
    }


    void Start()
    {
        Frame1 = transform.position;

        localEnemyScript = gameObject.GetComponent<LocalEnemyScript>();

        GameEvents.current.OnEnemyMeleeAttack += EnemyMeleeAttack;

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

        if (!attacking || localEnemyScript.hit)
        {

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
        else
        {
            if (flipLastDirection)
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
                Effect.transform.eulerAngles = new Vector3(Effect.transform.eulerAngles.x, Effect.transform.eulerAngles.y, Effect.transform.eulerAngles.z) * -1f;




            }
            if (!flipLastDirection)
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

            Quaternion rotation;
            Vector2 attackDirection = playerPosition - (Vector2)transform.position;
            if (attackDirection.x > 0f)
            {
                rotation = Quaternion.FromToRotation(new Vector2(1f, 0f), attackDirection);
            }
            else
            {
                rotation = Quaternion.FromToRotation(new Vector2(-1f, 0f), attackDirection);
            }



            Hand.transform.localPosition = Hand.transform.localPosition + (Vector3)attackDirection.normalized / 15f;

            Hand.transform.rotation = rotation * Hand.transform.rotation;
            Effect.transform.right = attackDirection * -1f;
            Effect.transform.localPosition = attackDirection.normalized / 10f;
            Effect.transform.localPosition = new Vector2(Effect.transform.localPosition.x , Effect.transform.localPosition.y - 0.05f);
            if (flipLastDirection)
            {

                Effect.transform.right = attackDirection;

            }
        }
        


    }


    void EnemyMeleeAttack(Vector2 Position)
    {
        playerPosition = Position;
    }

    public void Flip(bool input)
    {
        if (input)
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
        if (!input)
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

    public void StartAttack()
    {
        attacking = true;
    }
    public void StopAttack()
    {
        attacking = false;
    }

    public void TurnOffAttack()
    {
        attacking = false;
    }


}
