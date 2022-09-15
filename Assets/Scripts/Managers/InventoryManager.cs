using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    public List<Item> inventory;
    public float currentWeight;
    public float maxWeight;

    public Weapon currentWeapon;
    public Weapon secondaryWeapon;

    public Equipment currentHead;
    public Equipment currentChest;
    public Equipment currentPants;
    public Equipment currentShoes;

    public Equipment currentNecklace;
    public Equipment currentEarrings;
    public Equipment currentRing1;
    public Equipment currentRing2;


    // References to the UI Elements for inventory
    public GameObject uiObject;
    public Transform uiContent;
    public Transform uiItemInfo;
    public Transform weight;

    private int currentTab = 0;

    private float swapTime = 0.5f;
    private float swapClock = 0;
    private bool swap = false;


    public void AddItem(Item item)
    {
        inventory.Add(item);
        UpdateStats();
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
        UpdateStats();

    }



    private void Start()
    {
        GameEvents.current.OnChangeCurrentWeapon += ChangeCurrentWeapon;
        GameEvents.current.OnChangeSecondaryWeapon += ChangeSecondaryWeapon;
        GameEvents.current.OnAddItem += AddItem;
        GameEvents.current.OnRemoveItem += RemoveItem;
        GameEvents.current.OnRemoveCurrentSecondaryItem += RemoveCurrentSecondaryItem;
        GameEvents.current.OnChangeCurrentEquipment += ChangeCurrentEquipment;
        GameEvents.current.OnRemoveCurrentEquipment += RemoveCurrentEquipment;


        UpdateInventoryTab(currentTab);
    }

    // Debug purposes in Update
    void Update()
    {

        if (Input.GetButton("1") && !swap)
        {
            SwapWeapon();
            swap = true;
        }

        if(swap)
        {
            swapClock += Time.deltaTime;
            if(swapClock > swapTime)
            {
                swap = false;
                swapClock = 0f;
            }
        }

    }

    public void ChangeWeapon(Weapon weapon)
    {

        // Sends these events and updates the player with these
        GameEvents.current.ChangeWeapon(weapon);
        GameEvents.current.ChangeWeaponCollider(weapon.colliderPointX, weapon.colliderPointY, (int)weapon.weaponType);
        UpdateStats();

    }

    public void ChangeCurrentWeapon(Weapon weapon)
    {
        if (currentWeapon != null)
        {
            inventory.Add(currentWeapon);                 
        }
        currentWeapon = weapon;
        ChangeWeapon(currentWeapon);
        UpdateInventoryTab(currentTab);
    }

    public void ChangeSecondaryWeapon(Weapon weapon)
    {
        
        secondaryWeapon = weapon;
        UpdateInventoryTab(currentTab);

    }

    private void ChangeCurrentEquipment(Equipment equipment, int slotIndex)
    {
        switch (equipment.equipmentType)
        {
            case Equipment.EquipmentType.Head:
                #region Head
               
                currentHead = equipment;
                #endregion
                break;
            case Equipment.EquipmentType.Chest:
                #region Chest
                
                currentChest = equipment;
                #endregion
                break;
            case Equipment.EquipmentType.Pants:
                #region Pants

                currentPants = equipment;
                #endregion
                break;
            case Equipment.EquipmentType.Shoes:
                #region Shoes
                
                currentShoes = equipment;
                #endregion
                break;
            case Equipment.EquipmentType.Necklace:
                #region Necklace
               
                currentNecklace = equipment;
                #endregion
                break;
            case Equipment.EquipmentType.Earring:
                #region Earrings
                
                currentEarrings = equipment;
                #endregion
                break;
            case Equipment.EquipmentType.Ring:
                if (slotIndex == 6)
                {
                    #region Ring1
                    
                    currentRing1 = equipment;
                    #endregion
                }
                if (slotIndex == 7)
                {
                    #region Ring2
                    
                    currentRing2 = equipment;
                    #endregion
                }
                break;
            default:
                break;


        }
        UpdateInventoryTab(currentTab);

    }

    public void RemoveCurrentSecondaryItem(Weapon weapon)
    {
        if (weapon == secondaryWeapon)
        {
            secondaryWeapon = null;
        }
        
        UpdateStats();
    }

    public void RemoveCurrentEquipment(Equipment equipment, int slotIndex)
    {
        Debug.Log("Running");
        switch (equipment.equipmentType)
        {
            case Equipment.EquipmentType.Head:
                #region Head

                currentHead = null;

                #endregion
                break;
            case Equipment.EquipmentType.Chest:
                #region Chest

                currentChest = null;

                #endregion
                break;
            case Equipment.EquipmentType.Pants:
                #region Pants

                currentPants = null;

                #endregion
                break;
            case Equipment.EquipmentType.Shoes:
                #region Shoes

                currentShoes = null;

                #endregion
                break;
            case Equipment.EquipmentType.Necklace:
                #region Necklace

                currentNecklace = null;

                #endregion
                break;
            case Equipment.EquipmentType.Earring:
                #region Earrings

                currentEarrings = null;

                #endregion
                break;
            case Equipment.EquipmentType.Ring:
                if (slotIndex == 6)
                {
                    #region Ring1

                    currentRing1 = null;

                    #endregion
                }
                if (slotIndex == 7)
                {
                    #region Ring2

                    currentRing2 = null;

                    #endregion
                }
                break;
            default:
                break;


        }

        UpdateStats();
    }


    public void UpdateInventoryTab(int i)
    {
        UpdateStats();
        // When we open the inventory we reset the text here to be empty.
        // We update the info later when we click on an item

        uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";

      
        switch (i)
        {
            case 0:
                #region General Tab

                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                currentWeight = 0;

                foreach (Item item in inventory)
                {
                    currentWeight += item.itemWeight;
                    if (item.itemType != Item.ItemType.Weapon)
                    {
                        if (item.itemType != Item.ItemType.Equipment)
                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();


                            obj.GetComponent<InventoryItem>().item = item;
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
                }
               
                currentTab = 0;
                weight.GetComponent<TMPro.TextMeshProUGUI>().text = currentWeight.ToString();
                #endregion 
                break;
            case 1:
                #region WeaponsTab

                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                currentWeight = 0;

                foreach (Item item in inventory)
                {
                    
                    currentWeight += item.itemWeight;
                    if (item.itemType == Item.ItemType.Weapon)
                    {
                        GameObject obj = Instantiate(uiObject, uiContent);
                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();

                        
                        obj.GetComponent<InventoryItem>().item = item;
                        
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

                currentTab = 1;
                #endregion
                break;
            case 2:
                #region Consumables

                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                currentWeight = 0;

                foreach (Item item in inventory)
                {
                    currentWeight += item.itemWeight;
                    if (item.itemType == Item.ItemType.Consumable)
                    {
                        GameObject obj = Instantiate(uiObject, uiContent);
                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();
                        obj.GetComponent<InventoryItem>().item = item;
                        obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
                    }
                }
                currentTab = 2;
                foreach (Item item in inventory)
                {
                    
                    currentWeight += item.itemWeight;
                    if (item.itemType == Item.ItemType.Consumable)
                    {
                        GameObject obj = Instantiate(uiObject, uiContent);
                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();

                
                        obj.GetComponent<InventoryItem>().item = item;
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
                #endregion
                break;
            case 3:
                #region Materials


                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                currentWeight = 0;

                foreach (Item item in inventory)
                {
                    currentWeight += item.itemWeight;
                    if (item.itemType == Item.ItemType.Material)
                    {
                        GameObject obj = Instantiate(uiObject, uiContent);
                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();

                        
                        obj.GetComponent<InventoryItem>().item = item;
                        switch ((int)obj.GetComponent<InventoryItem>().item.itemRarity)
                        {
                            case 0:
                               
                                obj.GetComponent<Image>().color = new Color32(130, 130, 130,100);
                                break;
                            case 1:
                                
                                obj.GetComponent<Image>().color = new Color32(110, 190, 80,100);
                                break;
                            case 2:
                               
                                obj.GetComponent<Image>().color = new Color32(50, 140, 175,100);
                                break;
                            case 3:
                                
                                obj.GetComponent<Image>().color = new Color32(185, 80, 190,100);
                                break;
                            case 4:
                                
                                obj.GetComponent<Image>().color = new Color32(220, 150, 50,100);
                                break;
                            default:
                                break;
                        }
                        obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
                    }
                }
                currentTab = 3;
                #endregion
                break;
            case 4:
                #region Armor


                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                currentWeight = 0;

                foreach (Item item in inventory)
                {
                    currentWeight += item.itemWeight;
                    if (item.itemType == Item.ItemType.Equipment)
                    {
                        Equipment equipment = (Equipment)item;
                        if (equipment.equipmentType == Equipment.EquipmentType.Head ||
                            equipment.equipmentType == Equipment.EquipmentType.Chest ||
                            equipment.equipmentType == Equipment.EquipmentType.Pants ||
                            equipment.equipmentType == Equipment.EquipmentType.Shoes)

                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();


                            obj.GetComponent<InventoryItem>().item = item;
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
                    
                }
                currentTab = 4;
                #endregion
                break;
            case 5:
                #region Accessories


                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                currentWeight = 0;

                foreach (Item item in inventory)
                {
                    currentWeight += item.itemWeight;
                    if (item.itemType == Item.ItemType.Equipment)
                    {
                        Equipment equipment = (Equipment)item;
                        if (equipment.equipmentType == Equipment.EquipmentType.Necklace ||
                            equipment.equipmentType == Equipment.EquipmentType.Earring ||
                            equipment.equipmentType == Equipment.EquipmentType.Ring)

                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();


                            obj.GetComponent<InventoryItem>().item = item;
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
                }
                currentTab = 5;
                #endregion
                break;
            default:
                break;
        }


        GameEvents.current.InventoryRefresh(currentWeapon, secondaryWeapon);
    }


    private void SwapWeapon()
    {
        if (currentWeapon != null && secondaryWeapon != null)
        {
            Weapon weapon = currentWeapon;
            if (secondaryWeapon != null)
            {
                currentWeapon = secondaryWeapon;
            }
            else
            {
                currentWeapon = null;
            }
            secondaryWeapon = weapon;
            if (currentWeapon != null)
            {
                ChangeWeapon(currentWeapon);
            }
            
            UpdateInventoryTab(currentTab);

            GameEvents.current.SwapWeapon();
        }
        if (currentWeapon == null && secondaryWeapon != null)
        {

            currentWeapon = secondaryWeapon;                       
            secondaryWeapon = null;            
            ChangeWeapon(currentWeapon);
            

            UpdateInventoryTab(currentTab);

            GameEvents.current.SwapWeapon();
        }

       
    }

    public void SpawnCurrentWeapon()
    {     
        GameEvents.current.InventoryRefresh(currentWeapon, secondaryWeapon);
    }



    private void UpdateStats()
    {
        #region Damage
        float damage = 0f;
        if (currentWeapon != null)
        {
            damage += currentWeapon.damage;
        }


        #endregion

        #region Armor
        float armor = 0f;
        if (currentHead != null)
        {
            armor += currentHead.armor;
        }
        if(currentChest != null)
        {
            armor += currentChest.armor;
        }
        if (currentPants != null)
        {
            armor += currentPants.armor;
        }
        if(currentShoes != null)
        {
            armor += currentShoes.armor;
        }
        if(currentNecklace != null)
        {
            armor += currentNecklace.armor;
        }
        if(currentEarrings != null)
        {
            armor += currentEarrings.armor;
        }
        if(currentRing1 != null)
        {
            armor += currentRing1.armor;
        }
        if(currentRing2 != null)
        {
            armor += currentRing2.armor;
        }

        #endregion

        #region Crit
        float critRate = 0f;
        float critDamage = 1f;
        if (currentWeapon != null)
        {
            critRate += currentWeapon.critRate;
            critDamage *= currentWeapon.critDamage;
        }

        #endregion

        #region KnockBackForce
        float knockBackForce = 0f;
        if (currentWeapon != null)
        {
            knockBackForce += currentWeapon.knockBackForce;
        }

        #endregion

        #region SlowDebuff
        float slowDebuff = 0f;
        if (currentWeapon != null)
        {
            slowDebuff += currentWeapon.speedMultiplier;
        }

        #endregion

        #region SlowDebuffTime
        float slowDebuffTime = 0;
        if (currentWeapon != null)
        {
            slowDebuffTime += currentWeapon.slowDownLength;
        }
        #endregion

        #region Mana
        float manaCost = 0f;
        if (currentWeapon != null)
        {
            manaCost += currentWeapon.manaCost;
        }
        #endregion

        #region ShotForce
        float shotForce = 0f;
        if (currentWeapon != null)
        {
            shotForce = currentWeapon.force;
        }
        #endregion

        #region LocalPosition
        Vector2 localPos = new Vector2(0f, 0f);
        if (currentWeapon != null)
        {
            localPos = currentWeapon.localPosition;
        }
       
        #endregion


        GameEvents.current.UpdateArmorStat(armor);
        GameEvents.current.ChangeStats(damage, knockBackForce, slowDebuff,
            slowDebuffTime, manaCost, shotForce, critRate, critDamage, localPos);
    }


}
