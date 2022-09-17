using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSingleton : MonoBehaviour
{
    public static HUDSingleton instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
}
