using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityDash : Ability
{
    public float dashVelocity;

    private Camera mainCam;
    private Rigidbody2D rigidbody;

  

    public override void Activate(GameObject parent)
    {
        
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector2 direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;
        

        Vector2 newPosition = (Vector2)parent.transform.position + direction;

        parent.transform.position = newPosition;

    }


    public override void DeActivate(GameObject parent)
    {

    }
}
