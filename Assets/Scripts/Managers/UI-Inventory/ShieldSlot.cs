using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldSlot : MonoBehaviour, IDropHandler
{

    public GameObject uiObject;
    public Transform uiItemInfo;
    public GameObject current;

    int framesToReset = 10;
    int framesCount = 0;


    private bool swapped = false;

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
            GameEvents.current.RemoveCurrentShield((Shield)current.GetComponent<InventoryItem>().item);
            GameEvents.current.ChangeShield(null);
            framesCount = 0;
            current = null;

        }

    }



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {

            if (eventData.pointerDrag.GetComponent<InventoryItem>().item.itemType == Item.ItemType.Shield)
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

                GameEvents.current.RemoveItem(eventData.pointerDrag.GetComponent<InventoryItem>().item);
                GameEvents.current.ChangeCurrentShield((Shield)eventData.pointerDrag.GetComponent<InventoryItem>().item);

                current = eventData.pointerDrag;
                swapped = true;
            }
        }
    }
}