using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabButtonInput : MonoBehaviour
{
    CharacterCreationManager characterCreationManager;

    [SerializeField] Button prefabButton;
    [SerializeField] Image prefabImage;
    [SerializeField] Sprite prefabSprite;


    //--------------------


    private void Awake()
    {
        characterCreationManager = FindObjectOfType<CharacterCreationManager>();

        prefabImage.gameObject.GetComponent<Image>();
    }
    private void Start()
    {
        prefabSprite = prefabImage.sprite;

        prefabButton.onClick.AddListener(ButtonListener);
    }


    //--------------------


    void ButtonListener()
    {
        if (characterCreationManager.headTop)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headTop\" Button is clicked: ");
        }

        if (characterCreationManager.headBottom)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headBottom\" Button is clicked: ");
        }

        if (characterCreationManager.ear)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headBottom\" Button is clicked: ");
        }

        if (characterCreationManager.hair)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headBottom\" Button is clicked: ");
        }

        if (characterCreationManager.facialhair)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headBottom\" Button is clicked: ");
        }

        if (characterCreationManager.eye)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headBottom\" Button is clicked: ");
        }

        if (characterCreationManager.eyebrow)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headBottom\" Button is clicked: ");
        }

        if (characterCreationManager.mouth)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headBottom\" Button is clicked: ");
        }

        if (characterCreationManager.nose)
        {
            characterCreationManager.ChangeImage(prefabSprite);

            print("New - A \"headBottom\" Button is clicked: ");
        }
    }
}
