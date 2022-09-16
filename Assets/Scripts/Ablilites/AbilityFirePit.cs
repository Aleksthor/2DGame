using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityFirePit : Ability
{

    
    private Camera mainCam;
        
    public float range;
    public GameObject firePitObject;



    public override void Activate(GameObject parent)
    {

        parent.GetComponent<Animator>().SetBool("InfernoCast", true);


    }


    public override void DeActivate(GameObject parent)
    {

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (Vector2.Distance(parent.transform.position, mainCam.ScreenToWorldPoint(Input.mousePosition)) < range)
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            Instantiate(firePitObject, new Vector2(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y), parent.transform.rotation);
        }
        else
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            Vector2 direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;

            Instantiate(firePitObject, parent.transform.position + (Vector3)direction.normalized * (range - 2f), parent.transform.rotation);
        }
        parent.GetComponent<Animator>().SetBool("InfernoCast", false);
    }
}
