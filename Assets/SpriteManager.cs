using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{




    private SpriteRenderer BodySprite;
    private SpriteRenderer HeadSprite;
    private SpriteRenderer HandSprite;
    private SpriteRenderer HatSprite;
    private SpriteRenderer WeaponSprite;
    private SpriteRenderer ShieldSprite;
    private SpriteRenderer EffectsSprite;

    private Transform Hand;
    private Transform Shield;
    private Transform Effects;

    bool FlipLastInput = false;


    [SerializeField]
    ButtonInput buttonInput;
    [SerializeField]
    LocalPlayerScript localPlayerScript;

    private Player player;
    private GameObject playerObject;
    private Transform PivotPoint;
    private GameObject HandObject;
    private WeaponManager weaponManager;

    private Camera mainCam;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerObject = player.GetPlayer();

        BodySprite = playerObject.transform.Find("Body").GetComponent<SpriteRenderer>();
        HeadSprite = playerObject.transform.Find("Head").GetComponent<SpriteRenderer>();
        HandSprite = playerObject.transform.Find("Hand").GetComponent<SpriteRenderer>();
        HatSprite = playerObject.transform.Find("Head").transform.Find("Hat").GetComponent<SpriteRenderer>();
        WeaponSprite = playerObject.transform.Find("Hand").transform.Find("Weapon").GetComponent<SpriteRenderer>();
        ShieldSprite = playerObject.transform.Find("Shield").GetComponent<SpriteRenderer>();
        EffectsSprite = playerObject.transform.Find("Effects").GetComponent<SpriteRenderer>();

        Hand = playerObject.transform.Find("Hand").GetComponent<Transform>();
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


        weaponManager = FindObjectOfType<WeaponManager>();

    }



    void LateUpdate()
    {
        if (localPlayerScript.GetCanTurn())
        {
            if (buttonInput.GetMovementX() > 0 || FlipLastInput)
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
            if (buttonInput.GetMovementX() < 0 || !FlipLastInput)
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



            if (localPlayerScript.GetAttack())
            {

                Vector2 Direction = localPlayerScript.attackDirection.normalized;


                switch ((int)weaponManager.currentWeapon.weaponType)
                {
                    case 0: // Blunt

                        break;
                    case 1: // Dagger
                        float Distance = ((Vector2)Hand.transform.position - (Vector2)playerObject.transform.position).magnitude;
                        Hand.transform.localPosition = Direction / 4f * Distance;
                        Hand.transform.up = Direction;
                        Effects.transform.right = Direction * -1f;
                        Effects.transform.localPosition = Hand.transform.localPosition;

                        if (FlipLastInput)
                        {
                            EffectsSprite.flipX = false;
                        }
                        
                        break;
                    case 2: // Sword
   
                        break;
                    case 3: // Staff

                        break;
                    case 4: // Wand

                        break;
                    default:
                        break;
                }
                if (Direction.x > 0f)
                {
                    FlipLastInput = true;
                }
                else
                {
                    FlipLastInput = false;
                }
            }




        }


        //if (localPlayerScript.GetAttack())
        //{

        //}
    }



}
