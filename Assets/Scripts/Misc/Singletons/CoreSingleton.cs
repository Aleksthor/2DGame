using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSingleton : MonoBehaviour
{

    public static CoreSingleton instance;
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
