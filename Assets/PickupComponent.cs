using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupComponent : MonoBehaviour
{
    public Item item;
    public int health;
    public int amount;

    public void PickupHit()
    {
        health -= 1;
        if (health <= 0)
        {
            InventoryManager.Instance.AddItemToStack(item, amount);
            gameObject.SetActive(false);
        }
    }
}
