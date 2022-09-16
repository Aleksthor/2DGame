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
    public GameObject current;

    int framesToReset = 10;
    int framesCount = 0;


    private bool swapped = false;


    private void Start()
    {
        GameEvents.current.OnSwapWeapon += SwapWeapon;
        GameEvents.current.OnInventoryRefresh += InventoryRefresh;
    }

    private void Update()
    {
        
        if (swapped)
        {
            framesCount++;
            if (framesCount > framesToReset)
            {
                if (current != null)
                {
                    current.GetComponent<DragDrop>().enabled = true;
                    swapped = false;
                    framesCount = 0;
                }
                
                
            }
        }
        if (gameObject.transform.Find("ItemUI(Clone)") == null && current != null)
        {
            GameEvents.current.AddItem(current.GetComponent<InventoryItem>().item);
            GameEvents.current.RemoveCurrentSecondaryItem((Weapon)current.GetComponent<InventoryItem>().item);
            framesCount = 0;
            current = null;
            
        }

    }



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            
            if (eventData.pointerDrag.GetComponent<InventoryItem>().item.itemType == Item.ItemType.Weapon)
            {
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;


                if (slotIndex == 1)
                {                   


                    foreach (Transform child in gameObject.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                    eventData.pointerDrag.transform.SetParent(transform, false);
                    eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

                    GameEvents.current.RemoveItem(eventData.pointerDrag.GetComponent<InventoryItem>().item);
                    GameEvents.current.ChangeCurrentWeapon((Weapon)eventData.pointerDrag.GetComponent<InventoryItem>().item);

                }
                if (slotIndex == 2)
                {


                    foreach (Transform child in gameObject.transform)
                    {
                        GameEvents.current.AddItem(child.gameObject.GetComponent<InventoryItem>().item);
                        GameObject.Destroy(child.gameObject);
                    }
                    eventData.pointerDrag.transform.SetParent(transform, false);
                    eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

                    GameEvents.current.RemoveItem(eventData.pointerDrag.GetComponent<InventoryItem>().item);
                    GameEvents.current.ChangeSecondaryWeapon((Weapon)eventData.pointerDrag.GetComponent<InventoryItem>().item);

                    current = eventData.pointerDrag;

                }

                swapped = true;
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
                        obj.transform.SetParent(transform);
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
