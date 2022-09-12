using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : MonoBehaviour, IDropHandler
{


    public int slotIndex;
    private Weapon currentWeapon;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<InventoryItem>().item.itemType == Item.ItemType.Weapon)
            {
                eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
                foreach (Transform child in gameObject.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                if (slotIndex == 1)
                {
                    GameEvents.current.ChangeCurrentWeapon((Weapon)eventData.pointerDrag.GetComponent<InventoryItem>().item);
                }
                eventData.pointerDrag.transform.SetParent(transform, false);
                eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(0,1);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
                
            }
        }
    }
}
