using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConsumableManager : SingletonMonoBehaviour<ConsumableManager>, IDataPersistence
{
    // 3 current abilites
    public Potion potion1;
    public Consumable consumable1;
    public int stackAmount1;
    public Potion potion2;
    public Consumable consumable2;
    public int stackAmount2;
    public Potion potion3;
    public Consumable consumable3;
    public int stackAmount3;


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

    GameObject itemUi;

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

    private Potion basicPotion;
    private Transform uiItemInfo;
    private Transform consumableSlot1;
    private Transform consumableSlot2;
    private Transform consumableSlot3;



    public void Start()
    {
        playerObject = PlayerSingleton.instance.gameObject;
        cooldownIcon1 = HUDSingleton.instance.transform.Find("Potion1").transform.Find("PotionSlot1").GetComponent<Slider>();
        cooldownIcon2 = HUDSingleton.instance.transform.Find("Potion2").transform.Find("PotionSlot2").GetComponent<Slider>();
        cooldownIcon3 = HUDSingleton.instance.transform.Find("Potion3").transform.Find("PotionSlot3").GetComponent<Slider>();
        basicPotion = transform.GetComponent<Potion>();
        uiItemInfo = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemInfo").transform;
        consumableSlot1 = HUDSingleton.instance.transform.Find("Potion1").transform.Find("PotionSlot1").transform;
        consumableSlot2 = HUDSingleton.instance.transform.Find("Potion2").transform.Find("PotionSlot2").transform;
        consumableSlot3 = HUDSingleton.instance.transform.Find("Potion3").transform.Find("PotionSlot3").transform;

        StartCoroutine(GameLoaded()); 
    }


    public void SaveData(GameData data)
    {
        data.consumable1 = consumable1;
        data.consumableStackAmount1 = stackAmount1;

        data.consumable2 = consumable2;
        data.consumableStackAmount2 = stackAmount2;

        data.consumable3 = consumable3;
        data.consumableStackAmount3 = stackAmount3;
    }

    public void LoadData(GameData data)
    {
        if (data.consumable1 != null)
        {
            if (data.consumable1.isActive)
            {
                consumable1 = data.consumable1;
                Sprite[] sprite = Resources.LoadAll<Sprite>(consumable1.spriteAtlasPath);
                consumable1.itemSprite = sprite[consumable1.spriteIndex];
                stackAmount1 = data.consumableStackAmount1;
            }
        }

        if (data.consumable2 != null)
        {
            if (data.consumable2.isActive)
            {
                consumable2 = data.consumable2;
                Sprite[] sprite2 = Resources.LoadAll<Sprite>(consumable2.spriteAtlasPath);
                consumable2.itemSprite = sprite2[consumable2.spriteIndex];
                stackAmount2 = data.consumableStackAmount2;
            }
            
        }
        if (data.consumable3 != null)
        {
            if (data.consumable3.isActive)
            {
                consumable3 = data.consumable3;
                Sprite[] sprite3 = Resources.LoadAll<Sprite>(consumable3.spriteAtlasPath);
                consumable3.itemSprite = sprite3[consumable3.spriteIndex];
                stackAmount3 = data.consumableStackAmount3;
            }
            

        }
        
    }






    // Update is called once per frame
    void Update()
    {


        #region Consumable1
        if (potion1 != null)
        {
            switch (state1)
            {
                case ConsumableState.ready:
                    if (Input.GetKeyDown(key1) && !(cooldownTime1 > 0) && stackAmount1 > 0 && consumable1.isActive)
                    {
                        Debug.Log("Consumable 1 Activated");
                        stackAmount1 -= 1;
                        HUDSingleton.instance.transform.Find("Potion1").Find("PotionSlot1").Find("ItemUI(Clone)").transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount1.ToString();
                        HUDSingleton.instance.transform.Find("Potion1").Find("PotionSlot1").Find("ItemUI(Clone)").transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (consumable1.itemWeight * stackAmount1).ToString();
                        potion1.Activate(consumable1.hpHealing, consumable1.manaHealing);
                        cooldownIcon1.value = 1f;
                        state1 = ConsumableState.active;
                        activeTime1 = consumable1.activeTime;
                        cooldownTime1 = consumable1.cooldownTime;


                        if (potion1.hasBuff)
                        {
                            buffIcon2 = Instantiate(uiObject, buffParent);
                            buffIcon2.GetComponent<Image>().sprite = potion1.buffIcon;
                        }
                    }
                    if (cooldownTime1 >= 0f)
                    {
                        cooldownTime1 -= Time.deltaTime;
                    }


                    break;
                case ConsumableState.active:
                    if (activeTime1 > 0)
                    {
                        activeTime1 -= Time.deltaTime;
                    }
                    else
                    {
                        potion1.Trigger(playerObject);
                        potion1.DeActivate(playerObject);
                        Debug.Log("Consumable 1 Deactivated");
                        if (potion1.hasBuff)
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
            if (potion1 != null)
            cooldownIcon1.value = cooldownTime1 / consumable1.cooldownTime;
        }


        #endregion

        #region Consumable2

        if (potion2 != null)
        {
            switch (state2)
            {
                case ConsumableState.ready:
                    if (Input.GetKeyDown(key2) && !(cooldownTime2 > 0) && stackAmount2 > 0 && consumable2.isActive)
                    {
                        Debug.Log("Consumable 2 Activated");
                        stackAmount2 -= 1;
                        HUDSingleton.instance.transform.Find("Potion2").Find("PotionSlot2").Find("ItemUI(Clone)").transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount2.ToString();
                        HUDSingleton.instance.transform.Find("Potion1").Find("PotionSlot1").Find("ItemUI(Clone)").transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (consumable2.itemWeight * stackAmount2).ToString();
                        potion2.Activate(consumable2.hpHealing, consumable2.manaHealing);
                        cooldownIcon2.value = 1f;
                        state2 = ConsumableState.active;
                        activeTime2 = consumable2.activeTime;
                        cooldownTime2 = consumable2.cooldownTime;


                        if (potion1.hasBuff)
                        {
                            buffIcon2 = Instantiate(uiObject, buffParent);
                            buffIcon2.GetComponent<Image>().sprite = potion2.buffIcon;
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
                        potion2.Trigger(playerObject);
                        potion2.DeActivate(playerObject);
                        Debug.Log("Consumable 2 Deactivated");
                        if (potion2.hasBuff)
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
        if (potion3 != null)
        {
            switch (state3)
            {
                case ConsumableState.ready:
                    if (Input.GetKeyDown(key3) && !(cooldownTime3 > 0) && stackAmount3 > 0 && consumable3.isActive)
                    {
                        Debug.Log("Consumable 3 Activated");
                        stackAmount3 -= 1;
                        HUDSingleton.instance.transform.Find("Potion3").Find("PotionSlot3").Find("ItemUI(Clone)").transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount3.ToString();
                        HUDSingleton.instance.transform.Find("Potion1").Find("PotionSlot1").Find("ItemUI(Clone)").transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (consumable3.itemWeight * stackAmount3).ToString();
                        potion3.Activate(consumable3.hpHealing, consumable3.manaHealing);
                        cooldownIcon3.value = 1f;
                        state3 = ConsumableState.active;
                        activeTime3 = consumable3.activeTime;
                        cooldownTime3 = consumable3.cooldownTime;


                        if (potion3.hasBuff)
                        {
                            buffIcon3 = Instantiate(uiObject, buffParent);
                            buffIcon3.GetComponent<Image>().sprite = potion3.buffIcon;
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
                        potion3.Trigger(playerObject);
                        potion3.DeActivate(playerObject);
                        Debug.Log("Consumable 3 Deactivated");
                        if (potion3.hasBuff)
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

    IEnumerator GameLoaded()
    {
        yield return new WaitForSeconds(0.1f);

        
        
        if (consumable1.isActive)
        {
            ChangeConsumable1(consumable1, stackAmount1);
            GameObject obj = Instantiate(uiObject, consumableSlot1);
            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = consumable1.itemName;
            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = consumable1.itemSprite;
            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (consumable1.itemWeight * stackAmount1).ToString();
            obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount1.ToString();
            obj.GetComponent<InventoryItem>().amount = stackAmount1;

            obj.GetComponent<InventoryItem>().item = consumable1;
            obj.GetComponent<Image>().color = new Color(130, 130, 130);
            switch ((int)obj.GetComponent<InventoryItem>().item.itemRarity)
            {
                case 0:

                    obj.GetComponent<Image>().color = new Color32(130, 130, 130, 100);
                    break;
                case 1:

                    obj.GetComponent<Image>().color = new Color32(110, 190, 80, 100);
                    break;
                case 2:

                    obj.GetComponent<Image>().color = new Color32(50, 140, 175, 100);
                    break;
                case 3:

                    obj.GetComponent<Image>().color = new Color32(185, 80, 190, 100);
                    break;
                case 4:

                    obj.GetComponent<Image>().color = new Color32(220, 150, 50, 100);
                    break;
                default:
                    break;
            }
            obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
        }
        if (consumable2.isActive)
        {
            ChangeConsumable2(consumable2, stackAmount2);
            GameObject obj = Instantiate(uiObject, consumableSlot2);
            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = consumable2.itemName;
            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = consumable2.itemSprite;
            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (consumable2.itemWeight * stackAmount2).ToString();
            obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount2.ToString();
            obj.GetComponent<InventoryItem>().amount = stackAmount2;

            obj.GetComponent<InventoryItem>().item = consumable2;
            obj.GetComponent<Image>().color = new Color(130, 130, 130);
            switch ((int)obj.GetComponent<InventoryItem>().item.itemRarity)
            {
                case 0:

                    obj.GetComponent<Image>().color = new Color32(130, 130, 130, 100);
                    break;
                case 1:

                    obj.GetComponent<Image>().color = new Color32(110, 190, 80, 100);
                    break;
                case 2:

                    obj.GetComponent<Image>().color = new Color32(50, 140, 175, 100);
                    break;
                case 3:

                    obj.GetComponent<Image>().color = new Color32(185, 80, 190, 100);
                    break;
                case 4:

                    obj.GetComponent<Image>().color = new Color32(220, 150, 50, 100);
                    break;
                default:
                    break;
            }
            obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
        }
        if (consumable3.isActive)
        {
            ChangeConsumable3(consumable3, stackAmount3);
            GameObject obj = Instantiate(uiObject, consumableSlot3);
            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = consumable3.itemName;
            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = consumable3.itemSprite;
            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (consumable3.itemWeight * stackAmount3).ToString();
            obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = stackAmount3.ToString();
            obj.GetComponent<InventoryItem>().amount = stackAmount3;

            obj.GetComponent<InventoryItem>().item = consumable3;
            obj.GetComponent<Image>().color = new Color(130, 130, 130);
            switch ((int)obj.GetComponent<InventoryItem>().item.itemRarity)
            {
                case 0:

                    obj.GetComponent<Image>().color = new Color32(130, 130, 130, 100);
                    break;
                case 1:

                    obj.GetComponent<Image>().color = new Color32(110, 190, 80, 100);
                    break;
                case 2:

                    obj.GetComponent<Image>().color = new Color32(50, 140, 175, 100);
                    break;
                case 3:

                    obj.GetComponent<Image>().color = new Color32(185, 80, 190, 100);
                    break;
                case 4:

                    obj.GetComponent<Image>().color = new Color32(220, 150, 50, 100);
                    break;
                default:
                    break;
            }
            obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
        }
    }



    public void ChangeConsumable1(Consumable consumable, int amount)
    {
        if (consumable1.isActive && consumable1.itemName != consumable.itemName)
        {
            InventoryManager.Instance.AddItemToStack(consumable1, stackAmount1);
            InventoryManager.Instance.UpdateInventoryTab(InventoryManager.Instance.currentTab);
        }

        consumable1 = consumable;
        stackAmount1 = amount;
        switch(consumable.consumableType)
        {
            case Consumable.ConsumableType.Potion:
                potion1 = basicPotion;
                break;
            default:
                break;
        }


    }
    public void ChangeConsumable2(Consumable consumable, int amount)
    {
        if (consumable2.isActive && consumable2.itemName != consumable.itemName)
        {
            InventoryManager.Instance.AddItemToStack(consumable2, stackAmount2);
            InventoryManager.Instance.UpdateInventoryTab(InventoryManager.Instance.currentTab);
        }
        consumable2 =  consumable;
        stackAmount2 = amount;
        switch (consumable.consumableType)
        {
            case Consumable.ConsumableType.Potion:
                potion2 = basicPotion;
                break;
            default:
                break;
        }

    }
    public void ChangeConsumable3(Consumable consumable, int amount)
    {
        if (consumable3.isActive && consumable3.itemName != consumable.itemName)
        {
            InventoryManager.Instance.AddItemToStack(consumable2, stackAmount2);
            InventoryManager.Instance.UpdateInventoryTab(InventoryManager.Instance.currentTab);
        }
        consumable3 = consumable;
        stackAmount3 = amount;
        switch (consumable.consumableType)
        {
            case Consumable.ConsumableType.Potion:
                potion3 = basicPotion;
                break;
            default:
                break;
        }

    }



}
