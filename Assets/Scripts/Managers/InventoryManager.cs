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
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }



    private void Start()
    {
        GameEvents.current.OnChangeCurrentWeapon += ChangeCurrentWeapon;
        GameEvents.current.OnChangeSecondaryWeapon += ChangeSecondaryWeapon;
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
        GameEvents.current.ChangeStats(weapon.damage, weapon.knockBackForce, weapon.speedMultiplier, weapon.slowDownLength, weapon.manaCost, weapon.force, weapon.localPosition);
        GameEvents.current.ChangeWeaponCollider(weapon.colliderPointX, weapon.colliderPointY, (int)weapon.weaponType);

    }

    public void ChangeCurrentWeapon(Weapon weapon)
    {
        if (currentWeapon != null)
        {
            inventory.Add(currentWeapon);
            
            
        }
        inventory.Remove(weapon);
        currentWeapon = weapon;
        ChangeWeapon(currentWeapon);
        UpdateInventoryTab(currentTab);
    }


    public void ChangeSecondaryWeapon(Weapon weapon)
    {
        if (secondaryWeapon != null)
        {
            inventory.Add(secondaryWeapon);
            
        }
        inventory.Remove(weapon);
        secondaryWeapon = weapon;
        UpdateInventoryTab(currentTab);
    }


    public void UpdateInventoryTab(int i)
    {

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
                        Debug.Log(item);
                        Debug.Log(item.itemRarity);
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
                    Debug.Log(item);
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


}
