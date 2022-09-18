using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableManager : SingletonMonoBehaviour<ConsumableManager>
{
    // 3 current abilites
    public Consumable consumable1;
    private int stackAmount1;
    public Consumable consumable2;
    private int stackAmount2;
    public Consumable consumable3;
    private int stackAmount3;


    [SerializeField] float cooldownTime1;
    [SerializeField] float activeTime1;
    [SerializeField] Slider cooldownIcon1;

    [SerializeField] float cooldownTime2;
    [SerializeField] float activeTime2;
    [SerializeField] Slider cooldownIcon2;


    [SerializeField] float cooldownTime3;
    [SerializeField] float activeTime3;
    [SerializeField] Slider cooldownIcon3;

    [Header("UI Buff Icon")]
    [SerializeField] Transform buffParent;
    [SerializeField] GameObject uiObject;



    enum ConsumableState
    {
        ready,
        active,
        cooldown
    };

    ConsumableState state1 = ConsumableState.ready;
    ConsumableState state2 = ConsumableState.ready;
    ConsumableState state3 = ConsumableState.ready;

    public KeyCode key1;
    public KeyCode key2;
    public KeyCode key3;


    GameObject buffIcon1;
    GameObject buffIcon2;
    GameObject buffIcon3;

    GameObject playerObject;


    public void Start()
    {
        playerObject = PlayerSingleton.instance.gameObject;
        cooldownIcon1 = HUDSingleton.instance.transform.Find("Potion1").transform.Find("PotionSlot1").GetComponent<Slider>();
        cooldownIcon2 = HUDSingleton.instance.transform.Find("Potion2").transform.Find("PotionSlot2").GetComponent<Slider>();
        cooldownIcon3 = HUDSingleton.instance.transform.Find("Potion3").transform.Find("PotionSlot3").GetComponent<Slider>();
    }



    // Update is called once per frame
    void Update()
    {


        #region Consumable1
        if (consumable1 != null)
        {
            switch (state1)
            {
                case ConsumableState.ready:
                    if (Input.GetKeyDown(key1) && !(cooldownTime1 > 0) && stackAmount1 > 0)
                    {
                        Debug.Log("Consumable 1 Activated");
                        stackAmount1 -= 1;
                        HUDSingleton.instance.transform.Find("Potion1").Find("PotionSlot1").Find("ItemUI(Clone)").transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount1.ToString();
                        consumable1.Activate(playerObject);
                        cooldownIcon1.value = 1f;
                        state1 = ConsumableState.active;
                        activeTime1 = consumable1.activeTime;
                        cooldownTime1 = consumable1.cooldownTime;


                        if (consumable1.hasBuff)
                        {
                            buffIcon2 = Instantiate(uiObject, buffParent);
                            buffIcon2.GetComponent<Image>().sprite = consumable1.buffIcon;
                        }
                    }
                    if (cooldownTime1 >= 0f)
                    {
                        cooldownTime1 -= Time.deltaTime;
                    }
                    if (consumable1.stackAmount <= 0)
                    {
                        consumable1 = null;
                    }

                    break;
                case ConsumableState.active:
                    if (activeTime1 > 0)
                    {
                        activeTime1 -= Time.deltaTime;
                    }
                    else
                    {
                        consumable1.Trigger(playerObject);
                        consumable1.DeActivate(playerObject);
                        Debug.Log("Consumable 1 Deactivated");
                        if (consumable1.hasBuff)
                        {
                            foreach (Transform child in buffParent)
                            {

                                if (child.GetComponent<Image>().sprite == buffIcon1.GetComponent<Image>().sprite)
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                        }

                        state1 = ConsumableState.cooldown;

                    }
                    break;
                case ConsumableState.cooldown:
                    if (cooldownTime1 > 0)
                    {
                        
                        cooldownTime1 -= Time.deltaTime;
                    }
                    else
                    {
                        state1 = ConsumableState.ready;

                    }
                    break;
                default:
                    break;
            }
            if (consumable1 != null)
            cooldownIcon1.value = cooldownTime1 / consumable1.cooldownTime;
        }


        #endregion

        #region Consumable2

        if (consumable2 != null)
        {
            switch (state2)
            {
                case ConsumableState.ready:
                    if (Input.GetKeyDown(key2) && !(cooldownTime2 > 0) && stackAmount2 > 0)
                    {
                        Debug.Log("Consumable 2 Activated");
                        stackAmount2 -= 1;
                        HUDSingleton.instance.transform.Find("Potion2").Find("PotionSlot2").Find("ItemUI(Clone)").transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount2.ToString();
                        consumable2.Activate(playerObject);
                        cooldownIcon2.value = 1f;
                        state2 = ConsumableState.active;
                        activeTime2= consumable2.activeTime;
                        cooldownTime2 = consumable2.cooldownTime;


                        if (consumable2.hasBuff)
                        {
                            buffIcon2 = Instantiate(uiObject, buffParent);
                            buffIcon2.GetComponent<Image>().sprite = consumable2.buffIcon;
                        }
                    }
                    if (cooldownTime2 >= 0f)
                    {
                        cooldownTime2 -= Time.deltaTime;
                    }

                    break;
                case ConsumableState.active:
                    if (activeTime2 > 0)
                    {
                        activeTime2 -= Time.deltaTime;
                    }
                    else
                    {
                        consumable2.Trigger(playerObject);
                        consumable2.DeActivate(playerObject);
                        Debug.Log("Consumable 2 Deactivated");
                        if (consumable2.hasBuff)
                        {
                            foreach (Transform child in buffParent)
                            {

                                if (child.GetComponent<Image>().sprite == buffIcon2.GetComponent<Image>().sprite)
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                        }

                        state2 = ConsumableState.cooldown;

                    }
                    break;
                case ConsumableState.cooldown:
                    if (cooldownTime2 > 0)
                    {
                        
                        cooldownTime2-= Time.deltaTime;
                    }
                    else
                    {
                        state2 = ConsumableState.ready;

                    }
                    break;
                default:
                    break;
            }
            cooldownIcon2.value = cooldownTime2 / consumable2.cooldownTime;
        }

        #endregion

        #region Consumable3
        if (consumable3 != null)
        {
            switch (state3)
            {
                case ConsumableState.ready:
                    if (Input.GetKeyDown(key3) && !(cooldownTime3 > 0) && stackAmount3 > 0)
                    {
                        Debug.Log("Consumable 3 Activated");
                        stackAmount3 -= 1;
                        HUDSingleton.instance.transform.Find("Potion3").Find("PotionSlot3").Find("ItemUI(Clone)").transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount3.ToString();
                        consumable3.Activate(playerObject);
                        cooldownIcon3.value = 1f;
                        state3 = ConsumableState.active;
                        activeTime3 = consumable3.activeTime;
                        cooldownTime3 = consumable3.cooldownTime;


                        if (consumable3.hasBuff)
                        {
                            buffIcon3 = Instantiate(uiObject, buffParent);
                            buffIcon3.GetComponent<Image>().sprite = consumable3.buffIcon;
                        }
                    }
                    if (cooldownTime3 >= 0f)
                    {
                        cooldownTime3 -= Time.deltaTime;
                    }

                    break;
                case ConsumableState.active:
                    if (activeTime3 > 0)
                    {
                        activeTime3 -= Time.deltaTime;
                    }
                    else
                    {
                        consumable3.Trigger(playerObject);
                        consumable3.DeActivate(playerObject);
                        Debug.Log("Consumable 3 Deactivated");
                        if (consumable3.hasBuff)
                        {
                            foreach (Transform child in buffParent)
                            {

                                if (child.GetComponent<Image>().sprite == buffIcon3.GetComponent<Image>().sprite)
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                        }

                        state3 = ConsumableState.cooldown;

                    }
                    break;
                case ConsumableState.cooldown:
                    if (cooldownTime3 > 0)
                    {
                        
                        cooldownTime3 -= Time.deltaTime;
                    }
                    else
                    {
                        state3 = ConsumableState.ready;

                    }
                    break;
                default:
                    break;
            }
            cooldownIcon3.value = cooldownTime3 / consumable3.cooldownTime;
        }

        #endregion


    }


    public void ChangeConsumable1(Consumable consumable)
    {

        stackAmount1 = consumable.stackAmount;
        consumable1 = consumable;

    }
    public void ChangeConsumable2(Consumable consumable)
    {


        stackAmount2 = consumable.stackAmount;
        consumable2 = consumable;

    }
    public void ChangeConsumable3(Consumable consumable)
    {

        stackAmount3 = consumable.stackAmount;
        consumable3 = consumable;

    }

}
