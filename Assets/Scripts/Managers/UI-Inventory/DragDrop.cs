using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private InventoryItem inventoryItem;
    private CanvasGroup canvasGroup;
    private Transform startParent;

    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        inventoryItem = GetComponent<InventoryItem>();
        canvasGroup = GetComponent<CanvasGroup>();
        startParent = HUDSingleton.instance.transform.Find("Inventory").transform.Find("ItemList").transform.Find("Viewport").transform.Find("Content").transform;
    }

    private void Start()
    {
        canvas = GameObject.Find("UI_Canvas").GetComponent<Canvas>();
      
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        canvasGroup.blocksRaycasts = false;
        gameObject.transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        gameObject.transform.SetParent(startParent, false);
        InventoryManager.Instance.UpdateInventoryTab(InventoryManager.Instance.currentTab);
        canvasGroup.blocksRaycasts = true;                                          
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }


}
