using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    HUD hud;

    #region Variables
    [Header("Moveable Object")]
    [SerializeField] GameObject player;

    [Header("Movement Parameters")]
    [SerializeField] float walkingSpeed = 5;
    [SerializeField] float sneakingSpeed = 10;
    [SerializeField] float dashingSpeed = 20;

    [Header("HUD Elements")]
    [SerializeField] float staminaDrainSpeed = 150;
    [SerializeField] float staminaRegenSpeed = 50;

    //Button Inputs
    Vector2 movement;
    bool dash;
    bool sneak;

    //bools for checking
    bool isDashing = false;
    bool isSneaking = false;
    bool isDashCooldown = false;

    //Stamina Cooldown
    [SerializeField] float staminaCooldownPrecentage = 75f;
    float dashTransparity = 0.5f;
    #endregion


    //--------------------


    private void Awake()
    {
        hud = FindObjectOfType<HUD>();
    }
    private void Update()
    {
        ButtonInput();
        MovementSpeed();

        DashActivation();
        DashRunning();
        DashCooldown();

        Sneaking();
    }


    //--------------------


    //Player Button Inputs
    void ButtonInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        dash = Input.GetButton("Jump");
        sneak = Input.GetButton("Running");
    }

    //Movement Speed
    void MovementSpeed()
    {
        if (isDashing)
        {
            player.transform.position += new Vector3(movement.x, movement.y, 0f) * dashingSpeed * Time.deltaTime;
        }
        else if (isSneaking)
        {
            player.transform.position += new Vector3(movement.x, movement.y, 0f) * sneakingSpeed * Time.deltaTime;
        }
        else
        {
            player.transform.position += new Vector3(movement.x, movement.y, 0f) * walkingSpeed * Time.deltaTime;
        }
    }

    //Dash -----
    void DashActivation()
    {
        //When Space is pressed down
        if (dash)
        {
            if (isDashCooldown)
            {
                isDashing = false;
            }
            else
            {
                isDashing = true;
            }
        }
        else
        {
            isDashing = false;
        }
    }
    void DashRunning()
    {
        if (isDashing)
        {
            //Reduce Stamina Parameter
            hud.SetStaminaValue(-staminaDrainSpeed * Time.deltaTime);

            //Set player invisible
            //player.SetActive(false);
        }
        else
        {
            //Refill Stramina Parameter if stamina isn't full
            if (hud.GetCurrentStaminaValue() < hud.GetMaxStaminaValue())
            {
                hud.SetStaminaValue(staminaRegenSpeed * Time.deltaTime);
            }

            //Set player visible
            //player.SetActive(true);
        }
    }
    void DashCooldown()
    {
        //Start Cooldown when StaminaValue <= 0
        if (hud.GetCurrentStaminaValue() <= 0f)
        {
            isDashCooldown = true;
        }

        //During Dashing Cooldown
        if (isDashCooldown)
        {
            //Change to Cooldown color
            hud.SetStaminaFillColorCooldown();

            //Exit Cooldown when Stamina Value has regained X%
            if (hud.GetCurrentStaminaValue() >= hud.GetMaxStaminaValue() * (staminaCooldownPrecentage / 100))
            {
                isDashCooldown = false;

                //Change back to Default color
                hud.SetStaminaFillColorDefault();
            }
        }
    }

    //Run -----
    void Sneaking()
    {
        if (sneak)
        {
            isSneaking = true;
        }
        else
        {
            isSneaking = false;
        }
    }
}
