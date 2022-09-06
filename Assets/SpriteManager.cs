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

    private Camera mainCam;

    void Awake()
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
    }

    private void Start()
    {
        buttonInput = FindObjectOfType<ButtonInput>();
        localPlayerScript = playerObject.GetComponent<LocalPlayerScript>();

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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




        }


        //if (localPlayerScript.GetAttack())
        //{

        //}
    }



}
