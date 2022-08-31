using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables
    ButtonInput buttonInput;
    HUD hud;
    Player player;
    Animation playerAnimation;
    SpriteController spriteController;

    [Header("Movement Parameters")]
    [SerializeField] float walkingSpeed = 5;
    [SerializeField] float sneakingSpeed = 10;
    [SerializeField] float dashingSpeed = 20;

    [Header("HUD Elements")]
    [SerializeField] float staminaDrainSpeed = 150;
    [SerializeField] float staminaRegenSpeed = 50;

    //bools for checking
    bool isDashing = false;
    bool isSneaking = false;
    bool isDashCooldown = false;

    //Stamina Cooldown
    [SerializeField] float staminaCooldownPrecentage = 75f;
    #pragma warning disable 414
    float dashTransparity = 0.5f;
    #pragma warning restore 414
    #endregion


    //--------------------


    private void Awake()
    {
        hud = FindObjectOfType<HUD>();
        buttonInput = FindObjectOfType<ButtonInput>();
        player = FindObjectOfType<Player>();
        playerAnimation = FindObjectOfType<Animation>();
        spriteController = FindObjectOfType<SpriteController>();
    }


    private void Update()
    {
        Animations();
        MovementSpeed();

        DashActivation();
        DashRunning();
        DashCooldown();

        Sneaking();
    }


    //--------------------

    //Animations 
    void Animations()
    {
        if (buttonInput.GetMovementX() == 0 && buttonInput.GetMovementY() == 0)
        {
            playerAnimation.SetWalkingAnimation(false);
        }
        else
        {
            playerAnimation.SetWalkingAnimation(true);
        }
    }

    //Movement Speed
    void MovementSpeed()
    {
        if(spriteController.GetCanMove())
        {
            if (isDashing)
            {
                player.GetPlayer().transform.position += new Vector3(buttonInput.GetMovementX(), buttonInput.GetMovementY(), 0f) * dashingSpeed * Time.deltaTime;

            }
            else if (isSneaking)
            {

                player.GetPlayer().transform.position += new Vector3(buttonInput.GetMovementX(), buttonInput.GetMovementY(), 0f) * sneakingSpeed * Time.deltaTime;
            }
            else
            {

                player.GetPlayer().transform.position += new Vector3(buttonInput.GetMovementX(), buttonInput.GetMovementY(), 0f) * walkingSpeed * Time.deltaTime;
            }
        }
    }

    //Dash -----
    void DashActivation()
    {
        //When Space is pressed down
        if (buttonInput.GetDashInput())
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
        if (buttonInput.GetSneakInput())
        {
            isSneaking = true;
        }
        else
        {
            isSneaking = false;
        }
    }
}
