using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemOnDeath : MonoBehaviour
{

    public GameObject objectToRemove;
    private void OnDestroy()
    {
        Destroy(objectToRemove);
    }
}
