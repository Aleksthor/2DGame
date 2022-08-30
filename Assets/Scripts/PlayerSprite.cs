using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{


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
    public Transform Weapon;


    public Vector2 MovementVector;

    public Vector3 Rotation;

    bool FlipLastInput = false;

    void Update()
    {
        MovementVector.x = Input.GetAxis("Horizontal");
        MovementVector.y = Input.GetAxis("Vertical");
    }

    void LateUpdate()
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

            Rotation.x = Weapon.transform.eulerAngles.x;
            Rotation.y = Weapon.transform.eulerAngles.y;
            Rotation.z = Weapon.transform.eulerAngles.z;

            Hand.transform.localPosition = new Vector3 (Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
            Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x * -1f, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
            Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x * -1f, Effects.transform.localPosition.y, Effects.transform.localPosition.z);
            Weapon.transform.eulerAngles = new Vector3(Weapon.transform.eulerAngles.x , Weapon.transform.eulerAngles.y , Weapon.transform.eulerAngles.z * -1f);




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

            Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x, Hand.transform.localPosition.y, Hand.transform.localPosition.z);
            Shield.transform.localPosition = new Vector3(Shield.transform.localPosition.x, Shield.transform.localPosition.y, Shield.transform.localPosition.z);
            Effects.transform.localPosition = new Vector3(Effects.transform.localPosition.x, Effects.transform.localPosition.y, Effects.transform.localPosition.z);


            FlipLastInput = false;

        }


        





    }
}
