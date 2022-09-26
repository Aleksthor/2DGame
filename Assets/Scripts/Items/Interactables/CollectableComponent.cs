using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableComponent : MonoBehaviour
{
    public Item item;
    public int health;
    public int min;
    public int max;

    public void PickupHit()
    {
        health -= 1;
        if (health <= 0)
        {
            transform.GetComponent<RenderDistance>().isDead = true;
            InventoryManager.Instance.AddItemToStack(item, Random.Range(min,max));
            gameObject.SetActive(false);
        }
    }
}
