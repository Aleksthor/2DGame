using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{



    [Header("Moveable Object")]
    GameObject playerObject;
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
    [SerializeField] public float staminaPerHit = 22.5f;

    [Header("Mana Stats")]
    [SerializeField] float mana = 50;
    [SerializeField] float maxMana = 50;
    [SerializeField] float baseManaRegen = 1.2f;
    [SerializeField] float baseManaRegenSpeed = 2;
    [SerializeField] float baseManaRegenClock = 2;


    private void Start()
    {
        playerObject = PlayerSingleton.instance.gameObject;
        playerAnimator = playerObject.GetComponent<Animator>();
        playerHUD = HUD.Instance;
        movementManager = Movement.Instance;

        GameEvents.current.OnEnemyWeaponCollission += Hit;
        GameEvents.current.OnUpdateArmor += UpdateArmorStat;
        GameEvents.current.OnUseStamina += UseStamina;
        GameEvents.current.OnUseMana += UseMana;
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


    private void UseStamina(float Stamina)
    {
        stamina -= Stamina;
    }


    public float GetStamina()
    {
        return stamina;
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
        float hitDamage = 0;
        if (movementManager.isShielding)
        {
            hitDamage = damage - armor;
            hitDamage = Mathf.Clamp(hitDamage, damage / 5, damage);
            health -= hitDamage * 0.5f;
        }
        else
        {
            hitDamage = damage - armor;
            hitDamage = Mathf.Clamp(hitDamage, damage / 5, damage);
            health -= hitDamage;
        }
        playerAnimator.SetTrigger("Hit");
        if (health <= 0)
        {
            playerAnimator.SetBool("Dead", true);
        }
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

    private void UseMana(float Mana)
    {
        mana -= Mana;
        if (mana < 0 )
        {
            mana = 0;   
        }
    }
    public float GetManaValue()
    {
        return mana;
    }
}

