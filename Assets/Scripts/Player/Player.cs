using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Moveable Object")]
    [SerializeField] GameObject playerObject;
    Animator playerAnimator;
    HUD playerHUD;
    Movement movementManager;


    [Header("Health Stats")]
    [SerializeField] float health = 50;
    [SerializeField] float maxHealth = 50;

    [Header("Stamina Stats")]
    [SerializeField] float stamina = 50;
    [SerializeField] float maxStamina = 50;

    [Header("Mana Stats")]
    [SerializeField] float mana = 50;
    [SerializeField] float maxMana = 50;




    private void Awake()
    {
        playerAnimator = playerObject.GetComponent<Animator>();
        playerHUD = FindObjectOfType<HUD>();
        movementManager = FindObjectOfType<Movement>();
    }

    void Update()
    {
        UpdateStats();
    }

    void UpdateStats()
    { 
        // Update HUD with valid Info
        playerHUD.healthSliderMaxValue = maxHealth;
        playerHUD.healthSliderCurrent = health;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        playerHUD.staminaSliderMaxValue = maxStamina;
        playerHUD.staminaSliderCurrent = stamina;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }

        playerHUD.magicSliderMaxValue = maxMana;
        playerHUD.magicSliderCurrent = mana;

        if (mana > maxMana)
        {
            mana = maxMana;
        }

    }


    public void Hit(float damage)
    {

        if (movementManager.isShielding)
        {
            health -= damage * 0.5f;
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            playerAnimator.SetBool("Dead", true);
        }
    }




    public GameObject GetPlayer()
    {
        return playerObject;
    }

    public Animator GetPlayerAnimator()
    {
        return playerAnimator;
    }

    public void SetStaminaValue(float value)
    {
        stamina += value;
    }
}

