using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerParent : MonoBehaviour
{
    public static ManagerParent instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

}
