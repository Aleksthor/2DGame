using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : SingletonMonoBehaviour<Movement>
{
    #region Variables
    ButtonInput buttonInput;
    HUD hud;
    PlayerManager player;
    GameObject playerObject;
    Rigidbody2D playerRB;
    AnimationManager playerAnimation;
    LocalPlayerScript localPlayerScript;


    [Header("Movement Parameters")]
    [SerializeField] float walkingSpeed = 10;
    [SerializeField] float sneakingSpeed = 5;
    [SerializeField] float dashingSpeed = 15;

    [Header("HUD Elements")]
    [SerializeField] float staminaRegenSpeed = 50;

    [Header("Player Hitbox for i-frames")]
    [SerializeField] PolygonCollider2D playerCollider;

    //bools for checking
    [Header("Private Variables")]
    [SerializeField]
    bool isDashing = false;
    [SerializeField]
    bool isSneaking = false;
    [SerializeField]
    bool isDashCooldown = false;
    [SerializeField]
    public bool isShielding = false;
    [SerializeField]
    bool isAttacking = false;
    [SerializeField]
    float dashForce = 50f;
    [SerializeField]
    float dashStaminaCost = 50f;
    [SerializeField]
    float dashLength = 0.3f;
    public bool iFrames = false;
    float dashTimer = 0.3f;
    bool dashActivated = false;
    Vector2 direction;
    private Camera mainCam;

    //Stamina Cooldown
    [SerializeField] float staminaCooldownPrecentage = 75f;
    #pragma warning disable 414
    [SerializeField] float dashTransparity = 0.5f;
#pragma warning restore 414
    #endregion


    //--------------------




    private void Start()
    {
        hud = HUD.Instance;
        buttonInput = ButtonInput.Instance;
        player = PlayerManager.Instance;
        playerAnimation = AnimationManager.Instance;
        localPlayerScript = LocalPlayerScript.Instance;
        playerCollider = PlayerSingleton.instance.gameObject.GetComponent<PolygonCollider2D>();
        playerObject = PlayerSingleton.instance.gameObject;
        playerRB = playerObject.GetComponent<Rigidbody2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    private void Update()
    {
        Animations();
        Attack();
        Shield();

        Sneaking();
    }

    private void FixedUpdate()
    {
        DashActivation();
        DashRunning();
        MovementSpeed();
        DashCooldown();
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


    // Attack 
    void Attack()
    {
        if (buttonInput.GetAttackInput())
        {
            if (!isAttacking)
            {
                if (!isDashCooldown)
                {
                    playerAnimation.TriggerAttackAnimation();
                }
                
            }         
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }


    // Shield

    void Shield()
    {
        if(buttonInput.GetShieldInput())
        {
            playerAnimation.SetBlockingAnimation(true);
            isShielding = true;
        }
        else
        {
            playerAnimation.SetBlockingAnimation(false);
            isShielding = false;
        }
    }

    //Movement Speed
    void MovementSpeed()
    {
        if(localPlayerScript.GetCanMove())
        {
            if (isDashing && player.GetStamina() > 0 && !dashActivated)
            {
                player.UseStamina(dashStaminaCost);
                dashActivated = true;
                dashTimer = dashLength;
            }
            else if (isSneaking || isShielding)
            {
                Vector2 positionChange = (Vector2)playerObject.transform.position + new Vector2(buttonInput.GetMovementX(), buttonInput.GetMovementY()) * sneakingSpeed * Time.deltaTime;
                playerRB.MovePosition(positionChange);
            }
            else
            {
                Vector2 positionChange = (Vector2)playerObject.transform.position + new Vector2(buttonInput.GetMovementX(), buttonInput.GetMovementY()) * walkingSpeed * Time.deltaTime;
                playerRB.MovePosition(positionChange);
            }
        }
    }

    //Dash -----
    void DashActivation()
    {
        //When Space is pressed down
        if (buttonInput.GetDashInput() && !isDashing && player.GetStamina() > 0)
        {
            if (isDashCooldown)
            {
                
                playerAnimation.SetDashingAnimation(false);
                isDashing = false;
            }
            else
            {
                direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - playerObject.transform.position;
                playerAnimation.SetDashingAnimation(true);
                isDashing = true;
            }
        }
        else if (!isDashing)
        {
            
            playerAnimation.SetDashingAnimation(false);
            isDashing = false;
        }

    }
    void DashRunning()
    {
        if (isDashing)
        {
            //Set player invisible
            playerCollider.enabled = false;
            
        }
        else
        {
            //Refill Stramina Parameter if stamina isn't full
            if (hud.GetCurrentStaminaValue() < hud.GetMaxStaminaValue())
            {
                player.SetStaminaValue(staminaRegenSpeed * Time.fixedDeltaTime);
            }
            if (!iFrames)
            {
                
                playerCollider.enabled = true;
            }
            //Set player visible
            
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

        if (isDashing)
        {
            if (dashTimer > 0)
            {
                
                playerRB.AddForce(direction.normalized * dashForce, ForceMode2D.Force);
                dashTimer -= Time.fixedDeltaTime;
            }
            else
            {
                isDashing = false;
                dashActivated = false;
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
