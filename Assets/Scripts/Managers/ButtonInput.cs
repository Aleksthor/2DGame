using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInput : SingletonMonoBehaviour<ButtonInput>
{

    LocalPlayerScript localPlayerScript;
    AnimationManager playerAnimator;

    [Header("Private Variables")]
    [SerializeField]
    Vector2 movement;
    [SerializeField]
    bool dash;
    [SerializeField]
    bool sneak;
    [SerializeField]
    bool shield;
    [SerializeField]
    bool attack;
    [SerializeField]
    bool inventoryOpen = false;
    float triggerTimer = 0.3f;
    float triggerClock = 0f;

    [SerializeField] private bool isLocomotion = true;

    //--------------------

    private void Start()
    {
        localPlayerScript = PlayerSingleton.instance.gameObject.GetComponent<LocalPlayerScript>();
        playerAnimator = AnimationManager.Instance;
    }
    private void Update()
    {
        ButtonInputs();
    }


    //--------------------


    void ButtonInputs()
    {
        if (isLocomotion)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            dash = Input.GetButton("Jump");

            sneak = Input.GetButton("Running");
            shield = Input.GetButton("Fire2");
            attack = Input.GetButton("Fire1");
        }
        else
        {
            movement = new Vector2 (0,0);
        }
        playerAnimator.SetWalkingSpeedAnimation(movement);

        #region Open Inventory 
        if (Input.GetButton("Inventory") && triggerClock <= 0)
        {
            if (inventoryOpen)
            {
                HUDSingleton.instance.transform.Find("Inventory").gameObject.SetActive(false);
                inventoryOpen = false;
                isLocomotion = true;

            }
            else
            {
                HUDSingleton.instance.transform.Find("Inventory").gameObject.SetActive(true);
                InventoryManager.Instance.UpdateInventoryTab(0);
                InventoryManager.Instance.SpawnCurrentWeapons();
                inventoryOpen = true;
                isLocomotion = false;
            }


            triggerClock = triggerTimer;
        }
        if (triggerClock > 0)
        {
            triggerClock -= Time.deltaTime;
        }
        #endregion

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


    public void SetLocomotion(bool input)
    {
        isLocomotion = input;
    }

    public bool GetLocomotion()
    {
        return isLocomotion;
    }

}
