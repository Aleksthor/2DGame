using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Item> inventory;

    public Weapon currentWeapon;
    public Weapon secondaryWeapon;


    // References to the UI Elements for inventory
    public GameObject uiObject;
    public Transform uiContent;
    public Transform uiItemInfo;


    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }


    // Debug purposes in Update
    void Update()
    {

        if (Input.GetButton("1"))
        {
            ChangeWeapon((Weapon)inventory[0]);                    
        }

        if (Input.GetButton("2"))
        {
            ChangeWeapon((Weapon)inventory[1]);
        }

        if (Input.GetButton("3"))
        {
            ChangeWeapon((Weapon)inventory[2]);
        }

    }



    public void ChangeWeapon(Weapon weapon)
    {

        // Sends these events and updates the player with these
        GameEvents.current.ChangeWeapon(weapon);
        GameEvents.current.ChangeStats(weapon.damage, weapon.knockBackForce, weapon.speedMultiplier, weapon.slowDownLength, weapon.manaCost, weapon.force, weapon.localPosition);
        GameEvents.current.ChangeWeaponCollider(weapon.colliderPointX, weapon.colliderPointY, (int)weapon.weaponType);

        secondaryWeapon = currentWeapon;
        currentWeapon = weapon;

    }


    public void UpdateInventory()
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



        foreach (Transform item in uiContent)
        {
            Destroy(item.gameObject);
        }


        foreach (Item item in inventory)
        {
            
            GameObject obj = Instantiate(uiObject, uiContent);

            obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
            obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
            obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();
            obj.GetComponent<InventoryItem>().item = item;
            obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;

        }
    }


    public void UpdateInventoryTab(int i)
    {
        switch (i)
        {
            case 0:
                #region General Tab
                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";



                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }


                foreach (Item item in inventory)
                {
                    if (item.itemType != Item.ItemType.Weapon)
                    {
                        GameObject obj = Instantiate(uiObject, uiContent);

                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();
                        obj.GetComponent<InventoryItem>().item = item;
                        obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
                    }

                }

                #endregion 
                break;
            case 1:
                #region WeaponsTab

                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";



                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }


                foreach (Item item in inventory)
                {
                    if (item.itemType == Item.ItemType.Weapon)
                    {
                        GameObject obj = Instantiate(uiObject, uiContent);

                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();
                        obj.GetComponent<InventoryItem>().item = item;
                        obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
                    }

                }

                #endregion
                break;
            case 2:
                #region Consumables
                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";



                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }


                foreach (Item item in inventory)
                {
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
                #endregion
                break;
            case 3:
                #region Materials
                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = "";



                foreach (Transform item in uiContent)
                {
                    Destroy(item.gameObject);
                }


                foreach (Item item in inventory)
                {
                    if (item.itemType == Item.ItemType.Material)
                    {
                        GameObject obj = Instantiate(uiObject, uiContent);

                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (item.itemWeight * item.stackAmount).ToString();
                        obj.GetComponent<InventoryItem>().item = item;
                        obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
                    }

                }
                #endregion
                break;
            default:
                break;
        }
    }



}
