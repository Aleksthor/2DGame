using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public SpriteRenderer BodySprite;
    public SpriteRenderer HeadSprite;
    public SpriteRenderer HandSprite;
    public SpriteRenderer HatSprite;
    public SpriteRenderer Weapon;
    public SpriteRenderer ShieldSprite;



    Animator animator;

    public Vector2 MovementVector;
    public float MovementSpeed = 20f;


    bool Fired = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Update()
    {
        MovementVector.x = Input.GetAxis("Horizontal");
        MovementVector.y = Input.GetAxis("Vertical");


        if (Input.GetButton("Fire1"))
        {
            if (Fired == false)
            {
                OnFire();
                Fired = true;
            }
           
        }
        else
        {
            Fired = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MovementVector.x != 0 || MovementVector.y != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }


        Rigidbody.MovePosition(Rigidbody.position + (MovementVector * MovementSpeed * Time.fixedDeltaTime));



        
    }


    void LateUpdate()
    {
        if (MovementVector.x > 0)
        {
            BodySprite.flipX = true;
            HeadSprite.flipX = true;
            HandSprite.flipX = true;
            ShieldSprite.flipX = true;
            HatSprite.flipX = true;

        }
        if (MovementVector.x < 0)
        {
            BodySprite.flipX = false;
            HeadSprite.flipX = false;
            HandSprite.flipX = false;
            ShieldSprite.flipX = false;
            HatSprite.flipX = false;

        }
    }

    void OnFire()
    {
        animator.SetTrigger("Attack");
        if (MovementVector.x > 0)
        {
            animator.SetBool("Flip", true);
        }
        if (MovementVector.x < 0)
        {
            animator.SetBool("Flip", false);
        }
        Debug.Log("Working");
            
    }
}
