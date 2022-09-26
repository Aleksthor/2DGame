using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemPickup : MonoBehaviour
{
    private Item item;
    private TMPro.TextMeshProUGUI itemName;
    private TMPro.TextMeshProUGUI itemAmount;
    private Image image;



    public float timeToLive = 5f;


    private void Update()
    {
        timeToLive -= Time.deltaTime;



        if (timeToLive < 0)
        {
            Destroy(gameObject);
        }
    }



    public void PickUp(Item itemPickedUp, int amount)
    {
        itemName = transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>();
        itemAmount = transform.Find("Amount").GetComponent<TMPro.TextMeshProUGUI>();
        image = transform.GetComponent<Image>();


        item = itemPickedUp;

        itemName.text = item.itemName;


        if (item.isStackable)
        {
            itemAmount.text = "x" + amount;
        }
        else
        {
            itemAmount.text = "x1"; 
        }


        switch(item.itemRarity)
        {
            case Item.ItemRarity.Common:
                image.color = new Color32(170, 190, 180, 255);
                break;
            case Item.ItemRarity.UnCommon:
                image.color = new Color32(90, 180, 100, 255);
                break;
            case Item.ItemRarity.Rare:
                image.color = new Color32(90, 160, 180, 255);
                break;
            case Item.ItemRarity.Epic:
                image.color = new Color32(170, 190, 180, 255);
                break;
            case Item.ItemRarity.Legendary:
                image.color = new Color32(220, 170, 60, 255);
                break;

        }
    }
}
