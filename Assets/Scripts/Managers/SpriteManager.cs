using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpriteManager : SingletonMonoBehaviour<SpriteManager>
{


    #region Sprites
    public List<SpriteRenderer> renderersToFlip;

    private SpriteRenderer BodySprite;
    private SpriteRenderer HeadSprite;
    private SpriteRenderer HandSprite;
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

    public bool FlipLastInput = false;



    ButtonInput buttonInput;
    LocalPlayerScript localPlayerScript;

    private PlayerManager player;
    private GameObject playerObject;
    private Transform PivotPoint;
    private GameObject HandObject;

    private Camera mainCam;
    private InventoryManager inventoryManager;
    private Animator animator;


    private Vector2 attackDirection;        // Direction the player should face
    public bool attack;                    // Are we attacking right now?
    private bool canTurn;                   // Are we allowed to turn right now?


    private bool lowerOpacity;
    public bool isTwoHanded = false;
    public bool canDualWield = false;

    public bool hideHand = false;
    public bool hideShield = false;



    private void Start()
    {

        player = PlayerManager.Instance;
        playerObject = PlayerSingleton.instance.gameObject;

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
        mainCam = CameraSingleton.instance.gameObject.transform.Find("Main Camera").GetComponent<Camera>();



        buttonInput = ButtonInput.Instance;
        localPlayerScript = LocalPlayerScript.Instance;

        inventoryManager = InventoryManager.Instance;
        // GameEvents
        GameEvents.current.OnPlayerAttack += PlayerAttackStart;
        GameEvents.current.EndPlayerAttack += PlayerAttackEnd;
        GameEvents.current.OnChangeWeapon += SwapWeapon;
        GameEvents.current.OnChangeShield += ChangeShield;
        GameEvents.current.OnShowSecondary += ShowSecondary;
        GameEvents.current.OnRemoveSecondary += RemoveSecondary;
        canTurn = true;

        //PlayerSpriteListener

        GameEvents.current.OnPlayerSpriteChange += PlayerSpriteChange;
        GameEvents.current.OnLowerPlayerOpacity += LowerOpacity;
        GameEvents.current.OnNormalPlayerOpacity += NormalOpacity;
        GameEvents.current.OnShowShield += ShowShield;
        GameEvents.current.OnHideShield += HideShield;

        animator = PlayerSingleton.instance.gameObject.GetComponent<Animator>();
        Hand2.gameObject.SetActive(false);
       
    }
    




    public void LowerOpacity()
    {
        lowerOpacity = true;
        BodySprite.color = new Color(BodySprite.color.r, BodySprite.color.b, BodySprite.color.g, 0.5f);
        HeadSprite.color = new Color(HeadSprite.color.r, HeadSprite.color.b, HeadSprite.color.g, 0.5f);
        HandSprite.color = new Color(HandSprite.color.r, HandSprite.color.b, HandSprite.color.g, 0.5f);

        WeaponSprite.color = new Color(WeaponSprite.color.r, WeaponSprite.color.b, WeaponSprite.color.g, 0.5f);
        ShieldSprite.color = new Color(ShieldSprite.color.r, ShieldSprite.color.b, BodySprite.color.g, 0.5f);
        EffectsSprite.color = new Color(EffectsSprite.color.r, EffectsSprite.color.b, EffectsSprite.color.g, 0.5f);

        Head_Top.color = new Color(Head_Top.color.r, Head_Top.color.b, Head_Top.color.g, 0.5f);
        Head_Bottom.color = new Color(Head_Bottom.color.r, Head_Bottom.color.b, Head_Bottom.color.g, 0.5f);
        Ear.color = new Color(Ear.color.r, Ear.color.b, Ear.color.g, 0.5f);
        Hair.color = new Color(Hair.color.r, Hair.color.b, Hair.color.g, 0.5f);
        Facialhair.color = new Color(Facialhair.color.r, Facialhair.color.b, Facialhair.color.g, 0.5f);
        Eye.color = new Color(Eye.color.r, Eye.color.b, Eye.color.g, 0.5f);
        Eyebrow.color = new Color(Eyebrow.color.r, Eyebrow.color.b, Eyebrow.color.g, 0.5f);
        Mouth.color = new Color(Mouth.color.r, Mouth.color.b, Mouth.color.g, 0.5f);
        Nose.color = new Color(Nose.color.r, Nose.color.b, Nose.color.g, 0.5f);
    }

    public void NormalOpacity()
    {
        lowerOpacity = false;
        BodySprite.color = new Color(BodySprite.color.r, BodySprite.color.b, BodySprite.color.g, 1);
        HeadSprite.color = new Color(HeadSprite.color.r, HeadSprite.color.b, HeadSprite.color.g, 1);
        HandSprite.color = new Color(HandSprite.color.r, HandSprite.color.b, HandSprite.color.g, 1);

        WeaponSprite.color = new Color(WeaponSprite.color.r, WeaponSprite.color.b, WeaponSprite.color.g, 1);
        ShieldSprite.color = new Color(ShieldSprite.color.r, ShieldSprite.color.b, ShieldSprite.color.g, 1);
        EffectsSprite.color = new Color(EffectsSprite.color.r, EffectsSprite.color.b, EffectsSprite.color.g, 1);

        Head_Top.color = new Color(Head_Top.color.r, Head_Top.color.b, Head_Top.color.g, 1);
        Head_Bottom.color = new Color(Head_Bottom.color.r, Head_Bottom.color.b, Head_Bottom.color.g, 1);
        Ear.color = new Color(Ear.color.r, Ear.color.b, Ear.color.g, 1);
        Hair.color = new Color(Hair.color.r, Hair.color.b, Hair.color.g, 1);
        Facialhair.color = new Color(Facialhair.color.r, Facialhair.color.b, Facialhair.color.g, 1);
        Eye.color = new Color(Eye.color.r, Eye.color.b, Eye.color.g, 1);
        Eyebrow.color = new Color(Eyebrow.color.r, Eyebrow.color.b, Eyebrow.color.g, 1);
        Mouth.color = new Color(Mouth.color.r, Mouth.color.b, Mouth.color.g, 1);
        Nose.color = new Color(Nose.color.r, Nose.color.b, Nose.color.g, 1);
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
        #region Shield and OffHand
        if (hideShield)
        {
            animator.SetBool("ShieldEquipped", false);
            ShieldSprite.gameObject.SetActive(false);
        }
        else
        {
            if (ShieldSprite.sprite != null)
            {
                animator.SetBool("ShieldEquipped", true);
            }
            ShieldSprite.gameObject.SetActive(true);
        }
        if (hideHand)
        {
            Hand2.gameObject.SetActive(false);
        }
        else if (!attack)
        {
            Hand2.gameObject.SetActive(true);
        }
        if (isTwoHanded)
        {
            Hand2.transform.localPosition = new Vector2(Hand.transform.localPosition.x, Hand.transform.localPosition.y - 0.03f);
        }

        

        #endregion




        if (lowerOpacity)
        {
            BodySprite.color = new Color(BodySprite.color.r, BodySprite.color.b, BodySprite.color.g, 0.5f);
        }

        if (canTurn)
        {
            if (buttonInput.GetMovementX() > 0 || FlipLastInput)
            {

                foreach (SpriteRenderer renderer in renderersToFlip)
                {
                    renderer.flipX = true;
                }


                Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
                Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x * -1f, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
                Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x * -1f, Effects.transform.localPosition.y, Effects.transform.localPosition.z);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);
                Hand2.transform.localPosition = new Vector3(Hand2.transform.localPosition.x * -1f, Hand2.transform.localPosition.y, Hand2.transform.localPosition.z);

                if (InventoryManager.Instance.currentWeapon != null)
                {
                    if (InventoryManager.Instance.currentWeapon.weaponType == Weapon.WeaponType.Bow)
                    {
                        Hand.transform.Find("Weapon").transform.localPosition = new Vector2(-0.02f, 0f);
                    }
                }

                FlipLastInput = true;
            }
            if (buttonInput.GetMovementX() < 0 || !FlipLastInput)
            {

                foreach (SpriteRenderer renderer in renderersToFlip)
                {
                    renderer.flipX = false;
                }



                FlipLastInput = false;

            }
        }
        else
        {
            if (FlipLastInput)
            {

                foreach (SpriteRenderer renderer in renderersToFlip)
                {
                    renderer.flipX = true;
                }

                Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
                Hand.transform.Find("Weapon").transform.localPosition = InventoryManager.Instance.currentWeapon.localPosition * -1f;
                Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x * -1f, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
                Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x * -1f, Effects.transform.localPosition.y, Effects.transform.localPosition.z);
                Hand.transform.eulerAngles = new Vector3(Hand.transform.eulerAngles.x, Hand.transform.eulerAngles.y, Hand.transform.eulerAngles.z * -1f);
                Hand2.transform.localPosition = new Vector3(Hand2.transform.localPosition.x * -1f, Hand2.transform.localPosition.y, Hand2.transform.localPosition.z);



                FlipLastInput = true;


            }
            if (!FlipLastInput)
            {

                foreach (SpriteRenderer renderer in renderersToFlip)
                {
                    renderer.flipX = false;
                }



                FlipLastInput = false;

            }



            if (attack)
            {
                if (lowerOpacity)
                {
                    NormalOpacity();
                    GameEvents.current.PlayerNotInvisible();
                }

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
                            Hand2.transform.localPosition = attackDirection.normalized / 12f * distance;
                            Hand2.transform.localPosition = new Vector2(Hand2.transform.localPosition.x, Hand2.transform.localPosition.y - 0.16f);
                            Hand2.transform.up = attackDirection;
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
                                Hand.transform.Find("Weapon").transform.localPosition = InventoryManager.Instance.currentWeapon.localPosition * -1f;

                            }



                            break;
                        case 3: // Staff
                            break;
                        case 4: // Wand

                            break;
                        case 5: // Bow

                            Hand.transform.localPosition = attackDirection.normalized / 12f * distance;
                            Hand.transform.localPosition = new Vector2(Hand.transform.localPosition.x, Hand.transform.localPosition.y - 0.08f);

                            Hand.transform.right = attackDirection * -1f;
                            if (FlipLastInput)
                            {
                                Hand.transform.right = attackDirection;
                            }

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
            if(isTwoHanded || canDualWield)
            {
                animator.SetBool("ShieldEquipped", false);
                hideHand = false;
                hideShield = true;
                ShieldSprite.sprite = shield.itemSprite;
            }
            else
            {
                animator.SetBool("ShieldEquipped", true);
                hideHand = true;
                hideShield = false;
                ShieldSprite.sprite = shield.itemSprite;
            }
        }
        else
        {
            animator.SetBool("ShieldEquipped", false);
            hideHand = true;
            ShieldSprite.sprite = null; 
        }
       
    }

    private void ShowSecondary(Weapon weapon)
    {
        Hand2.transform.Find("Weapon2").gameObject.SetActive(true);
        Hand2.transform.Find("Weapon2").transform.GetComponent<SpriteRenderer>().sprite = weapon.itemSprite;
        PlayerSingleton.instance.transform.GetComponent<Animator>().SetBool("IsDualwielding", true);
    }

    private void RemoveSecondary()
    {
        PlayerSingleton.instance.transform.GetComponent<Animator>().SetBool("IsDualwielding", false);
        Hand2.transform.Find("Weapon2").transform.GetComponent<SpriteRenderer>().sprite = null;
        Hand2.transform.Find("Weapon2").gameObject.SetActive(false);
    }

    private void ShowShield()
    {
        hideHand = true;
        hideShield = false;
    }

    private void HideShield()
    {
        hideHand = false;
        hideShield = true;
    }


    void PlayerSpriteChange(Sprite head_top, Sprite head_bottom, Sprite head_ear, Sprite head_hand, Sprite head_hair, Sprite head_facialhair, Sprite head_eye, Sprite head_eyebrow, Sprite head_mouth, Sprite head_nose,
                                Color headTop, Color hair, Color facialhair, Color eye, Color eyebrow, Color mouth)
    {


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

    }
}
