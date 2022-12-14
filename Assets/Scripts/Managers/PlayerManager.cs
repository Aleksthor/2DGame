using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : SingletonMonoBehaviour<PlayerManager>, IDataPersistence
{



    [Header("Moveable Object")]
    GameObject playerObject;
    Animator playerAnimator;
    HUD playerHUD;
    Movement movementManager;
    


    [Header("Health Stats")]
    [SerializeField] float health = 50;
    [SerializeField] float maxHealth = 50;
    [SerializeField] float levelMaxHealth = 50;
    [SerializeField] float bonusMaxHealth;
    [SerializeField] float baseHealthRegenSpeed = 5;
    [SerializeField] float baseHealthRegen = 2f;

    [Header("Stamina Stats")]
    [SerializeField] float stamina = 50;
    [SerializeField] public float maxStamina = 50;
    [SerializeField] public float staminaPerHit = 22.5f;

    [Header("Mana Stats")]
    [SerializeField] float mana = 50;

    [SerializeField] float maxMana = 50;
    [SerializeField] float baseManaRegen = 1.2f;
    [SerializeField] float baseManaRegenSpeed = 2;
    [SerializeField] float baseManaRegenClock = 2;

    [Header("XP")]
    [SerializeField] public int currentLevel;
    [SerializeField] public int maxLevel;
    [SerializeField] public float currentXPAmount;
    [SerializeField] public float neededXPToLevel;
    [SerializeField] public float[] levelXPArray;


    [Header("Equipped Item Stats")]
    [SerializeField] public float meleeDamage;
    [SerializeField] public float critDamage;
    [SerializeField] public float critRate;
    [SerializeField] public float knockbackForce;
    [SerializeField] float armor;
    [SerializeField] float magicDamage;
    [SerializeField] float manaCost;
    [SerializeField] float magicShotForce;
    [SerializeField] float slowDebuff;
    [SerializeField] float slowDownLength;
    [SerializeField] Vector2 localPosition;
    [SerializeField] int poise;
    [SerializeField] float bonusHealth;

    [SerializeField] Slider xpSlider;
    [SerializeField] TMPro.TextMeshProUGUI levelText;

    [Header("Buff Stats")]
    [SerializeField] List<float> defenseBuff;
    [SerializeField] List<float> defenseBuffTime;
    [SerializeField] float baseDefense;
    private bool defenseBuffActive = false;

    public bool canBeHit = true;

    private Transform deathScreen;
    // Where to spawn on a new scene

    public Vector2 spawnPosition;
    private LocalPlayerScript localPlayerScript;

    public void Respawn()
    {
        health = maxHealth;
        mana = maxMana;
        stamina = maxStamina;

        localPlayerScript.StopAttack();

    }



    private void Start()
    {
        playerObject = PlayerSingleton.instance.gameObject;
        playerAnimator = playerObject.GetComponent<Animator>();
        playerHUD = HUD.Instance;
        movementManager = Movement.Instance;
        deathScreen = HUDSingleton.instance.transform.Find("DeathScreen").transform;
        localPlayerScript = playerObject.GetComponent<LocalPlayerScript>();

        GameEvents.current.OnEnemyWeaponCollission += Hit;
        GameEvents.current.OnUpdateArmor += UpdateArmorStat;
        GameEvents.current.OnUseStamina += UseStamina;
        GameEvents.current.OnUseMana += UseMana;
        GameEvents.current.OnBuffDefense += BuffDefense;
        GameEvents.current.OnUpdateInventoryStats += UpdateInventoryStats;
        GameEvents.current.OnAbilityBuffDefense += AbilityDefenseBuff;
        GameEvents.current.OnAbilityRemoveBuffDefense += AbilityRemoveDefenseBuff;
        GameEvents.current.OnGainXP += GainXP;

        StartCoroutine(BaseHealthRegen());
    }


    void Update()
    {
        BaseManaRegen();
        health = Mathf.Clamp(health, 0, maxHealth);
        mana = Mathf.Clamp(mana, 0, maxMana);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        if (xpSlider != null)
        {
            xpSlider.value = currentXPAmount / neededXPToLevel;
        }
        if (levelText != null)
        {
            levelText.text = "Lv. " + currentLevel;
        }
        // run this last to clamp values
        UpdateStats();
        
    }


    public void LoadData(GameData data)
    {
        if (playerObject != null)
        {
            playerObject.transform.position = data.position;
        }
       
        health = data.health;
        maxHealth = data.maxHealth;
        stamina = data.stamina;
        maxStamina = data.maxStamina;
        mana = data.mana;
        maxMana = data.maxMana;
        currentLevel = data.currentLevel;
        currentXPAmount = data.currentXPAmount;
        neededXPToLevel = data.neededXpToLevel;
    }

    public void SaveData(GameData data)
    {
        data.health = health;
        data.maxHealth = maxHealth;
        data.stamina = stamina;
        data.maxStamina = maxStamina;
        data.mana = mana;
        data.maxMana = maxMana;
        data.position = playerObject.transform.position;
        data.currentLevel = currentLevel;
        data.neededXpToLevel = neededXPToLevel;
        data.currentXPAmount = currentXPAmount;
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
        if (health < 0)
        {
            health = 0f;
        }

        playerHUD.staminaSliderMaxValue = maxStamina;
        playerHUD.staminaSliderCurrent = stamina;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if (stamina < 0)
        {
            stamina = 0f;
        }


        playerHUD.magicSliderMaxValue = maxMana;
        playerHUD.magicSliderCurrent = mana;

        if (mana > maxMana)
        {
            mana = maxMana;
        }
        if (mana < 0)
        {
            mana = 0f;
        }


        #region DefenseBuff

        if (defenseBuffTime.Count > 0)
        {
            int i = 0;
            float totalBuff = 1f;
            foreach (float buff in defenseBuff)
            {
                defenseBuffTime[i] -= Time.deltaTime;

                if (defenseBuffTime[i] < 0)
                {
                    defenseBuffTime.RemoveAt(i);
                    defenseBuff.RemoveAt(i);

                }
                else
                {
                    totalBuff *= defenseBuff[i];
                }

                i++;
            }

            armor = baseDefense *= totalBuff;
        }
        else if (!defenseBuffActive)
        {
            armor = baseDefense;
        }


        #endregion

    }



    private void Hit(float damage, float knockbackForce, WeaponCollider.DamageType damageType)
    {
        float hitDamage = 0;
        if (damageType == WeaponCollider.DamageType.Physical)
        {
            if (movementManager.isShielding)
            {
                hitDamage = damage - (armor/2);
                hitDamage = Mathf.Clamp(hitDamage, damage / 5, damage);
                health -= hitDamage * 0.5f;
            }
            else
            {
                hitDamage = damage - (armor / 2);
                hitDamage = Mathf.Clamp(hitDamage, damage / 5, damage);
                health -= hitDamage;
            }
        }
        else
        {
            health -= damage;
        }


        if (canBeHit)
        {
            playerAnimator.SetTrigger("Hit");
        }


        if (health <= 0)
        {
            deathScreen.gameObject.SetActive(true);
            playerAnimator.SetBool("Dead", true);
        }
    }


    private void UpdateInventoryStats(float Damage, float MagicDamage, float KnockBackForce, float SpeedMultiplier, float SlowDownLength, float ManaCost, float Force, float CritRate, float CritDamage, Vector2 LocalPosition, int Poise, float BonusHealth)
    {
        meleeDamage = Damage;
        magicDamage = MagicDamage;
        knockbackForce = KnockBackForce;
        slowDebuff = SpeedMultiplier;
        slowDownLength = SlowDownLength;
        manaCost = ManaCost;
        magicShotForce = Force;
        critRate = CritRate;
        critDamage = CritDamage;
        localPosition = LocalPosition;
        poise = Poise;
        bonusHealth = BonusHealth;


        UpdatePlayerStats();

    }

    private void UpdateArmorStat(float Armor)
    {
        armor = Armor;
        baseDefense = Armor;
    }



    private void UpdatePlayerStats()
    {
        float Damage = meleeDamage;
        float MagicDamage = magicDamage;
        float KnockBackForce = knockbackForce;
        float SlowDebuff = slowDebuff;
        float SlowDebuffTime = slowDownLength;
        float ManaCost = manaCost;
        float ShotForce = magicShotForce;
        float CritRate = critRate;
        float CritDamage = critDamage;
        Vector2 LocalPos = localPosition;
        int Poise = poise;   
        

        GameEvents.current.ChangeStats(Damage, MagicDamage, KnockBackForce, SlowDebuff,
    SlowDebuffTime, ManaCost, ShotForce, CritRate, CritDamage, LocalPos, Poise);




        maxHealth = levelMaxHealth + bonusHealth;
    }





    private void BuffDefense(float DefenseBuff, float DefenseTime)
    {

        defenseBuff.Add(DefenseBuff);
        defenseBuffTime.Add(DefenseTime);

    }


    private void AbilityDefenseBuff(float DefenseBuff)
    {
        defenseBuffActive = true;
        armor *= DefenseBuff;
    }

    private void AbilityRemoveDefenseBuff(float DefenseBuff)
    {
        defenseBuffActive = false;
        if (defenseBuff.Count > 0)
        {
            armor /= DefenseBuff;
        }
        else
        {
            armor = baseDefense;
        }
    }


    public Animator GetPlayerAnimator()
    {
        return playerAnimator;
    }

    public void SetHealthValue(float value)
    {
        health += value;
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
        if (mana < 0)
        {
            mana = 0;
        }
    }
    public float GetManaValue()
    {
        return mana;
    }

    public void UseStamina(float Stamina)
    {
        stamina -= Stamina;
    }


    public float GetStamina()
    {
        return stamina;
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


    void GainXP(float xpGiven)
    {
        currentXPAmount += xpGiven;
        if (currentXPAmount > neededXPToLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (currentLevel + 1 >=  maxLevel)
        {
            currentLevel = maxLevel;
        }
        else
        {
            currentLevel++;

            if (levelXPArray.Length >= currentLevel)
            {
                currentXPAmount = currentXPAmount - neededXPToLevel;
                neededXPToLevel = levelXPArray[currentLevel - 1];
            }
        }



        
    }

    IEnumerator BaseHealthRegen()
    {
        health += baseHealthRegen;
        yield return new WaitForSeconds(baseHealthRegenSpeed);
        StartCoroutine(BaseHealthRegen());
    }
}

