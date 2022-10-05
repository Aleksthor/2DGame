using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmorSlot : MonoBehaviour, IDropHandler
{


    public int slotIndex;
    public GameObject uiObject;
    public GameObject current;

    int framesToReset = 10;
    int framesCount = 0;
    private bool swapped = false;
    public Transform uiItemInfo;

    private void Awake()
    {
        GameEvents.current.OnSpawnCurrentEquipment += SpawnCurrentEquipment;
    }

    private void Start()
    {
        if (uiItemInfo == null)
        {
            uiItemInfo = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemInfo").transform;
        }

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
            GameEvents.current.RemoveCurrentEquipment((Equipment)current.GetComponent<InventoryItem>().item, slotIndex);
           
            framesCount = 0;
            current = null;
        }


        
    }



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<InventoryItem>().item.itemType == Item.ItemType.Equipment)
            {
                Equipment equipment = (Equipment)eventData.pointerDrag.GetComponent<InventoryItem>().item;
                if (equipment != null)
                {
                    switch (slotIndex)
                    {
                        case 0:
                            #region Head
                            if (equipment.equipmentType == Equipment.EquipmentType.Head)
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

                                GameEvents.current.RemoveItem(equipment);
                                GameEvents.current.ChangeCurrentEquipment(equipment, slotIndex);
                                current = eventData.pointerDrag;
                                // Game Event Change Head

                            }
                            #endregion
                            break;
                        case 1:
                            #region Chest
                            if (equipment.equipmentType == Equipment.EquipmentType.Chest)
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

                                GameEvents.current.RemoveItem(equipment);
                                GameEvents.current.ChangeCurrentEquipment(equipment, slotIndex);
                                current = eventData.pointerDrag;


                            }
                            #endregion
                            break;
                        case 2:
                            #region Pants
                            if (equipment.equipmentType == Equipment.EquipmentType.Pants)
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

                                GameEvents.current.RemoveItem(equipment);
                                GameEvents.current.ChangeCurrentEquipment(equipment, slotIndex);
                                current = eventData.pointerDrag;


                            }
                            #endregion
                            break;
                        case 3:
                            #region Shoes
                            if (equipment.equipmentType == Equipment.EquipmentType.Shoes)
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

                                GameEvents.current.RemoveItem(equipment);
                                GameEvents.current.ChangeCurrentEquipment(equipment, slotIndex);
                                current = eventData.pointerDrag;


                            }
                            #endregion
                            break;
                        case 4:
                            #region Necklace
                            if (equipment.equipmentType == Equipment.EquipmentType.Necklace)
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


                                GameEvents.current.RemoveItem(equipment);
                                GameEvents.current.ChangeCurrentEquipment(equipment, slotIndex);
                                current = eventData.pointerDrag;


                            }
                            #endregion
                            break;
                        case 5:
                            #region Earrings
                            if (equipment.equipmentType == Equipment.EquipmentType.Earring)
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

                                GameEvents.current.RemoveItem(equipment);
                                GameEvents.current.ChangeCurrentEquipment(equipment, slotIndex);
                                current = eventData.pointerDrag;


                            }
                            #endregion
                            break;
                        case 6:
                            #region Ring
                            if (equipment.equipmentType == Equipment.EquipmentType.Ring)
                            {

                                eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
                                eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
                                foreach (Transform child in gameObject.transform)
                                {
                                    GameObject.Destroy(child.gameObject);
                                }
                                eventData.pointerDrag.transform.SetParent(transform, false);
                                eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

                                GameEvents.current.RemoveItem(equipment);
                                GameEvents.current.ChangeCurrentEquipment(equipment, slotIndex);
                                current = eventData.pointerDrag;


                            }
                            #endregion
                            break;
                        case 7:
                            #region Ring
                            if (equipment.equipmentType == Equipment.EquipmentType.Ring)
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

                                GameEvents.current.RemoveItem(equipment);
                                GameEvents.current.ChangeCurrentEquipment(equipment, slotIndex);
                                current = eventData.pointerDrag;


                            }
                            #endregion
                            break;
                        default:
                            break;

                    }


                    swapped = true;
                }
            }

        }
    }



    public void SpawnCurrentEquipment()
    {
        
        InventoryManager inventoryManager = InventoryManager.Instance;

        switch (slotIndex)
        {
            case 0:
                #region Head
                if (inventoryManager.currentHead.isActive)
                {
                    GameObject obj = Instantiate(uiObject, transform);
                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentHead.itemName;
                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventoryManager.currentHead.itemSprite;
                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentHead.itemWeight.ToString();
                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    obj.GetComponent<InventoryItem>().item = inventoryManager.currentHead;
                    obj.transform.SetParent(transform);
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
                    current = obj;
                }
                #endregion
                break;
            case 1:
                #region Chest
                if (inventoryManager.currentChest.isActive)
                {
                    GameObject obj = Instantiate(uiObject, transform);
                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentChest.itemName;
                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventoryManager.currentChest.itemSprite;
                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentChest.itemWeight.ToString();
                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    obj.GetComponent<InventoryItem>().item = inventoryManager.currentChest;
                    obj.transform.SetParent(transform);
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
                    current = obj;
                }
                #endregion
                break;
            case 2:
                #region Pants
                if (inventoryManager.currentPants.isActive)
                {
                    GameObject obj = Instantiate(uiObject, transform);
                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentPants.itemName;
                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventoryManager.currentPants.itemSprite;
                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentPants.itemWeight.ToString();
                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    obj.GetComponent<InventoryItem>().item = inventoryManager.currentPants;
                    obj.transform.SetParent(transform);
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
                    current = obj;
                }
                #endregion
                break;
            case 3:
                #region Shoes
                if (inventoryManager.currentShoes.isActive)
                {
                    GameObject obj = Instantiate(uiObject, transform);
                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentShoes.itemName;
                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventoryManager.currentShoes.itemSprite;
                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentShoes.itemWeight.ToString();
                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    obj.GetComponent<InventoryItem>().item = inventoryManager.currentShoes;
                    obj.transform.SetParent(transform);
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
                    current = obj;
                }
                #endregion
                break;
            case 4:
                #region Necklace
                if (inventoryManager.currentNecklace.isActive)
                {
                    GameObject obj = Instantiate(uiObject, transform);
                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentNecklace.itemName;
                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventoryManager.currentNecklace.itemSprite;
                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentNecklace.itemWeight.ToString();
                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    obj.GetComponent<InventoryItem>().item = inventoryManager.currentNecklace;
                    obj.transform.SetParent(transform);
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
                    current = obj;
                }
                #endregion
                break;
            case 5:
                #region Earrings
                if (inventoryManager.currentEarrings.isActive)
                {
                    Debug.Log("EarringsActive");
                    GameObject obj = Instantiate(uiObject, transform);
                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentEarrings.itemName;
                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventoryManager.currentEarrings.itemSprite;
                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentEarrings.itemWeight.ToString();
                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    obj.GetComponent<InventoryItem>().item = inventoryManager.currentEarrings;
                    obj.transform.SetParent(transform);
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
                    current = obj;
                }
                #endregion
                break;
            case 6:
                #region Ring
                if (inventoryManager.currentRing1.isActive)
                {
                    GameObject obj = Instantiate(uiObject, transform);
                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentRing1.itemName;
                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventoryManager.currentRing1.itemSprite;
                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentRing1.itemWeight.ToString();
                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    obj.GetComponent<InventoryItem>().item = inventoryManager.currentRing1;
                    obj.transform.SetParent(transform);
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
                    current = obj;
                }
                #endregion
                break;
            case 7:
                #region Ring
                if (inventoryManager.currentRing2.isActive)
                {
                    GameObject obj = Instantiate(uiObject, transform);
                    obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentRing2.itemName;
                    obj.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventoryManager.currentRing2.itemSprite;
                    obj.transform.Find("ItemWeight").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryManager.currentRing2.itemWeight.ToString();
                    obj.transform.Find("StackAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    obj.GetComponent<InventoryItem>().item = inventoryManager.currentRing2;
                    obj.transform.SetParent(transform);
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
                    current = obj;
                }
                #endregion
                break;
            default:
                break;

        }
    }




}
