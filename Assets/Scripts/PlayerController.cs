using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    Animator animator;

    public Vector2 MovementVector;
    public float MovementSpeed = 2f;


 

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

   





}
