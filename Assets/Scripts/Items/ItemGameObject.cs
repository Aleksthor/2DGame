using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{

    public Item item;
    private GameObject playerObject;
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
                InventoryManager.Instance.AddItem(item);
                Destroy(gameObject);  
            }

        }
    }
}
