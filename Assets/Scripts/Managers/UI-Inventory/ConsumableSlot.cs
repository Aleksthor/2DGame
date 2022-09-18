using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumableSlot : MonoBehaviour, IDropHandler
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

            GameEvents.current.AddItem(current.GetComponent<InventoryItem>().item);
            switch(slotIndex)
            {
                case 1:
                    ConsumableManager.Instance.consumable1 = null;
                    break;
                case 2:
                    ConsumableManager.Instance.consumable2 = null;
                    break;
                case 3:
                    ConsumableManager.Instance.consumable3 = null;
                    break;
                default:
                    break;

            }

            framesCount = 0;
            current = null;
        }
    }



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Consumable consumable = (Consumable)eventData.pointerDrag.GetComponent<InventoryItem>().item;
            if (consumable != null)
            {
                switch (slotIndex)
                {
                    case 0:
                        #region ConsumableSlot1

                        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
                        foreach (Transform child in gameObject.transform)
                        {
                            if (child.tag == "UIElement")
                            {
                                GameObject.Destroy(child.gameObject);
                            }
                            
                        }
                        eventData.pointerDrag.transform.SetParent(transform, false);
                        eventData.pointerDrag.transform.SetSiblingIndex(1);
                        eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -50f);
                        Debug.Log(consumable);
                        ConsumableManager.Instance.ChangeConsumable1(consumable);
                        GameEvents.current.RemoveItem(consumable);

                        current = eventData.pointerDrag;
                      


                        #endregion
                        break;
                    case 1:
                        #region ConsumableSlot2
                        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
                        foreach (Transform child in gameObject.transform)
                        {
                            if (child.tag == "UIElement")
                            {
                                GameObject.Destroy(child.gameObject);
                            }

                        }
                        eventData.pointerDrag.transform.SetParent(transform, false);
                        eventData.pointerDrag.transform.SetSiblingIndex(1);
                        eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -50f);

                        GameEvents.current.RemoveItem(consumable);
                        ConsumableManager.Instance.ChangeConsumable2(consumable);
                        current = eventData.pointerDrag;

                
                        #endregion
                        break;
                    case 2:
                        #region ConsumableSlot3
                        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
                        foreach (Transform child in gameObject.transform)
                        {
                            if (child.tag == "UIElement")
                            {
                                GameObject.Destroy(child.gameObject);
                            }

                        }
                        eventData.pointerDrag.transform.SetParent(transform, false);
                        eventData.pointerDrag.transform.SetSiblingIndex(1);
                        eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -50f);

                        GameEvents.current.RemoveItem(consumable);
                        ConsumableManager.Instance.ChangeConsumable3(consumable);
                        current = eventData.pointerDrag;
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