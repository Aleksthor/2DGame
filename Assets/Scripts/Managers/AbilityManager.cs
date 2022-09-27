using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : SingletonMonoBehaviour<AbilityManager>
{

    // 3 current abilites
    [SerializeReference] public Ability ability1;
    [SerializeReference] public Ability ability2;
    [SerializeReference] public Ability ability3;

    private PlayerManager playerManager;
    private GameObject playerObject;

    
    [SerializeField] float cooldownTime1;
    [SerializeField] float activeTime1;
    [SerializeField] Slider cooldownIcon1;
    [SerializeField] float oldCooldownTime1;

    [SerializeField] float cooldownTime2;
    [SerializeField] float activeTime2;
    [SerializeField] Slider cooldownIcon2;
    [SerializeField] float oldCooldownTime2;

    [SerializeField] float cooldownTime3;
    [SerializeField] float activeTime3;
    [SerializeField] Slider cooldownIcon3;
    [SerializeField] float oldCooldownTime3;

    [Header("UI Buff Icon")]
    [SerializeField] Transform buffParent;
    [SerializeField] GameObject uiObject;



    enum AbilityState
    {
        ready,
        active,
        cooldown
    };

    AbilityState state1 = AbilityState.ready;
    AbilityState state2 = AbilityState.ready;
    AbilityState state3 = AbilityState.ready;

    public KeyCode key1;
    public KeyCode key2;
    public KeyCode key3;



    GameObject buffIcon1;
    GameObject buffIcon2;
    GameObject buffIcon3;



    private AbilityDash dash;
    private AbilityDashBehindEnemy dashBehindEnemy;
    private AbilityDashBackwards dashBackwards;
    private AbilityFirePit firePit;
    private AbilityWaterSlam waterSlam;
    private AbilityBuffDefense buffDefense;

    public Sprite dashSprite;
    public Sprite dashBehindEnemySprite;
    public Sprite dashBackwardsSprite;
    public Sprite firePitSprite;
    public Sprite waterSlamSprite;
    public Sprite buffDefenseSprite;

    public void Start()
    {
        
        playerManager = PlayerManager.Instance;
        playerObject = PlayerSingleton.instance.gameObject;
        GameEvents.current.OnChangeWeaponAbility += ChangeAbilities;
        cooldownIcon1 = HUDSingleton.instance.transform.Find("Ability1").GetComponent<Slider>();
        cooldownIcon2 = HUDSingleton.instance.transform.Find("Ability2").GetComponent<Slider>();
        cooldownIcon3 = HUDSingleton.instance.transform.Find("Ability3").GetComponent<Slider>();

        dash = transform.GetComponent<AbilityDash>();
        dashBehindEnemy = transform.GetComponent<AbilityDashBehindEnemy>();
        dashBackwards = transform.GetComponent<AbilityDashBackwards>();
        firePit = transform.GetComponent<AbilityFirePit>();
        waterSlam = transform.GetComponent<AbilityWaterSlam>();
        buffDefense = transform.GetComponent<AbilityBuffDefense>();

        ability1 = dash;
        ability2 = dash;
        ability3 = dash;
    }



    // Update is called once per frame
    void Update()
    {


        #region Ablity1
        if (ability1 != null)
        {
            switch (state1)
            {
                case AbilityState.ready:
                    if (Input.GetKeyDown(key1) && (cooldownTime1 <= 0) && (playerManager.GetManaValue() - ability1.manaCost) > 0) 
                    {
                        Debug.Log("Ability 1 is Active");
                        cooldownIcon1.value = 1f;
                        ability1.Activate(playerObject);
                        state1 = AbilityState.active;
                        activeTime1 = ability1.activeTime;
                        cooldownTime1 = ability1.cooldownTime;


                        if (ability1.hasBuff)
                        {
                            buffIcon1 = Instantiate(uiObject, buffParent);
                            buffIcon1.GetComponent<Image>().sprite = ability1.buffIcon;
                        }

                    }

                    if (cooldownTime1 > 0f)
                    {
                        cooldownTime1 -= Time.deltaTime;
                    }
                    if (playerManager.GetManaValue() - ability1.manaCost < 0)
                    {
                        cooldownIcon1.value = 1f;
                    }
                    else
                    {
                        cooldownIcon1.value = cooldownTime1 / ability1.cooldownTime;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime1 > 0)
                    {
                        activeTime1 -= Time.deltaTime;
                    }
                    else
                    {
                        ability1.Trigger(playerObject);
                        ability1.DeActivate(playerObject);
                        Debug.Log("Ability 1 Deactivated");
                        if (ability1.hasBuff)
                        {
                            foreach(Transform child in buffParent)
                            {
                                
                                if (child.GetComponent<Image>().sprite == buffIcon1.GetComponent<Image>().sprite)
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                        }

                        state1 = AbilityState.cooldown;

                    }
                    break;
                case AbilityState.cooldown:
                    if (cooldownTime1 > 0)
                    {
                        cooldownIcon1.value = cooldownTime1 / ability1.cooldownTime;
                        cooldownTime1 -= Time.deltaTime;
                    }
                    else
                    {
                        state1 = AbilityState.ready;

                    }
                    break;
                default:
                    break;
            }
            cooldownIcon1.value = cooldownTime1 / ability1.cooldownTime;

        }
        else
        {
            cooldownIcon1.value = 0f;
        }

        #endregion

        #region Ability2

        if (ability2 != null)
        {
            switch (state2)
            {
                case AbilityState.ready:
                    if (Input.GetKeyDown(key2) && (cooldownTime2 <= 0) && (playerManager.GetManaValue() - ability2.manaCost) > 0)
                    {
                        Debug.Log("Ability 2 is Active");
                        ability2.Activate(playerObject);
                        cooldownIcon2.value = 1f;
                        state2 = AbilityState.active;
                        activeTime2 = ability2.activeTime;
                        cooldownTime2 = ability2.cooldownTime;


                        if (ability2.hasBuff)
                        {
                            buffIcon2 = Instantiate(uiObject, buffParent);
                            buffIcon2.GetComponent<Image>().sprite = ability2.buffIcon;
                        }
                    }
                    if (cooldownTime2 >= 0f)
                    {
                        cooldownTime2 -= Time.deltaTime;
                    }
                    if (playerManager.GetManaValue() - ability2.manaCost < 0)
                    {
                        cooldownIcon2.value = 1f;
                    }
                    else
                    {
                        cooldownIcon2.value = cooldownTime2 / ability2.cooldownTime;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime2 > 0)
                    {
                        activeTime2 -= Time.deltaTime;
                    }
                    else
                    {
                        ability2.Trigger(playerObject);
                        ability2.DeActivate(playerObject);
                        Debug.Log("Ability 2 Deactivated");
                        if (ability2.hasBuff)
                        {
                            foreach (Transform child in buffParent)
                            {
                                if (child.GetComponent<Image>().sprite == buffIcon2.GetComponent<Image>().sprite)
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                        }
                        state2 = AbilityState.cooldown;

                    }
                    break;
                case AbilityState.cooldown:
                    if (cooldownTime2 >= 0)
                    {
                        cooldownIcon2.value = cooldownTime2 / ability2.cooldownTime;
                        cooldownTime2 -= Time.deltaTime;
                    }
                    else
                    {
                        state2 = AbilityState.ready;

                    }
                    break;
                default:
                    break;
            }
            cooldownIcon2.value = cooldownTime2 / ability2.cooldownTime;
        }
        else
        {
            cooldownIcon2.value = 0f;
        }
        #endregion

        #region Ability3
        if (ability3 != null)
        {
            switch (state3)
            {
                case AbilityState.ready:
                    if (Input.GetKeyDown(key3) && (cooldownTime3 <= 0) && (playerManager.GetManaValue() - ability3.manaCost) > 0)
                    {
                        Debug.Log("Ability 3 is Active");
                        cooldownIcon3.value = 1f;
                        ability3.Activate(playerObject);
                        state3 = AbilityState.active;
                        activeTime3 = ability3.activeTime;
                        cooldownTime3 = ability3.cooldownTime;

                        if (ability3.hasBuff)
                        {
                            buffIcon3 = Instantiate(uiObject, buffParent);
                            buffIcon3.GetComponent<Image>().sprite = ability3.buffIcon;
                        }
                    }
                    if (cooldownTime3 >= 0f)
                    {
                        cooldownTime3 -= Time.deltaTime;
                    }
                    if (playerManager.GetManaValue() - ability3.manaCost < 0)
                    {
                        cooldownIcon3.value = 1f;
                    }
                    else
                    {
                        cooldownIcon3.value = cooldownTime3 / ability3.cooldownTime;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime3 > 0)
                    {
                        activeTime3 -= Time.deltaTime;
                    }
                    else
                    {
                        ability3.Trigger(playerObject);
                        ability3.DeActivate(playerObject);
                        Debug.Log("Ability 3 Deactivated");

                        if (ability3.hasBuff)
                        {
                            foreach (Transform child in buffParent)
                            {
                                if (child.GetComponent<Image>().sprite == buffIcon3.GetComponent<Image>().sprite)
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                        }
                        state3 = AbilityState.cooldown;

                    }
                    break;
                case AbilityState.cooldown:
                    if (cooldownTime3 > 0)
                    {
                        cooldownIcon3.value = cooldownTime3 / ability3.cooldownTime;
                        cooldownTime3 -= Time.deltaTime;
                    }
                    else
                    {
                        state3 = AbilityState.ready;

                    }
                    break;
                default:
                    break;
            }
            cooldownIcon3.value = cooldownTime3 / ability3.cooldownTime;
        }
        else
        {
            cooldownIcon3.value = 0f;
        }
        if (oldCooldownTime1 > 0)
        {
            oldCooldownTime1 -= Time.deltaTime;
        }
        if (oldCooldownTime2 > 0)
        {
            oldCooldownTime2 -= Time.deltaTime;
        }
        if (oldCooldownTime3 > 0)
        {
            oldCooldownTime3 -= Time.deltaTime;
        }


        #endregion


    }



    private void ChangeAbilities(Weapon.AbilityType Ability1, Weapon.AbilityType Ability2, Weapon.AbilityType Ability3)
    {


        float temp1 = oldCooldownTime1;
        float temp2 = oldCooldownTime2;
        float temp3 = oldCooldownTime3;
        oldCooldownTime1 = cooldownTime1;
        oldCooldownTime2 = cooldownTime2;
        oldCooldownTime3 = cooldownTime3;
        cooldownTime1 = temp1;
        cooldownTime2 = temp2;
        cooldownTime3 = temp3;

        #region Ability1

        ability1.DeActivate(playerObject);
        Debug.Log("Ability 1 Deactivated");
        if (ability1.hasBuff)
        {
            foreach (Transform child in buffParent)
            {
                Debug.Log(child);
                if (child.GetComponent<Image>().sprite == buffIcon1.GetComponent<Image>().sprite)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        state1 = AbilityState.ready;

        switch (Ability1)
        {
            case Weapon.AbilityType.Dash:
                ability1 = dash;
                cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = dashSprite;
                break;
            case Weapon.AbilityType.DashBehindEnemy:
                ability1 = dashBehindEnemy;
                cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = dashBehindEnemySprite;
                break;
            case Weapon.AbilityType.DashBackwards:
                ability1 = dashBackwards;
                cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = dashBackwardsSprite;
                break;
            case Weapon.AbilityType.FirePit:
                ability1 = firePit;
                cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = firePitSprite;
                break;
            case Weapon.AbilityType.WaterWaterSlam:
                ability1 = waterSlam;
                cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = waterSlamSprite;
                break;
            case Weapon.AbilityType.BuffDefense:
                ability1 = buffDefense;
                cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = buffDefenseSprite;
                break;
            default:
                cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = null;
                break;
        }


        #endregion

        #region Ability2

        ability2.DeActivate(playerObject);
        Debug.Log("Ability 2 Deactivated");
        if (ability2.hasBuff)
        {
            foreach (Transform child in buffParent)
            {
                Debug.Log(child);
                if (child.GetComponent<Image>().sprite == buffIcon2.GetComponent<Image>().sprite)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        state2 = AbilityState.ready;



        switch (Ability2)
        {
            case Weapon.AbilityType.Dash:
                ability2 = dash;
                cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = dashSprite;
                break;
            case Weapon.AbilityType.DashBehindEnemy:
                ability2 = dashBehindEnemy;
                cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = dashBehindEnemySprite;
                break;
            case Weapon.AbilityType.DashBackwards:
                ability2 = dashBackwards;
                cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = dashBackwardsSprite;
                break;
            case Weapon.AbilityType.FirePit:
                ability2 = firePit;
                cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = firePitSprite;
                break;
            case Weapon.AbilityType.WaterWaterSlam:
                ability2 = waterSlam;
                cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = waterSlamSprite;
                break;
            case Weapon.AbilityType.BuffDefense:
                ability2 = buffDefense;
                cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = buffDefenseSprite;
                break;
            default:
                cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = null;
                break;
        }

        #endregion

        #region Ability3

        ability3.DeActivate(playerObject);
        Debug.Log("Ability 3 Deactivated");
        if (ability3.hasBuff)
        {
            foreach (Transform child in buffParent)
            {
                Debug.Log(child);
                if (child.GetComponent<Image>().sprite == buffIcon3.GetComponent<Image>().sprite)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        state3 = AbilityState.ready;


        switch (Ability3)
        {
            case Weapon.AbilityType.Dash:
                ability3 = dash;
                cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = dashSprite;
                break;
            case Weapon.AbilityType.DashBehindEnemy:
                ability3 = dashBehindEnemy;
                cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = dashBehindEnemySprite;
                break;
            case Weapon.AbilityType.DashBackwards:
                ability3 = dashBackwards;
                cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = dashBackwardsSprite;
                break;
            case Weapon.AbilityType.FirePit:
                ability3 = firePit;
                cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = firePitSprite;
                break;
            case Weapon.AbilityType.WaterWaterSlam:
                ability3 = waterSlam;
                cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = waterSlamSprite;
                break;
            case Weapon.AbilityType.BuffDefense:
                ability3 = buffDefense;
                cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = buffDefenseSprite;
                break;
            default:
                cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = null;
                break;
        }

        #endregion





    }


    public void ResetCooldowns()
    {
        cooldownTime1 = 0;
        cooldownTime2 = 0;
        cooldownTime3 = 0;
        oldCooldownTime1 = 0;
        oldCooldownTime2 = 0;
        oldCooldownTime3 = 0;
    }
}
