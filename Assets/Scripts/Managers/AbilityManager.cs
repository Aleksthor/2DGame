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

    [SerializeField] float cooldownTime2;
    [SerializeField] float activeTime2;
    [SerializeField] Slider cooldownIcon2;

    [SerializeField] float cooldownTime3;
    [SerializeField] float activeTime3;
    [SerializeField] Slider cooldownIcon3;


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
                    if (Input.GetKeyDown(key1))
                    {
                        cooldownIcon1.value = 1f;
                        ability1.Activate(playerObject);
                        state1 = AbilityState.active;
                        activeTime1 = ability1.activeTime;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime1 > 0)
                    {
                        activeTime1 -= Time.deltaTime;
                    }
                    else
                    {
                        ability1.DeActivate(playerObject);
                        state1 = AbilityState.cooldown;
                        cooldownTime1 = ability1.cooldownTime;
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
                    if (Input.GetKeyDown(key2))
                    {
                        ability2.Activate(playerObject);
                        cooldownIcon2.value = 1f;
                        state2 = AbilityState.active;
                        activeTime2 = ability2.activeTime;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime2 > 0)
                    {
                        activeTime2 -= Time.deltaTime;
                    }
                    else
                    {
                        ability2.DeActivate(playerObject);
                        state2 = AbilityState.cooldown;
                        cooldownTime2 = ability2.cooldownTime;
                    }
                    break;
                case AbilityState.cooldown:
                    if (cooldownTime2 > 0)
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
                    if (Input.GetKeyDown(key3))
                    {
                        cooldownIcon3.value = 1f;
                        ability3.Activate(playerObject);
                        state3 = AbilityState.active;
                        activeTime3 = ability3.activeTime;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime3 > 0)
                    {
                        activeTime3 -= Time.deltaTime;
                    }
                    else
                    {
                        ability3.DeActivate(playerObject);
                        state3 = AbilityState.cooldown;
                        cooldownTime3 = ability3.cooldownTime;
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
        

        #endregion


    }



    private void ChangeAbilities(Ability Ability1, Ability Ability2, Ability Ability3, Sprite Sprite1, Sprite Sprite2, Sprite Sprite3)
    {
        if (Ability1 != null)
        {
            ability1 = Ability1;
        }
        else
        {
            ability1 = null;

        }
        if (Ability2 != null)
        {
            ability2 = Ability2;
        }
        else
        {
            ability2 = null;
        }
        if (ability3 != null)
        {
            ability3 = Ability3;
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
