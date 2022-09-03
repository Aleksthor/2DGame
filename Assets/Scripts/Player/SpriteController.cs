using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [Header("Sprite & Tranform References")]
    [SerializeField]
    Rigidbody2D Rigidbody;
    public SpriteRenderer BodySprite;
    public SpriteRenderer HeadSprite;
    public SpriteRenderer HandSprite;
    public SpriteRenderer HatSprite;
    public SpriteRenderer WeaponSprite;
    public SpriteRenderer ShieldSprite;
    public SpriteRenderer EffectsSprite;

    public Transform Hand;
    public Transform Shield;
    public Transform Effects;

    public PolygonCollider2D weaponCollider;

    [Header("Private Variables")]
    public Vector2 MovementVector;

    public int StartingWeaponType;
  
    bool FlipLastInput = false;

    bool CanTurn = true;
    #pragma warning disable 414
    bool CanMove = true;
#pragma warning restore 414

    private Camera mainCam;


    [SerializeField] GameObject EnergyBall;
    [SerializeField] Transform ShotPoint;

    void Start()
    {
        weaponCollider.enabled = false;
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("WeaponType", StartingWeaponType);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        MovementVector.x = Input.GetAxis("Horizontal");
        MovementVector.y = Input.GetAxis("Vertical");
    }


    void LateUpdate()
    {
        if (CanTurn)
        {
            if (MovementVector.x > 0 || FlipLastInput)
            {

                BodySprite.flipX = true;
                HeadSprite.flipX = true;
                HandSprite.flipX = true;
                ShieldSprite.flipX = true;
                HatSprite.flipX = true;
                WeaponSprite.flipX = true;
                EffectsSprite.flipX = true;


                Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
                Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x * -1f, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
                Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x * -1f, Effects.transform.localPosition.y, Effects.transform.localPosition.z);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);


                FlipLastInput = true;
            }
            if (MovementVector.x < 0 || !FlipLastInput)
            {
                BodySprite.flipX = false;
                HeadSprite.flipX = false;
                HandSprite.flipX = false;
                ShieldSprite.flipX = false;
                HatSprite.flipX = false;
                WeaponSprite.flipX = false;
                EffectsSprite.flipX = false;



                FlipLastInput = false;

            }
        }
        else
        {
            if (FlipLastInput)
            {
                BodySprite.flipX = true;
                HeadSprite.flipX = true;
                HandSprite.flipX = true;
                ShieldSprite.flipX = true;
                HatSprite.flipX = true;
                WeaponSprite.flipX = true;
                EffectsSprite.flipX = true;

                Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
                Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x * -1f, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
                Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x * -1f, Effects.transform.localPosition.y, Effects.transform.localPosition.z);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);



                FlipLastInput = true;


            }
            if (!FlipLastInput)
            {
                BodySprite.flipX = false;
                HeadSprite.flipX = false;
                HandSprite.flipX = false;
                ShieldSprite.flipX = false;
                HatSprite.flipX = false;
                WeaponSprite.flipX = false;
                EffectsSprite.flipX = false;

                Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
                Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
                Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x, Effects.transform.localPosition.y, Effects.transform.localPosition.z);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z);



                FlipLastInput = false;

            }




        }
    }




    public void StartAttack()
    {
        CanMove = false;
        CanTurn = false;
    }


    public void StopAttack()
    {
        CanMove = true;
        CanTurn = true;
    }

    public bool GetCanMove()
    {
        return CanMove;
    }

    public void ColliderOn()
    {
        weaponCollider.enabled = true;
    }
    public void ColliderOff()
    {
        weaponCollider.enabled = false;
    }

    public void SpawnEnergyBall()
    {
        Vector2 direction = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)ShotPoint.position;

        GameObject NewEnergyBall = Instantiate(EnergyBall, ShotPoint.position, Quaternion.Euler(0f, 0f, Vector2.Angle((Vector2)ShotPoint.position, (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition))));

    }


}