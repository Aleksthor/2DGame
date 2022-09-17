using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    // 3 current abilites
    public Ability ability1;
    public Ability ability2;
    public Ability ability3;

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




    public void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerObject = playerManager.GetPlayer();
        GameEvents.current.OnChangeWeaponAbility += ChangeAbilities;
        
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
                    if (Input.GetKeyDown(key1) && !(cooldownTime1 > 0))
                    {
                        cooldownIcon1.value = 1f;
                        ability1.Activate(playerObject);
                        state1 = AbilityState.active;
                        activeTime1 = ability1.activeTime;
                        cooldownTime1 = ability1.cooldownTime;
                    }

                    if (cooldownTime1 > 0f)
                    {
                        cooldownTime1 -= Time.deltaTime;
                    }
                    cooldownIcon1.value = cooldownTime1 / ability1.cooldownTime;
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
        }

        #endregion

        #region Ability2

        if (ability2 != null)
        {
            switch (state2)
            {
                case AbilityState.ready:
                    if (Input.GetKeyDown(key2) && !(cooldownTime2 > 0))
                    {
                        ability2.Activate(playerObject);
                        cooldownIcon2.value = 1f;
                        state2 = AbilityState.active;
                        activeTime2 = ability2.activeTime;
                        cooldownTime2 = ability2.cooldownTime;
                    }
                    if (cooldownTime2 >= 0f)
                    {
                        cooldownTime2 -= Time.deltaTime;
                    }
                    cooldownIcon2.value = cooldownTime2 / ability2.cooldownTime;
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
        }

        #endregion

        #region Ability3
        if (ability3 != null)
        {
            switch (state3)
            {
                case AbilityState.ready:
                    if (Input.GetKeyDown(key3) && !(cooldownTime1 > 0))
                    {
                        cooldownIcon3.value = 1f;
                        ability3.Activate(playerObject);
                        state3 = AbilityState.active;
                        activeTime3 = ability3.activeTime;
                        cooldownTime3 = ability3.cooldownTime;
                    }
                    if (cooldownTime2 >= 0f)
                    {
                        cooldownTime2 -= Time.deltaTime;
                    }
                    cooldownIcon2.value = cooldownTime2 / ability2.cooldownTime;
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



    private void ChangeAbilities(Ability Ability1, Ability Ability2, Ability Ability3, Sprite Sprite1, Sprite Sprite2, Sprite Sprite3)
    {
        if(ability1 != null)
        {
            ability1.DeActivate(playerObject);
            state1 = AbilityState.ready;
        }
        if (ability2 != null)
        {
            ability2.DeActivate(playerObject);
            state2 = AbilityState.ready;
        }
        if (ability3 != null)
        {
            ability3.DeActivate(playerObject);
            state3 = AbilityState.ready;
        }
        float temp1 = oldCooldownTime1;
        float temp2 = oldCooldownTime2;
        float temp3 = oldCooldownTime3;
        oldCooldownTime1 = cooldownTime1;
        oldCooldownTime2 = cooldownTime2;
        oldCooldownTime3 = cooldownTime3;
        cooldownTime1 = temp1;
        cooldownTime2 = temp2;
        cooldownTime3 = temp3;
  


        if (Ability1 != null)
        {
            ability1 = Ability1;
            cooldownIcon1.value = cooldownTime1 / ability1.cooldownTime;

        }
        else
        {
            ability1 = null;

        }
        if (Ability2 != null)
        {
            ability2 = Ability2;
            cooldownIcon2.value = cooldownTime2 / ability2.cooldownTime;
        }
        else
        {
            ability2 = null;
        }
        if (ability3 != null)
        {
            ability3 = Ability3;
            cooldownIcon3.value = cooldownTime3 / ability3.cooldownTime;
        }
        else
        {
            ability3 = null;
        }
        if (Sprite1 != null)
        {
            cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = Sprite1;
        }
        else
        {
            cooldownIcon1.transform.Find("Background").GetComponent<Image>().sprite = null;
        }
        if(Sprite2 != null)
        {
            cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = Sprite2;
        }
        else
        {
            cooldownIcon2.transform.Find("Background").GetComponent<Image>().sprite = null;
        }
        if (Sprite3 != null)
        {
            cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = Sprite3;
        }
        else
        {
            cooldownIcon3.transform.Find("Background").GetComponent<Image>().sprite = null;
        }

    }
}
