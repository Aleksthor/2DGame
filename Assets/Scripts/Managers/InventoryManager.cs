using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;


[System.Serializable]
public class InventorySlot
{
    [SerializeField] public Item item;
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

[System.Serializable]
public class ConsumableInventorySlot
{
    [SerializeField] public Consumable consumable;
    public int stackAmount;
    public ConsumableInventorySlot(Consumable Consumable, int Amount)
    {
        consumable = Consumable;
        stackAmount = Amount;

    }

    public void AddAmount(int value)
    {
        stackAmount += value;
    }
}

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>, IDataPersistence
{
    [SerializeField] public List<InventorySlot> itemInventory = new List<InventorySlot>();
    [SerializeField] public List<Weapon> weaponInventory = new List<Weapon>();
    [SerializeField] public List<Equipment> equipmentInventory = new List<Equipment>();
    [SerializeField] public List<Shield> shieldInventory = new List<Shield>();
    [SerializeField] public List<ConsumableInventorySlot> consumableInventory = new List<ConsumableInventorySlot>();


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

    public int currentTab = 0;

    private float swapTime = 0.5f;
    private float swapClock = 0;
    private bool swap = false;

    public Weapon starterWeapon;

    private void Start()
    {
       

        if (itemInventory == null)
        {
            itemInventory = new List<InventorySlot>();
        }
        if (weaponInventory == null)
        {
            weaponInventory = new List<Weapon>();
        }
        if (equipmentInventory == null)
        {
            equipmentInventory = new List<Equipment>();
        }
        if (shieldInventory == null)
        {
            shieldInventory = new List<Shield>();
        }
        if (consumableInventory == null)
        {
            consumableInventory = new List<ConsumableInventorySlot>();
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

        if (currentWeapon.isActive)
        {
            GameEvents.current.ChangeWeaponAbility(currentWeapon.ability1, currentWeapon.ability2, currentWeapon.ability3);
        }

        uiContent = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemList").transform.Find("Viewport").transform.Find("Content").transform;
        uiItemInfo = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemInfo").transform;
        weight = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemList").transform.Find("CurrentWeight").transform;
        PickupItems = HUDSingleton.instance.transform.Find("PickupItems").transform.Find("Viewport").transform.Find("Content").transform;


        StartCoroutine(SpawnCurrentWeapon());


    }

    public void AddItem(Item item)
    { 
        //spawn a icon on the hud 
        GameObject hudNotice = Instantiate(pickupItem, PickupItems);
        hudNotice.GetComponent<UIItemPickup>().PickUp(item, 1);

        bool hasItem = false;

        switch(item.itemType)
        {
            case Item.ItemType.Weapon:

                #region Weapon
                for (int i = 0; i < weaponInventory.Count; i++)
                {
                    if (weaponInventory[i].itemName == item.itemName)
                    {
                        hasItem = true;
                        break;
                    }
                }
                if (!hasItem)
                {
                    weaponInventory.Add((Weapon)item);
                }
                #endregion

                break;
            case Item.ItemType.Equipment:

                #region Equipment
                for (int i = 0; i < equipmentInventory.Count; i++)
                {
                    if (equipmentInventory[i].itemName == item.itemName)
                    {
                        hasItem = true;
                        break;
                    }
                }
                if (!hasItem)
                {
                    equipmentInventory.Add((Equipment)item);
                }
                #endregion

                break;
            case Item.ItemType.Consumable:

                #region Consumable
                for (int i = 0; i < consumableInventory.Count; i++)
                {
                    if (consumableInventory[i].consumable.itemName == item.itemName)
                    {
                        hasItem = true;
                        break;
                    }
                }
                if (!hasItem)
                {
                    consumableInventory.Add(new ConsumableInventorySlot((Consumable)item, 1));
                }
                #endregion

                break;
            case Item.ItemType.Shield:

                #region Shield
                for (int i = 0; i < shieldInventory.Count; i++)
                {
                    if (shieldInventory[i].itemName == item.itemName)
                    {
                        hasItem = true;
                        break;
                    }
                }
                if (!hasItem)
                {
                    shieldInventory.Add((Shield)item);
                }
                #endregion
                
                break;
            default:

                #region Default
                for (int i = 0; i < itemInventory.Count; i++)
                {
                    if (itemInventory[i].item.itemName == item.itemName)
                    {
                        hasItem = true;
                        break;
                    }
                }
                if (!hasItem)
                {
                    itemInventory.Add(new InventorySlot(item, 1));
                }
                #endregion

                break;

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

        switch (item.itemType)
        {
            case Item.ItemType.Weapon:
                break;
            case Item.ItemType.Equipment:
                break;
            case Item.ItemType.Consumable:

                #region Consumable
                for (int i = 0; i < consumableInventory.Count; i++)
                {
                    if (consumableInventory[i].consumable.itemName == item.itemName)
                    {
                        consumableInventory[i].AddAmount(amount);
                        hasItem = true;
                        break;
                    }
                }
                if (!hasItem)
                {
                    consumableInventory.Add(new ConsumableInventorySlot((Consumable)item, amount));
                }
                #endregion

                break;
            case Item.ItemType.Shield:
                break;
            default:

                #region Default
                for (int i = 0; i < itemInventory.Count; i++)
                {
                    if (itemInventory[i].item.itemName == item.itemName)
                    {
                        itemInventory[i].AddAmount(amount);
                        hasItem = true;
                        break;
                    }
                }
                if (!hasItem)
                {
                    itemInventory.Add(new InventorySlot(item, amount));
                }
                #endregion

                break;

        }
        

        // Update all stats
        UpdateStats();
    }

    public void RemoveItem(Item item)
    {

        switch (item.itemType)
        {
            case Item.ItemType.Weapon:
                for (int i = 0; i < weaponInventory.Count; i++)
                {
                    if (weaponInventory[i].itemName == item.itemName)
                    {
                        weaponInventory.RemoveAt(i);
                    }
                }
                break;
            case Item.ItemType.Equipment:
                for (int i = 0; i < equipmentInventory.Count; i++)
                {
                    if (equipmentInventory[i].itemName == item.itemName)
                    {
                        equipmentInventory.RemoveAt(i);
                    }
                }
                break;
            case Item.ItemType.Consumable:
                for (int i = 0; i < consumableInventory.Count; i++)
                {
                    if (consumableInventory[i].consumable.itemName == item.itemName)
                    {
                        consumableInventory.RemoveAt(i);
                    }
                }
                break;
            case Item.ItemType.Shield:
                for (int i = 0; i < shieldInventory.Count; i++)
                {
                    if (shieldInventory[i].itemName == item.itemName)
                    {
                        shieldInventory.RemoveAt(i);
                    }
                }
                break;
            default:
                for (int i = 0; i < itemInventory.Count; i++)
                {
                    if (itemInventory[i].item.itemName == item.itemName)
                    {
                        itemInventory.RemoveAt(i);
                    }
                }
                break;

        }
        

        UpdateStats();

    }
    public void RemoveItemStack(Item item, int amount)
    {


        switch (item.itemType)
        {
            case Item.ItemType.Weapon:
                
                break;
            case Item.ItemType.Equipment:
                
                break;
            case Item.ItemType.Consumable:
                for (int i = 0; i < consumableInventory.Count; i++)
                {
                    if (consumableInventory[i].consumable.itemName == item.itemName)
                    {
                        consumableInventory[i].stackAmount -= amount;
                        if (consumableInventory[i].stackAmount < 0)
                        {
                            consumableInventory.RemoveAt(i);
                        }

                    }
                }
                break;
            case Item.ItemType.Shield:
                
                break;
            default:
                for (int i = 0; i < itemInventory.Count; i++)
                {
                    if (itemInventory[i].item.itemName == item.itemName)
                    {
                        itemInventory[i].stackAmount -= amount;
                        if (itemInventory[i].stackAmount < 0)
                        {
                            itemInventory.RemoveAt(i);
                        }

                    }
                }
                break;

        }
        
        UpdateStats();

    }




    public void LoadData(GameData data)
    {
        if (data.itemInventory != null)
        {
            itemInventory = data.itemInventory;
            foreach (InventorySlot itemSlot in itemInventory)
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>(itemSlot.item.spriteAtlasPath);
                itemSlot.item.itemSprite = sprites[itemSlot.item.spriteIndex];
            }
        }

        if (data.weaponInventory != null)
        {
            weaponInventory = data.weaponInventory;
            foreach (Weapon weapon in weaponInventory)
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>(weapon.spriteAtlasPath);
                weapon.itemSprite = sprites[weapon.spriteIndex];

                if(weapon.weaponType == Weapon.WeaponType.Bow)
                {
                    
                    Sprite[] bowSprites = Resources.LoadAll<Sprite>(weapon.bowSpriteLocation);
                    for (int i = 0; i < weapon.bowSprites.Count; i ++)
                    {
                        weapon.bowSprites[i] = bowSprites[weapon.bowSpriteIndex[i]];
                    }
                    
                }
                
            }
        }

        if (data.equipmentInventory != null)
        {
            equipmentInventory = data.equipmentInventory;
            foreach (Equipment equipment in equipmentInventory)
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>(equipment.spriteAtlasPath);
                equipment.itemSprite = sprites[equipment.spriteIndex];
            }
        }


        if (data.shieldInventory != null)
        {
            shieldInventory = data.shieldInventory;
            foreach (Shield shield in shieldInventory)
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>(shield.spriteAtlasPath);
                shield.itemSprite = sprites[shield.spriteIndex];
            }
        }


