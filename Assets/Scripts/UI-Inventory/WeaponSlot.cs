using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour, IDropHandler
{


    public int slotIndex;
    public Transform otherWeaponSlot;
    public GameObject uiObject;
    public Transform uiItemInfo;


    private void Start()
    {
        GameEvents.current.OnSwapWeapon += SwapWeapon;
        GameEvents.current.OnInventoryRefresh += InventoryRefresh;
    }



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            
            if (eventData.pointerDrag.GetComponent<InventoryItem>().item.itemType == Item.ItemType.Weapon)
            {
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
                foreach (Transform child in gameObject.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                eventData.pointerDrag.transform.SetParent(transform, false);
                eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

                if (slotIndex == 1)
                {
                    GameEvents.current.ChangeCurrentWeapon((Weapon)eventData.pointerDrag.GetComponent<InventoryItem>().item);
                }
                if (slotIndex == 2)
                {
                    GameEvents.current.ChangeSecondaryWeapon((Weapon)eventData.pointerDrag.GetComponent<InventoryItem>().item);
                }

                
            }
        }
    }

    private void SwapWeapon()
    {

        
        if (gameObject.transform.Find("ItemUI(Clone)") != null)
        {
            gameObject.transform.Find("ItemUI(Clone)").transform.SetParent(otherWeaponSlot, false);
        }
       
    }

    private void InventoryRefresh(Weapon current, Weapon secondary)
    {
        if (gameObject.transform.Find("ItemUI(Clone)") == null)
        {
            
            switch(slotIndex)
            {
                case 1:
                    if (current != null)
                    {
                        
                        GameObject obj = Instantiate(uiObject, transform);
                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = current.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = current.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (current.itemWeight * current.stackAmount).ToString();
                        obj.GetComponent<InventoryItem>().item = current;
                        obj.transform.parent = transform;
                        obj.GetComponent<DragDrop>().enabled = false;
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
                        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
                        obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
                    }

                    break;
                case 2:
                    if (secondary != null)
                    {
                        GameObject obj = Instantiate(uiObject, transform);
                        obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = secondary.itemName;
                        obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = secondary.itemSprite;
                        obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = (secondary.itemWeight * secondary.stackAmount).ToString();
                        obj.GetComponent<InventoryItem>().item = secondary;
                        obj.transform.parent = transform;
                        obj.GetComponent<DragDrop>().enabled = false;
                        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
                        obj.GetComponent<InventoryItem>().uiItemInfo = uiItemInfo;
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
