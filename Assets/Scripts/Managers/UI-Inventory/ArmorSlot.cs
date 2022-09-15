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



    private void Start()
    { 
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
            Debug.Log("Runnning");
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
                        if (equipment.equipmentType == Equipment.EquipmentType.Earring  )
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
