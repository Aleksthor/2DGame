using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableGameObject : MonoBehaviour
{
    public Consumable consumable;
    private GameObject playerObject;
    public int amount;
    public float pickupRange = 1f;


    private void Start()
    {
        playerObject = PlayerSingleton.instance.gameObject;
    }


    private void Update()
    {

        if (Vector2.Distance(playerObject.transform.position, transform.position) < pickupRange)
        {
            transform.position = Vector2.Lerp(transform.position, playerObject.transform.position, Time.deltaTime);
            if (Vector2.Distance(playerObject.transform.position, transform.position) < pickupRange * 0.2f)
            {
                InventoryManager.Instance.AddItemToStack(consumable, amount);
                Destroy(gameObject);
            }

        }
    }
}
