using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityWaterSlam : Ability
{
    [Header("This Ability")]

    private Camera mainCam;
    public float range;
    public GameObject waterWaveObject;
    private PlayerManager playerManager;


    public override void Activate(GameObject parent)
    {
        if (playerManager == null)
        {
            playerManager = PlayerManager.Instance;
        }
        if (playerManager.GetManaValue() - manaCost > 0)
        {
            parent.GetComponent<Animator>().SetTrigger("WaterCast");
        }
        else
        {

        }

    }



    public override void DeActivate(GameObject parent)
    {


    }



    public override void Trigger(GameObject parent)
    {

        if (playerManager.GetManaValue() - manaCost > 0)
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            if (Vector2.Distance(parent.transform.position, mainCam.ScreenToWorldPoint(Input.mousePosition)) < range)
            {
                mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

                Object.Instantiate(waterWaveObject, new Vector2(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y), parent.transform.rotation);
            }
            else
            {
                mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

                Vector2 direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;

                Object.Instantiate(waterWaveObject, parent.transform.position + (Vector3)direction.normalized * (range - 2f), parent.transform.rotation);
            }

            GameEvents.current.UseMana(manaCost);
        }


    }
}
