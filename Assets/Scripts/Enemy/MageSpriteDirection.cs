using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpriteDirection : MonoBehaviour
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
    private bool flipLastDirection = false;
    [SerializeField]
    private Vector2 left = new Vector2(0.02f, 0.12f);
    [SerializeField]
    private Vector2 right = new Vector2(-0.02f, 0.12f);
    [SerializeField]
    private LocalEnemyScript localEnemyScript;
    private Transform playerTransform;
    private Player player;


    void Start()
    {
        Frame1 = transform.position;

        localEnemyScript = gameObject.GetComponent<LocalEnemyScript>();
        player = FindObjectOfType<Player>();
        playerTransform = player.GetPlayer().transform;
    }


    // Update is called once per frame
    void LateUpdate()
    {


        direction = playerTransform.position - transform.position;


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
