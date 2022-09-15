using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public Item item;

    public Transform uiItemInfo;


    public void UpdateItemInfo()
    {
        switch ((int)item.itemType)
        {
            case 0:
                #region Weapon
                Weapon weapon = (Weapon)item;
                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = weapon.weaponType.ToString();
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = weapon.itemName;



                if (weapon.weaponType == Weapon.WeaponType.Wand || weapon.weaponType == Weapon.WeaponType.Staff)
                {
                    uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "Magic Damage";
                    uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = weapon.magicDamage.ToString();
                    uiItemInfo.Find("Background").transform.Find("text-3").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    uiItemInfo.Find("Background").transform.Find("Info3").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "Mana Cost";
                    uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = weapon.manaCost.ToString();
                }
                else
                {
                    uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "Damage";
                    uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = weapon.damage.ToString();
                    uiItemInfo.Find("Background").transform.Find("text-3").GetComponent<TMPro.TextMeshProUGUI>().text = "Crit Damage";
                    uiItemInfo.Find("Background").transform.Find("Info3").GetComponent<TMPro.TextMeshProUGUI>().text = weapon.critDamage.ToString();
                    uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "Crit Rate";
                    uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = weapon.critRate.ToString();
                }
                
                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "Description";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = weapon.itemDescription;
                #endregion
                break;
            case 1:
                #region Consumables
                Consumable consumable = (Consumable)item;
                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = consumable.itemType.ToString();
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = consumable.itemName;

                uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "Healing";
                uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = consumable.healing.ToString();

                uiItemInfo.Find("Background").transform.Find("text-3").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info3").GetComponent<TMPro.TextMeshProUGUI>().text = "";

                uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                
                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "Description";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = consumable.itemDescription;
                #endregion
                break;
            case 2:
                #region Materials
                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemType.ToString();
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemName;

                uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = "";

                uiItemInfo.Find("Background").transform.Find("text-3").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info3").GetComponent<TMPro.TextMeshProUGUI>().text = "";

                uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";

                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "Description";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = item.itemDescription;
                #endregion
                break;
            case 3:
                #region Equipment
                Equipment equipment = (Equipment)item;
                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = equipment.equipmentType.ToString();
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = equipment.itemName;

                uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "Armor";
                uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = equipment.armor.ToString();

                uiItemInfo.Find("Background").transform.Find("text-3").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info3").GetComponent<TMPro.TextMeshProUGUI>().text = "";

                uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";

                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "Description";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = equipment.itemDescription;
                #endregion
                break;
            case 4:
                #region Shield
                Shield shield = (Shield)item;
                uiItemInfo.Find("Background").transform.Find("ItemType").GetComponent<TMPro.TextMeshProUGUI>().text = shield.itemType.ToString();
                uiItemInfo.Find("Background").transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = shield.itemName;

                uiItemInfo.Find("Background").transform.Find("text-1").GetComponent<TMPro.TextMeshProUGUI>().text = "Blcck";
                uiItemInfo.Find("Background").transform.Find("Info1").GetComponent<TMPro.TextMeshProUGUI>().text = shield.block.ToString();

                uiItemInfo.Find("Background").transform.Find("text-3").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info3").GetComponent<TMPro.TextMeshProUGUI>().text = "";

                uiItemInfo.Find("Background").transform.Find("text-4").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                uiItemInfo.Find("Background").transform.Find("Info4").GetComponent<TMPro.TextMeshProUGUI>().text = "";

                uiItemInfo.Find("Background").transform.Find("text-Description").GetComponent<TMPro.TextMeshProUGUI>().text = "Description";
                uiItemInfo.Find("Background").transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = shield.itemDescription;
                #endregion
                break;

            default:
                break;

        }
    }
}
