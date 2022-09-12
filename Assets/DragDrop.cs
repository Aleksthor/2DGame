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
    }

    private void Start()
    {
        canvas = GameObject.Find("UI_Canvas").GetComponent<Canvas>();
        startParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("onBeginDrag");
        canvasGroup.blocksRaycasts = false;
        gameObject.transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Ondrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("onEndDrag");
        gameObject.transform.SetParent(startParent, false);
        canvasGroup.blocksRaycasts = true;                                          
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("onClick");
    }


}
