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
            
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }


    private void Start()
    {
        GameEvents.current.OnCharacterCreation += CharacterCreatorStart;
        GameEvents.current.OnCharacterCreationEnd += CharacterCreatorEnd;
    }

    private void CharacterCreatorStart()
    {
        gameObject.SetActive(false);
    }

    private void CharacterCreatorEnd()
    {
        gameObject.SetActive(true);
    }

}
