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
    [SerializeField] float armor = 0;

    [Header("Stamina Stats")]
    [SerializeField] float stamina = 50;
    [SerializeField] float maxStamina = 50;

    [Header("Mana Stats")]
    [SerializeField] float mana = 50;
    [SerializeField] float maxMana = 50;
    [SerializeField] float baseManaRegen = 1.2f;
    [SerializeField] float baseManaRegenSpeed = 2;
    [SerializeField] float baseManaRegenClock = 2;



    private void Awake()
    {
        playerAnimator = playerObject.GetComponent<Animator>();
        playerHUD = FindObjectOfType<HUD>();
        movementManager = FindObjectOfType<Movement>();
    }


    private void Start()
    {
        GameEvents.current.OnEnemyWeaponCollission += Hit;
        GameEvents.current.OnUpdateArmor += UpdateArmorStat;
    }


    void Update()
    {
        BaseManaRegen();


        // run this last to clamp values
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

    private void UpdateArmorStat(float Armor)
    {
        armor = Armor;
    }

    void BaseManaRegen()
    {
        baseManaRegenClock += Time.deltaTime;
        if (baseManaRegenClock > baseManaRegenSpeed)
        {
            baseManaRegenClock = 0;
            SetManaValue(baseManaRegen);
        }
    }


    private void Hit(float damage, float knockbackForce)
    {

        if (movementManager.isShielding)
        {
            health -= Mathf.Clamp(damage, damage / 5, damage - armor) * 0.5f;
        }
        else
        {
            health -= Mathf.Clamp(damage, damage / 5, damage - armor);
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

    public void SetManaValue(float value)
    {
        mana += value;
    }
    public float GetManaValue()
    {
        return mana;
    }
}

