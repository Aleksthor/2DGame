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

    
    [SerializeField] float cooldownTime1;
    [SerializeField] float activeTime1;
    [SerializeField] Slider cooldownIcon1;

    [SerializeField] float cooldownTime2;
    [SerializeField] float activeTime2;
    [SerializeField] Slider cooldownIcon2;

    //[SerializeField] float cooldownTime3;
    //[SerializeField] float activeTime3;


    enum AbilityState
    {
        ready,
        active,
        cooldown
    };

    AbilityState state1 = AbilityState.ready;
    AbilityState state2 = AbilityState.ready;

    //AbilityState state3 = AbilityState.ready;

    public KeyCode key1;
    public KeyCode key2;
    //public KeyCode key3;



    // Update is called once per frame
    void Update()
    {
        switch(state1)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key1))
                {
                    cooldownIcon1.value = 1f;
                    ability1.Activate(gameObject);
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
                    ability1.DeActivate(gameObject);
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


        switch (state2)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key2))
                {
                    ability2.Activate(gameObject);
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
                    ability2.DeActivate(gameObject);
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
}