        if (data.consumableInventory != null)
        {
            consumableInventory = data.consumableInventory;
            foreach (ConsumableInventorySlot consumableSlot in consumableInventory)
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>(consumableSlot.consumable.spriteAtlasPath);
                consumableSlot.consumable.itemSprite = sprites[consumableSlot.consumable.spriteIndex];
            }
        }

        if (data.currentWeapon != null)
        {
            #region Equipped Items
            if (data.currentWeapon.isActive)
            {
                currentWeapon = data.currentWeapon;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentWeapon.spriteAtlasPath);
                currentWeapon.itemSprite = sprite[currentWeapon.spriteIndex];

                if (currentWeapon.weaponType == Weapon.WeaponType.Bow)
                {

                    Sprite[] bowSprites = Resources.LoadAll<Sprite>(currentWeapon.bowSpriteLocation);
                    for (int i = 0; i < currentWeapon.bowSprites.Count; i++)
                    {
                        currentWeapon.bowSprites[i] = bowSprites[currentWeapon.bowSpriteIndex[i]];
                    }

                }
            }
        }


        if (data.secondaryWeapon != null)
        {
            if (data.secondaryWeapon.isActive)
            {
                secondaryWeapon = data.secondaryWeapon;
                Sprite[] sprite = Resources.LoadAll<Sprite>(secondaryWeapon.spriteAtlasPath);
                secondaryWeapon.itemSprite = sprite[secondaryWeapon.spriteIndex];

                if (secondaryWeapon.weaponType == Weapon.WeaponType.Bow)
                {

                    Sprite[] bowSprites = Resources.LoadAll<Sprite>(secondaryWeapon.bowSpriteLocation);
                    for (int i = 0; i < secondaryWeapon.bowSprites.Count; i++)
                    {
                        secondaryWeapon.bowSprites[i] = bowSprites[secondaryWeapon.bowSpriteIndex[i]];
                    }

                }
            }
        }


        if (data.currentShield != null)
        {
            if (data.currentShield.isActive)
            {
                currentShield = data.currentShield;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentShield.spriteAtlasPath);
                currentShield.itemSprite = sprite[currentShield.spriteIndex];
            }
        }


        if (data.currentHead != null)
        {
            if (data.currentHead.isActive)
            {
                currentHead = data.currentHead;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentHead.spriteAtlasPath);
                currentHead.itemSprite = sprite[currentHead.spriteIndex];
            }
        }


        if (data.currentChest != null)
        {
            if (data.currentChest.isActive)
            {
                currentChest = data.currentChest;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentChest.spriteAtlasPath);
                currentChest.itemSprite = sprite[currentChest.spriteIndex];
            }
        }

        if (data.currentPants != null)
        {
            if (data.currentPants.isActive)
            {
                currentPants = data.currentPants;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentPants.spriteAtlasPath);
                currentPants.itemSprite = sprite[currentPants.spriteIndex];
            }
        }


        if (data.currentShoes != null)
        {
            if (data.currentShoes.isActive)
            {
                currentShoes = data.currentShoes;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentShoes.spriteAtlasPath);
                currentShoes.itemSprite = sprite[currentShoes.spriteIndex];
            }
        }


        if (data.currentNecklace != null)
        {
            if (data.currentNecklace.isActive)
            {
                currentNecklace = data.currentNecklace;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentNecklace.spriteAtlasPath);
                currentNecklace.itemSprite = sprite[currentNecklace.spriteIndex];
            }
        }

        if (data.currentEarrings != null)
        {
            if (data.currentEarrings.isActive)
            {

                currentEarrings = data.currentEarrings;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentEarrings.spriteAtlasPath);
                currentEarrings.itemSprite = sprite[currentEarrings.spriteIndex];

            }
        }


        if (data.currentRing1 != null)
        {
            if (data.currentRing1.isActive)
            {

                currentRing1 = data.currentRing1;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentRing1.spriteAtlasPath);
                currentRing1.itemSprite = sprite[currentRing1.spriteIndex];

            }
        }

        if (data.currentRing2 != null)
        {
            if (data.currentRing2.isActive)
            {

                currentRing2 = data.currentRing2;
                Sprite[] sprite = Resources.LoadAll<Sprite>(currentRing2.spriteAtlasPath);
                currentRing2.itemSprite = sprite[currentRing2.spriteIndex];

            }
        }

        #endregion


    }

    public void SaveData(GameData data)
    {
        data.itemInventory = itemInventory;
        data.weaponInventory = weaponInventory;
        data.equipmentInventory = equipmentInventory;
        data.shieldInventory = shieldInventory;
        data.consumableInventory = consumableInventory;

        #region Current Equipment
        if (currentWeapon != null)
        {

            data.currentWeapon = currentWeapon;
        }

        if (secondaryWeapon != null)
        {

            data.secondaryWeapon = secondaryWeapon;
        }

        if (currentShield != null)
        {

            data.currentShield = currentShield;
        }


        if (currentHead != null)
        {

            data.currentHead = currentHead;
        }
        if (currentChest != null)
        {

            data.currentChest = currentChest;
        }
        if (currentPants != null)
        {

            data.currentPants = currentPants;
        }
        if (currentShoes != null)
        {

            data.currentShoes = currentShoes;
        }

        if (currentNecklace != null)
        {


            data.currentNecklace = currentNecklace;
        }
        if (currentEarrings != null)
        {

            data.currentEarrings = currentEarrings;
        }
        if (currentRing1 != null)
        {

            data.currentRing1 = currentRing1;
        }
        if (currentRing2 != null)
        {

            data.currentRing2 = currentRing2;
        }
        #endregion
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
        GameEvents.current.ChangeWeaponAbility(weapon.ability1, weapon.ability2, weapon.ability3);
       
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
        if (secondaryWeapon.isActive)
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
        if (currentWeapon.isActive)
        {
            weaponInventory.Add(currentWeapon);                 
        }
        currentWeapon = weapon;
        AbilityManager.Instance.ResetCooldowns();
        ChangeWeapon(currentWeapon);
        UpdateInventoryTab(currentTab);
    }

    public void ChangeSecondaryWeapon(Weapon weapon)
    {
        if (currentWeapon.isActive)
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
            secondaryWeapon = new Weapon();
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
            currentShield = new Shield();
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
                currentHead = new Equipment();
                #endregion
                break;
            case Equipment.EquipmentType.Chest:
                #region Chest

                currentChest = null;
                currentChest = new Equipment();
                #endregion
                break;
            case Equipment.EquipmentType.Pants:
                #region Pants

                currentPants = null;
                currentPants = new Equipment();
                #endregion
                break;
            case Equipment.EquipmentType.Shoes:
                #region Shoes

                currentShoes = null;
                currentShoes = new Equipment();
                #endregion
                break;
            case Equipment.EquipmentType.Necklace:
                #region Necklace

                currentNecklace = null;
                currentNecklace = new Equipment();
                #endregion
                break;
            case Equipment.EquipmentType.Earring:
                #region Earrings

                currentEarrings = null;
                currentEarrings = new Equipment();
                #endregion
                break;
            case Equipment.EquipmentType.Ring:
                if (slotIndex == 6)
                {
                    #region Ring1

                    currentRing1 = null;
                    currentRing1 = new Equipment();
                    #endregion
                }
                if (slotIndex == 7)
                {
                    #region Ring2

                    currentRing2 = null;
                    currentRing2 = new Equipment();
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
        currentWeight = 0;

        #region Weight
        foreach (InventorySlot inventorySlot1 in itemInventory)
        {
            currentWeight += (inventorySlot1.item.itemWeight * inventorySlot1.stackAmount);
        }
        foreach (Weapon weapon1 in weaponInventory)
        {
            currentWeight += weapon1.itemWeight;
        }
        foreach (Equipment equipment1 in equipmentInventory)
        {
            currentWeight += equipment1.itemWeight;
        }
        foreach (Shield shield1 in shieldInventory)
        {
            currentWeight += shield1.itemWeight;
        }
        foreach (ConsumableInventorySlot inventorySlot1 in consumableInventory)
        {
            currentWeight += (inventorySlot1.consumable.itemWeight * inventorySlot1.stackAmount);
        }
        #endregion

        switch (i)
        {
            case 0:
                #region General Tab

                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                
                if (itemInventory.Count > 0)
                {

                    foreach (InventorySlot inventorySlot in itemInventory)
                    {
                        Item item = inventorySlot.item;
                       
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
                                    obj.GetComponent<InventoryItem>().amount = inventorySlot.stackAmount;

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
               
                if (weaponInventory.Count > 0)
                {
                    
                    foreach (Weapon item in weaponInventory)
                    {
                        
                        
                        if (item.itemType == Item.ItemType.Weapon)
                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemWeight.ToString();
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
                
                if (consumableInventory.Count > 0)
                {


                    currentTab = 2;
                    foreach (ConsumableInventorySlot inventorySlot in consumableInventory)
                    {
                        Item item = inventorySlot.consumable;
                       
                        if (item.itemType == Item.ItemType.Consumable)
                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();
                            obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = inventorySlot.stackAmount.ToString();

                            obj.GetComponent<InventoryItem>().item = item;
                            obj.GetComponent<InventoryItem>().amount = inventorySlot.stackAmount;
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
                
                if (itemInventory.Count > 0)
                {

                    foreach (InventorySlot inventorySlot in itemInventory)
                    {
                        Item item = inventorySlot.item;
                       
                        if (item.itemType == Item.ItemType.Material)
                        {
                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * inventorySlot.stackAmount).ToString();
                            obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = inventorySlot.stackAmount.ToString();


                            obj.GetComponent<InventoryItem>().item = item;
                            obj.GetComponent<InventoryItem>().amount = inventorySlot.stackAmount;
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
                
                currentTab = 3;
                #endregion
                break;
            case 4:
                #region Armor


                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                
                if (equipmentInventory.Count > 0)
                {

                    foreach (Equipment item in equipmentInventory)
                    {
                        
                        
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
                                obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemWeight.ToString();
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
               
                currentTab = 4;
                #endregion
                break;
            case 5:
                #region Accessories


                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                
                if (equipmentInventory.Count > 0)
                {

                    foreach (Equipment item in equipmentInventory)
                    {
                        
                        //currentWeight += (item.itemWeight * inventorySlot.stackAmount);
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
                                obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemWeight.ToString();
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
                
                currentTab = 5;
                #endregion
                break;
            case 6:
                #region Shields


                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }
                
                if (shieldInventory.Count > 0)
                {

                    foreach (Shield item in shieldInventory)
                    {
                        
                        
                        if (item.itemType == Item.ItemType.Shield)
                        {


                            GameObject obj = Instantiate(uiObject, uiContent);
                            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemWeight.ToString();
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
                
                currentTab = 6;
                #endregion
                break;
            default:
                break;
        }

        weight.GetComponent<TMPro.TextMeshProUGUI>().text = currentWeight.ToString();
        GameEvents.current.InventoryRefresh(currentWeapon, secondaryWeapon);
        Debug.Log("Inventory Refreshed");
    }


    private void SwapWeapon()
    {
        if (currentWeapon.isActive && secondaryWeapon.isActive)
        {
            Weapon weapon = currentWeapon;           
            currentWeapon = secondaryWeapon;           
            secondaryWeapon = weapon;


            
            ChangeWeapon(currentWeapon);
            
            
            UpdateInventoryTab(currentTab);

            GameEvents.current.SwapWeapon();
        }
        

       
    }

    IEnumerator SpawnCurrentWeapon()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeWeapon(currentWeapon);
        GameEvents.current.SpawnCurrentEquipment();
        GameEvents.current.InventoryRefresh(currentWeapon, secondaryWeapon);
    }

    public void SpawnCurrentWeapons()
    {
        GameEvents.current.SpawnCurrentEquipment();
        GameEvents.current.InventoryRefresh(currentWeapon, secondaryWeapon);

    }

    private void UpdateStats()
    {
        #region Damage
        float damage = 0f;
        float equipmentMeleeDamageBoost = 0f;
        float equipmentMagicDamageBoost = 0f;
        if (currentWeapon.isActive)
        {
            damage += currentWeapon.damage;
        }


        #endregion

        #region MagicDamage
        float magicDamage = 0f;
        if (currentWeapon.isActive)
        {
            magicDamage += currentWeapon.magicDamage;
        }

        #endregion

        #region Armor
        float armor = 0f;
        if (currentHead != null)
        {

            if (currentHead.isActive)
            {
                equipmentMeleeDamageBoost += currentHead.bonusMeleeDamage;
                equipmentMagicDamageBoost += currentHead.bonusMagicDamage;
                armor += currentHead.armor;
            }
        }
        if (currentChest != null)
        {
            if (currentChest.isActive)
            {
                equipmentMeleeDamageBoost += currentChest.bonusMeleeDamage;
                equipmentMagicDamageBoost += currentChest.bonusMagicDamage;
                armor += currentChest.armor;
            }
        }
        if (currentPants != null)
        {
            if (currentPants.isActive)
            {
                equipmentMeleeDamageBoost += currentPants.bonusMeleeDamage;
                equipmentMagicDamageBoost += currentPants.bonusMagicDamage;
                armor += currentPants.armor;
            }
        }
        if (currentShoes != null)
        {
            if (currentShoes.isActive)
            {
                equipmentMeleeDamageBoost += currentShoes.bonusMeleeDamage;
                equipmentMagicDamageBoost += currentShoes.bonusMagicDamage;
                armor += currentShoes.armor;
            }
        }
        if (currentNecklace != null)
        {
            if (currentNecklace.isActive)
            {
                equipmentMeleeDamageBoost += currentNecklace.bonusMeleeDamage;
                equipmentMagicDamageBoost += currentNecklace.bonusMagicDamage;
                armor += currentNecklace.armor;
            }
        }
        if (currentEarrings != null)
        {
            if (currentEarrings.isActive)
            {
                equipmentMeleeDamageBoost += currentEarrings.bonusMeleeDamage;
                equipmentMagicDamageBoost += currentEarrings.bonusMagicDamage;
                armor += currentEarrings.armor;
            }
        }
        if (currentRing1 != null)
        {
            if (currentRing1.isActive)
            {
                equipmentMeleeDamageBoost += currentRing1.bonusMeleeDamage;
                equipmentMagicDamageBoost += currentRing1.bonusMagicDamage;
                armor += currentRing1.armor;
            }
        }
        if (currentRing2 != null)
        {
            if (currentRing2.isActive)
            {
                equipmentMeleeDamageBoost += currentRing2.bonusMeleeDamage;
                equipmentMagicDamageBoost += currentRing2.bonusMagicDamage;
                armor += currentRing2.armor;
            }
        }
        magicDamage += equipmentMagicDamageBoost;
        damage += equipmentMeleeDamageBoost;
        #endregion

        #region Crit
        float critRate = 0f;
        float critDamage = 1f;
        if (currentWeapon.isActive)
        {
            critRate += currentWeapon.critRate;
            critDamage *= currentWeapon.critDamage;
        }

        #endregion

        #region KnockBackForce
        float knockBackForce = 0f;
        if (currentWeapon.isActive)
        {
            knockBackForce += currentWeapon.knockBackForce;
        }

        #endregion

        #region SlowDebuff
        float slowDebuff = 0f;
        if (currentWeapon.isActive)
        {
            slowDebuff += currentWeapon.speedMultiplier;
        }

        #endregion

        #region SlowDebuffTime
        float slowDebuffTime = 0;
        if (currentWeapon.isActive)
        {
            slowDebuffTime += currentWeapon.slowDownLength;
        }
        #endregion

        #region Mana
        float manaCost = 0f;
        if (currentWeapon.isActive)
        {
            manaCost += currentWeapon.manaCost;
        }
        #endregion

        #region ShotForce
        float shotForce = 0f;
        if (currentWeapon.isActive)
        {
            shotForce = currentWeapon.force;
        }
        #endregion

        #region LocalPosition
        Vector2 localPos = new Vector2(0f, 0f);
        if (currentWeapon.isActive)
        {
            localPos = currentWeapon.localPosition;
        }

        #endregion

        GameEvents.current.UpdateDamageType(currentWeapon.damageType);
        GameEvents.current.UpdateArmorStat(armor);
        GameEvents.current.UpdateInventoryStats(damage, magicDamage, knockBackForce, slowDebuff,
slowDebuffTime, manaCost, shotForce, critRate, critDamage, localPos, currentWeapon.poise);
    }


}
