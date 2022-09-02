using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour
{
    SpriteController spriteController;
    Animation playerAnimator;

    Vector2 movement;
    bool dash;
    bool sneak;
    bool shield;
    bool attack;


    //--------------------

    private void Awake()
    {
        spriteController = FindObjectOfType<SpriteController>();
        playerAnimator = FindObjectOfType<Animation>();
    }
    private void Update()
    {
        ButtonInputs();
    }


    //--------------------


    void ButtonInputs()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        dash = Input.GetButton("Jump");
        sneak = Input.GetButton("Running");
        shield = Input.GetButton("Fire2");
        attack = Input.GetButton("Fire1");
    }


    //--------------------


    public float GetMovementX()
    {
        return movement.x;
    }
    public float GetMovementY()
    {
        return movement.y;
    }
    public bool GetDashInput()
    {
        return dash;
    }
    public bool GetSneakInput()
    {
        return sneak;
    }
    public bool GetShieldInput()
    {
        return shield;
    }
    public bool GetAttackInput()
    {
        return attack;
    }

}
