using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentGameObject : MonoBehaviour
{
    public Equipment equipment;
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
                InventoryManager.Instance.AddItem(equipment);
                Destroy(gameObject);
            }

        }
    }
}
