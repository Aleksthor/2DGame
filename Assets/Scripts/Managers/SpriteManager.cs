using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpriteManager : MonoBehaviour
{
    #region Sprites
    private SpriteRenderer BodySprite;
    private SpriteRenderer HeadSprite;
    private SpriteRenderer HandSprite;
    private SpriteRenderer HatSprite;
    private SpriteRenderer WeaponSprite;
    private SpriteRenderer ShieldSprite;
    private SpriteRenderer EffectsSprite;

    SpriteRenderer Head_Top;
    SpriteRenderer Head_Bottom;
    SpriteRenderer Ear;
    SpriteRenderer Hair;
    SpriteRenderer Facialhair;
    SpriteRenderer Eye;
    SpriteRenderer Eyebrow;
    SpriteRenderer Mouth;
    SpriteRenderer Nose;
    #endregion

    private Transform Hand;
    private Transform Hand2;
    private Transform Shield;
    private Transform Effects;

    bool FlipLastInput = false;


    [SerializeField]
    ButtonInput buttonInput;
    [SerializeField]
    LocalPlayerScript localPlayerScript;

    private PlayerManager player;
    private GameObject playerObject;
    private Transform PivotPoint;
    private GameObject HandObject;

    private Camera mainCam;
    private InventoryManager inventoryManager;
    private Animator animator;


    private Vector2 attackDirection;        // Direction the player should face
    private bool attack;                    // Are we attacking right now?
    private bool canTurn;                   // Are we allowed to turn right now?

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
        playerObject = player.GetPlayer();

        Head_Top = playerObject.transform.Find("Head").transform.Find("Head_Top").GetComponent<SpriteRenderer>();
        Head_Bottom = playerObject.transform.Find("Head").transform.Find("Head_Bottom").GetComponent<SpriteRenderer>();
        Ear = playerObject.transform.Find("Head").transform.Find("Ear").GetComponent<SpriteRenderer>();
        Hair = playerObject.transform.Find("Head").transform.Find("Hair").GetComponent<SpriteRenderer>();
        Facialhair = playerObject.transform.Find("Head").transform.Find("Facialhair").GetComponent<SpriteRenderer>();
        Eye = playerObject.transform.Find("Head").transform.Find("Eyes").GetComponent<SpriteRenderer>();
        Eyebrow = playerObject.transform.Find("Head").transform.Find("Eyebrow").GetComponent<SpriteRenderer>();
        Mouth = playerObject.transform.Find("Head").transform.Find("Mouth").GetComponent<SpriteRenderer>();
        Nose = playerObject.transform.Find("Head").transform.Find("Nose").GetComponent<SpriteRenderer>();

        BodySprite = playerObject.transform.Find("Body").GetComponent<SpriteRenderer>();
        HeadSprite = playerObject.transform.Find("Head").GetComponent<SpriteRenderer>();
        HandSprite = playerObject.transform.Find("Hand").GetComponent<SpriteRenderer>();
        WeaponSprite = playerObject.transform.Find("Hand").transform.Find("Weapon").GetComponent<SpriteRenderer>();
        ShieldSprite = playerObject.transform.Find("Shield").GetComponent<SpriteRenderer>();
        EffectsSprite = playerObject.transform.Find("Effects").GetComponent<SpriteRenderer>();

        Hand = playerObject.transform.Find("Hand").GetComponent<Transform>();
        Hand2 = playerObject.transform.Find("Hand2").GetComponent<Transform>();
        Shield = playerObject.transform.Find("Shield").GetComponent<Transform>();
        Effects = playerObject.transform.Find("Effects").GetComponent<Transform>();

        HandObject = playerObject.transform.Find("Hand").gameObject;
        PivotPoint = playerObject.transform.Find("PivotPoint").GetComponent<Transform>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

      
    }

    private void Start()
    {
        buttonInput = FindObjectOfType<ButtonInput>();
        localPlayerScript = playerObject.GetComponent<LocalPlayerScript>();

        inventoryManager = FindObjectOfType<InventoryManager>();
        // GameEvents
        GameEvents.current.OnPlayerAttack += PlayerAttackStart;
        GameEvents.current.EndPlayerAttack += PlayerAttackEnd;
        GameEvents.current.OnChangeWeapon += SwapWeapon;
        GameEvents.current.OnChangeShield += ChangeShield;
        canTurn = true;

        //PlayerSpriteListener

        GameEvents.current.OnPlayerSpriteChange += PlayerSpriteChange;
        GameEvents.current.OnLowerPlayerOpacity += LowerOpacity;
        GameEvents.current.OnNormalPlayerOpacity += NormalOpacity;

        animator = player.GetPlayerAnimator();
        Hand2.gameObject.SetActive(false);
    }

    public void LowerOpacity()
    {

    }

    public void NormalOpacity()
    {

    }

    private void PlayerAttackStart(float x, float y, bool a, bool t)
    {
        attackDirection.x = x;
        attackDirection.y = y;
        attack = a;
        canTurn = t;
    }

    private void PlayerAttackEnd(bool a, bool t)
    {
        canTurn = t;
        attack = a;
    }



    void LateUpdate()
    {
        if (canTurn)
        {
            if (buttonInput.GetMovementX() > 0 || FlipLastInput)
            {

                BodySprite.flipX = true;
                HeadSprite.flipX = true;
                HandSprite.flipX = true;
                ShieldSprite.flipX = true;

                WeaponSprite.flipX = true;
                EffectsSprite.flipX = true;


                Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
                Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x * -1f, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
                Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x * -1f, Effects.transform.localPosition.y, Effects.transform.localPosition.z);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);
                Hand2.transform.localPosition = new Vector3(Hand2.transform.localPosition.x * -1f, Hand2.transform.localPosition.y, Hand2.transform.localPosition.z);

                FlipLastInput = true;
            }
            if (buttonInput.GetMovementX() < 0 || !FlipLastInput)
            {
                BodySprite.flipX = false;
                HeadSprite.flipX = false;
                HandSprite.flipX = false;
                ShieldSprite.flipX = false;

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

                WeaponSprite.flipX = true;
                EffectsSprite.flipX = true;

                Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
                Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x * -1f, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
                Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x * -1f, Effects.transform.localPosition.y, Effects.transform.localPosition.z);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);
                Hand2.transform.localPosition = new Vector3(Hand2.transform.localPosition.x * -1f, Hand2.transform.localPosition.y, Hand2.transform.localPosition.z);



                FlipLastInput = true;


            }
            if (!FlipLastInput)
            {
                BodySprite.flipX = false;
                HeadSprite.flipX = false;
                HandSprite.flipX = false;
                ShieldSprite.flipX = false;
                WeaponSprite.flipX = false;
                EffectsSprite.flipX = false;



                FlipLastInput = false;

            }



            if (attack)
            {
                float distance = ((Vector2)Hand.transform.position - (Vector2)playerObject.transform.position).magnitude;
                if (inventoryManager.currentWeapon != null)
                {
                    switch ((int)inventoryManager.currentWeapon.weaponType)
                    {
                        case 0:
                            #region Blunt
                            Hand.transform.localPosition = attackDirection.normalized / 7f * distance;
                            Hand.transform.localPosition = new Vector2(Hand.transform.localPosition.x, Hand.transform.localPosition.y - 0.05f);
                            Quaternion bluntRotation;
                            if (attackDirection.x > 0f)
                            {
                                bluntRotation = Quaternion.FromToRotation(new Vector2(1f, 0f), attackDirection);
                            }
                            else
                            {
                                bluntRotation = Quaternion.FromToRotation(new Vector2(-1f, 0f), attackDirection);
                            }

                            Hand.transform.rotation = bluntRotation * Hand.transform.rotation;
                            Effects.transform.right = attackDirection * -1f;
                            Effects.transform.localPosition = Hand.transform.localPosition / 1.5f;
                            Effects.transform.localPosition = new Vector2(Effects.transform.localPosition.x, Effects.transform.localPosition.y - 0.02f);
                            if (FlipLastInput)
                            {
                                Effects.transform.right = attackDirection;

                            }

                            #endregion
                            break;
                        case 1:
                            #region Dagger
                            Hand.transform.localPosition = attackDirection.normalized / 12f * distance;
                            Hand.transform.localPosition = new Vector2(Hand.transform.localPosition.x, Hand.transform.localPosition.y - 0.08f);
                            Hand.transform.up = attackDirection;
                            Effects.transform.right = attackDirection * -1f;
                            Effects.transform.localPosition = Hand.transform.localPosition;

                            if (FlipLastInput)
                            {
                                Effects.transform.right = attackDirection;
                            }
                            #endregion
                            break;
                        case 2: // Sword

                            Quaternion rotation;
                            if (attackDirection.x > 0f)
                            {
                                rotation = Quaternion.FromToRotation(new Vector2(1f, 0f), attackDirection);
                            }
                            else
                            {
                                rotation = Quaternion.FromToRotation(new Vector2(-1f, 0f), attackDirection);
                            }



                            Hand.transform.localPosition = Hand.transform.localPosition + (Vector3)attackDirection.normalized / 10f;

                            Hand.transform.rotation = rotation * Hand.transform.rotation;
                            Effects.transform.right = attackDirection * -1f;
                            Effects.transform.localPosition = attackDirection.normalized / 10f;
                            Effects.transform.localPosition = new Vector2(Effects.transform.localPosition.x, Effects.transform.localPosition.y - 0.15f);
                            if (FlipLastInput)
                            {

                                Effects.transform.right = attackDirection;

                            }



                            break;
                        case 3: // Staff

                            break;
                        case 4: // Wand

                            break;
                        default:
                            break;
                    }
                    if (attackDirection.x > 0f)
                    {
                        FlipLastInput = true;
                    }
                    else
                    {
                        FlipLastInput = false;
                    }
                }
               
            }




        }

    }


    private void SwapWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            WeaponSprite.sprite = weapon.itemSprite;
        }
    }

    private void ChangeShield(Shield shield)
    {
        if(shield != null)
        {
            animator.SetBool("ShieldEquipped", true);
            Hand2.gameObject.SetActive(false);
            ShieldSprite.sprite = shield.itemSprite;
        }
        else
        {
            animator.SetBool("ShieldEquipped", false);
            Hand2.gameObject.SetActive(true);
            ShieldSprite.sprite = null; 
        }
       
    }


    void PlayerSpriteChange(Sprite head_top, Sprite head_bottom, Sprite head_ear, Sprite head_hand, Sprite head_hair, Sprite head_facialhair, Sprite head_eye, Sprite head_eyebrow, Sprite head_mouth, Sprite head_nose,
                                Color headTop, Color hair, Color facialhair, Color eye, Color eyebrow, Color mouth)
    {
        print("test 1");
        Debug.Log(head_top);
        Debug.Log(Head_Top);

        if (head_top != null)
        {
            Head_Top.sprite = head_top;
            Head_Top.color = headTop;
        }
        if (head_bottom != null)
        {
            Head_Bottom.sprite = head_bottom;
            Head_Bottom.color = headTop;
        }
        if (head_ear != null)
        {
            Ear.sprite = head_ear;
            Ear.color = headTop;
        }
        if (head_top != null)
        {
            HandSprite.color = Head_Top.color;
            HandSprite.color = headTop;
        }
        if (head_hair != null)
        {
            Hair.sprite = head_hair;
            Hair.color = hair;
        }

        if (head_facialhair != null)
        {
            Facialhair.sprite = head_facialhair;
            Facialhair.color = facialhair;
        }
        if (head_eye != null)
        {
            Eye.sprite = head_eye;
            Eye.color = eye;
        }

        if (head_eyebrow != null)
        {
            Eyebrow.sprite = head_eyebrow;
            Eyebrow.color = eyebrow;
        }
        if (head_mouth != null)
        {
            Mouth.sprite = head_mouth;
            Mouth.color = mouth;
        }
        if (head_nose != null)
        {
            Nose.sprite = head_nose;
            Nose.color = eyebrow;
        }

        print("test 2");
    }
}
