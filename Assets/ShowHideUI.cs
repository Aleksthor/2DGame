using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{
    public KeyCode toggleKey;
    private bool questLogOpen = false;
    public GameObject UIContainer;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Toggle();

        }
    }

    public void Toggle()
    {
        if (questLogOpen)
        {
            UIContainer.SetActive(false);
            questLogOpen = false;
        }
        else
        {
            UIContainer.SetActive(true);
            questLogOpen = true;
        }
    }
}
