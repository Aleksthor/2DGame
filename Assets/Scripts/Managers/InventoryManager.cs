using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;



[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int stackAmount;

    public InventorySlot(Item Item, int Amount)
    {
        item = Item;


        stackAmount = Amount;
        
    }

    public void AddAmount(int value)
    {
        stackAmount += value;
    }
}

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>, IDataPersistence
{
    public List<InventorySlot> inventory = new List<InventorySlot>();
    public float currentWeight;
    public float maxWeight;

    public Weapon currentWeapon;
    public Weapon secondaryWeapon;

    public Shield currentShield;

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
    private Transform uiContent;
    private Transform uiItemInfo;
    private Transform weight;
    private Transform PickupItems;
    public GameObject pickupItem;

    private int currentTab = 0;

    private float swapTime = 0.5f;
    private float swapClock = 0;
    private bool swap = false;

    public Weapon starterWeapon;



    public void AddItem(Item item)
    { 
        //spawn a icon on the hud 
        GameObject hudNotice = Instantiate(pickupItem, PickupItems);
        hudNotice.GetComponent<UIItemPickup>().PickUp(item, 1);

        bool hasItem = false;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == item)
            {
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            inventory.Add(new InventorySlot(item, 1));
        }


        // Update all stats
        UpdateStats();
    }

    public void AddItemToStack(Item item, int amount)
    {
        //spawn a icon on the hud 
        GameObject hudNotice = Instantiate(pickupItem, PickupItems);
        hudNotice.GetComponent<UIItemPickup>().PickUp(item, amount);

        bool hasItem = false;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == item)
            {
                inventory[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            inventory.Add(new InventorySlot(item, amount));
        }

        // Update all stats
        UpdateStats();
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == item)
            {
                inventory.RemoveAt(i);
            }
        }
        UpdateStats();

    }
    public void RemoveItemStack(Item item, int amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == item)
            {
                inventory[i].stackAmount -= amount;
                if (inventory[i].stackAmount < 0)
                {
                    inventory.RemoveAt(i);
                }
               
            }
        }
        UpdateStats();

    }




    public void LoadData(GameData data)
    {

        inventory = data.inventory;

        #region Equipped Items
        if (data.currentWeapon != null)
        {
            List<Item> currentweapon = new List<Item>();

            currentweapon = data.currentWeapon;
            if(currentweapon.Count > 0)
                currentWeapon = (Weapon)currentweapon[0];
            

        }
        if (data.secondaryWeapon != null)
        {
            List<Item> secondaryweapon = new List<Item>();

            secondaryweapon = data.secondaryWeapon;
            if (secondaryweapon.Count > 0)
                secondaryWeapon = (Weapon)secondaryweapon[0];
        }
        if (data.currentShield != null)
        {
            List<Item> currentshield = new List<Item>();

            currentshield = data.currentShield;
            if (currentshield.Count > 0)
                currentShield = (Shield)currentshield[0];
        }

        if (data.currentHead != null)
        {
            List<Item> currenthead = new List<Item>();

            currenthead = data.currentHead;
            if (currenthead.Count > 0)
                currentHead = (Equipment)currenthead[0];
        }
        if (data.currentChest != null)
        {
            List<Item> currentchest = new List<Item>();

            currentchest = data.currentChest;
            if (currentchest.Count > 0)
                currentChest = (Equipment)currentchest[0];
        }
        if (data.currentPants != null)
        {
            List<Item> currentpants = new List<Item>();

            currentpants = data.currentPants;
            if (currentpants.Count > 0)
                currentPants = (Equipment)currentpants[0];
        }
        if (data.currentShoes != null)
        {
            List<Item> currentshoes = new List<Item>();

            currentshoes = data.currentShoes;
            if (currentshoes.Count > 0)
                currentShoes = (Equipment)currentshoes[0];
        }

        if (data.currentNecklace != null)
        {
            List<Item> currentnecklace = new List<Item>();

            currentnecklace = data.currentNecklace;
            if (currentnecklace.Count > 0)
                currentNecklace = (Equipment)currentnecklace[0];
        }
        if (data.currentEarrings != null)
        {
            List<Item> currentearrings = new List<Item>();

            currentearrings = data.currentEarrings;
            if (currentearrings.Count > 0)
                currentEarrings = (Equipment)currentearrings[0];
        }
        if (data.currentRing1 != null)
        {
            List<Item> currentring1 = new List<Item>();

            currentring1 = data.currentRing1;
            if (currentring1.Count > 0)
                currentRing1 = (Equipment)currentring1[0];
        }
        if (data.currentRing2 != null)
        {
            List<Item> currentring2 = new List<Item>();

            currentring2 = data.currentRing2;
            if (currentring2.Count > 0)
                currentRing2 = (Equipment)currentring2[0];
        }

        #endregion


    }

    public void SaveData(ref GameData data)
    {
        data.inventory = inventory;

        #region Current Equipment
        if (currentWeapon != null)
        {
            List<Item> currentweapon = new List<Item>();
            currentweapon.Add(currentWeapon);
            data.currentWeapon = currentweapon;
        }

        if (secondaryWeapon != null)
        {
            List<Item> secondaryweapon = new List<Item>();
            secondaryweapon.Add(secondaryWeapon);
            data.secondaryWeapon = secondaryweapon;
        }

        if (currentShield != null)
        {
            List<Item> currentshield = new List<Item>();
            currentshield.Add(currentShield);
            data.currentShield = currentshield;
        }


        if (currentHead != null)
        {
            List<Item> currenthead = new List<Item>();
            currenthead.Add(currentHead);
            data.currentHead = currenthead;
        }
        if (currentChest != null)
        {
            List<Item> currentchest = new List<Item>();
            currentchest.Add(currentChest);
            data.currentChest = currentchest;
        }
        if (currentPants != null)
        {
            List<Item> currentpants = new List<Item>();
            currentpants.Add(currentPants);
            data.currentPants = currentpants;
        }
        if (currentShoes != null)
        {
            List<Item> currentshoes = new List<Item>();
            currentshoes.Add(currentShoes);
            data.currentShoes = currentshoes;
        }

        if (currentNecklace != null)
        {
            List<Item> currentnecklace = new List<Item>();
            currentnecklace.Add(currentNecklace);
            data.currentNecklace = currentnecklace;
        }
        if (currentEarrings != null)
        {
            List<Item> currentearrings = new List<Item>();
            currentearrings.Add(currentEarrings);
            data.currentEarrings = currentearrings;
        }
        if (currentRing1 != null)
        {
            List<Item> currentring1 = new List<Item>();
            currentring1.Add(currentRing1);
            data.currentRing1 = currentring1;
        }
        if (currentRing2 != null)
        {
            List<Item> currentring2 = new List<Item>();
            currentring2.Add(currentRing2);
            data.currentRing2 = currentring2;
        }
        #endregion
    }


    private void Start()
    {
        if (currentWeapon == null)
        {
            currentWeapon = starterWeapon;
        }
        else
        {
            ChangeWeapon(currentWeapon);
        }
        if (inventory == null)
        {
            inventory = new List<InventorySlot>();
        }
        

        GameEvents.current.OnAddItem += AddItem;
        GameEvents.current.OnRemoveItem += RemoveItem;

        GameEvents.current.OnChangeCurrentWeapon += ChangeCurrentWeapon;

        GameEvents.current.OnChangeSecondaryWeapon += ChangeSecondaryWeapon;
        GameEvents.current.OnRemoveCurrentSecondaryItem += RemoveCurrentSecondaryItem;

        GameEvents.current.OnChangeCurrentShield += ChangeCurrentShield;
        GameEvents.current.OnRemoveCurrentShield += RemoveCurrentShield;

        GameEvents.current.OnChangeCurrentEquipment += ChangeCurrentEquipment;
        GameEvents.current.OnRemoveCurrentEquipment += RemoveCurrentEquipment;

        if (currentWeapon != null)
        {
            GameEvents.current.ChangeWeaponAbility(currentWeapon.ability1, currentWeapon.ability2, currentWeapon.ability3, currentWeapon.ability1Icon, currentWeapon.ability2Icon, currentWeapon.ability3Icon);
        }

        uiContent = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemList").transform.Find("Viewport").transform.Find("Content").transform;
        uiItemInfo = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemInfo").transform;
        weight = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemList").transform.Find("CurrentWeight").transform;
        PickupItems = HUDSingleton.instance.transform.Find("PickupItems").transform.Find("Viewport").transform.Find("Content").transform;

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
        Debug.Log("Weapon Was Changed");

        GameEvents.current.ChangeWeapon(weapon);
        GameEvents.current.ChangeWeaponCollider(weapon.colliderPointX, weapon.colliderPointY, (int)weapon.weaponType);
        GameEvents.current.ChangeWeaponAbility(weapon.ability1, weapon.ability2, weapon.ability3, weapon.ability1Icon, weapon.ability2Icon, weapon.ability3Icon);
       
        PlayerSingleton.instance.gameObject.GetComponent<Animator>().SetInteger("StaffAttackType", (int)weapon.staffBA);
        PlayerSingleton.instance.gameObject.GetComponent<Animator>().SetInteger("WandAttackType", (int)weapon.wandBA);


        if (weapon.isTwoHanded)
        {
            Debug.Log("Two Handed Weapon");
            SpriteManager.Instance.isTwoHanded = true;
            SpriteManager.Instance.canDualWield = false;
            GameEvents.current.HideShield();
        }
        else if (weapon.canDualWield)
        {
            Debug.Log("Dual Wield Weapon");
            SpriteManager.Instance.isTwoHanded = false;
            SpriteManager.Instance.canDualWield = true;
            GameEvents.current.HideShield();
        }
        else
        {
            Debug.Log("One Handed Weapon");
            SpriteManager.Instance.isTwoHanded = false;
            SpriteManager.Instance.canDualWield = false;
            GameEvents.current.ShowShield();
        }
        PlayerManager.Instance.staminaPerHit = weapon.staminaUse;
        if (secondaryWeapon != null)
        {
            if (currentWeapon.canDualWield && secondaryWeapon.canDualWield)
            {
                PlayerManager.Instance.staminaPerHit = weapon.staminaUse + secondaryWeapon.staminaUse;
            }
        }

        UpdateStats();

    }

    public void ChangeCurrentWeapon(Weapon weapon)
    {
        if (currentWeapon != null)
        {
            inventory.Add(new InventorySlot(currentWeapon, 1));                 
        }
        currentWeapon = weapon;
        AbilityManager.Instance.ResetCooldowns();
        ChangeWeapon(currentWeapon);
        UpdateInventoryTab(currentTab);
    }

    public void ChangeSecondaryWeapon(Weapon weapon)
    {
        if (currentWeapon != null)
        {
            if (currentWeapon.canDualWield && weapon.canDualWield)
            {
                GameEvents.current.UpdateSecondaryWeapon(weapon);
                GameEvents.current.ShowSecondary(weapon);
                PlayerManager.Instance.staminaPerHit = weapon.staminaUse + currentWeapon.staminaUse;
            }
            else
            {
                GameEvents.current.RemoveSecondary();
                PlayerManager.Instance.staminaPerHit = currentWeapon.staminaUse;
            }
        }
        secondaryWeapon = weapon;
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

    public void ChangeShield(Shield shield)
    {
        Debug.Log("Shield Was Changed");
        GameEvents.current.ChangeShield(shield);
    }

    public void ChangeCurrentShield(Shield shield)
    {

        currentShield = shield;
        ChangeShield(shield);
        UpdateInventoryTab(currentTab);

    }
    public void RemoveCurrentShield(Shield shield)
    {
        if (shield == currentShield)
        {
            currentShield = null;
        }

        UpdateStats();
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
    public void RemoveCurrentEquipment(Equipment equipment, int slotIndex)
    {
       
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
        uiItemInfo.Find("Background").transform.Find("text-3").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        uiItemInfo.Find("Background").transform.Find("Info3").GetComponent<TMPro.TextMeshProUGUI>().text = "";
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
                if (inventory.Count > 0)
                {

                    foreach (InventorySlot inventorySlot in inventory)
                    {
                        Item item = inventorySlot.item;
                        currentWeight += (item.itemWeight * inventorySlot.stackAmount);
                        if (item.itemType != Item.ItemType.Weapon)
                        {
                            if (item.itemType != Item.ItemType.Equipment)
                            {
                                if(item.itemType != Item.ItemType.Shield)
                                {
                                    GameObject obj = Instantiate(uiObject, uiContent);
                                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();
                                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = inventorySlot.stackAmount.ToString() ;


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
                if (inventory.Count > 0)
                {
                    
                    foreach (InventorySlot inventorySlot in inventory)
                    {
                        Item item = inventorySlot.item;
                        currentWeight += (item.itemWeight * inventorySlot.stackAmount);
                        if (item.itemType == Item.ItemType.Weapon)
                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();
                            obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";


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
                weight.GetComponent<TMPro.TextMeshProUGUI>().text = currentWeight.ToString();
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
                if (inventory.Count > 0)
                {


                    currentTab = 2;
                    foreach (InventorySlot inventorySlot in inventory)
                    {
                        Item item = inventorySlot.item;
                        currentWeight += item.itemWeight;
                        if (item.itemType == Item.ItemType.Consumable)
                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();

                
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
                weight.GetComponent<TMPro.TextMeshProUGUI>().text = currentWeight.ToString();
                #endregion
                break;
            case 3:
                #region Materials


                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                currentWeight = 0;
                if (inventory.Count > 0)
                {

                    foreach (InventorySlot inventorySlot in inventory)
                    {
                        Item item = inventorySlot.item;
                        currentWeight += (item.itemWeight * inventorySlot.stackAmount);
                        if (item.itemType == Item.ItemType.Material)
                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();
                            obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = inventorySlot.stackAmount.ToString();


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
                }
                weight.GetComponent<TMPro.TextMeshProUGUI>().text = currentWeight.ToString();
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
                if (inventory.Count > 0)
                {

                    foreach (InventorySlot inventorySlot in inventory)
                    {
                        Item item = inventorySlot.item;
                        currentWeight += (item.itemWeight * inventorySlot.stackAmount);
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
                                obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();
                                obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";


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
                }
                weight.GetComponent<TMPro.TextMeshProUGUI>().text = currentWeight.ToString();
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
                if (inventory.Count > 0)
                {

                    foreach (InventorySlot inventorySlot in inventory)
                    {
                        Item item = inventorySlot.item;
                        currentWeight += (item.itemWeight * inventorySlot.stackAmount);
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
                                obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();
                                obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";

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
                }
                weight.GetComponent<TMPro.TextMeshProUGUI>().text = currentWeight.ToString();
                currentTab = 5;
                #endregion
                break;
            case 6:
                #region Shields


                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                currentWeight = 0;
                if (inventory.Count > 0)
                {

                    foreach (InventorySlot inventorySlot in inventory)
                    {
                        Item item = inventorySlot.item;
                        currentWeight += (item.itemWeight * inventorySlot.stackAmount);
                        if (item.itemType == Item.ItemType.Shield)
                        {


                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();
                            obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";

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
                weight.GetComponent<TMPro.TextMeshProUGUI>().text = currentWeight.ToString();
                currentTab = 6;
                #endregion
                break;
            default:
                break;
        }


        GameEvents.current.InventoryRefresh(currentWeapon, secondaryWeapon);
        Debug.Log("Inventory Refreshed");
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
        float equipmentDamageBoost = 0f;
        if (currentWeapon != null)
        {
            damage += currentWeapon.damage;
        }


        #endregion

        #region MagicDamage
        float magicDamage = 0f;
        if (currentWeapon != null)
        {
            magicDamage += currentWeapon.magicDamage;
        }

        #endregion

        #region Armor
        float armor = 0f;
        if (currentHead != null)
        {
            equipmentDamageBoost += currentHead.bonusDamage;
            armor += currentHead.armor;
        }
        if(currentChest != null)
        {
            equipmentDamageBoost += currentChest.bonusDamage;
            armor += currentChest.armor;
        }
        if (currentPants != null)
        {
            equipmentDamageBoost += currentPants.bonusDamage;
            armor += currentPants.armor;
        }
        if(currentShoes != null)
        {
            equipmentDamageBoost += currentShoes.bonusDamage;
            armor += currentShoes.armor;
        }
        if(currentNecklace != null)
        {
            equipmentDamageBoost += currentNecklace.bonusDamage;
            armor += currentNecklace.armor;
        }
        if(currentEarrings != null)
        {
            equipmentDamageBoost += currentEarrings.bonusDamage;
            armor += currentEarrings.armor;
        }
        if(currentRing1 != null)
        {
            equipmentDamageBoost += currentRing1.bonusDamage;
            armor += currentRing1.armor;
        }
        if(currentRing2 != null)
        {
            equipmentDamageBoost += currentRing2.bonusDamage;
            armor += currentRing2.armor;
        }
        damage += equipmentDamageBoost;
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
        GameEvents.current.UpdateInventoryStats(damage, magicDamage, knockBackForce, slowDebuff,
slowDebuffTime, manaCost, shotForce, critRate, critDamage, localPos);
    }


}
