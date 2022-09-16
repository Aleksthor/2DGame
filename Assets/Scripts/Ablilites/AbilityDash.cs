using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityDash : Ability
{
    public float dashVelocity;

    private Camera mainCam;
    private Rigidbody2D rigidbody;

    private LayerMask layerMask;
    private int layer = 3;

    public override void Activate(GameObject parent)
    {
        layerMask = (1 << layer);

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector2 direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;
        Vector2 newPosition = (Vector2)parent.transform.position + direction;

        Collider2D collider = Physics2D.OverlapCircle(newPosition, 0.5f, layerMask);

        Debug.Log(collider);

        parent.transform.position = newPosition;

    }


    public override void DeActivate(GameObject parent)
    {

    }
}
