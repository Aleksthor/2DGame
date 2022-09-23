using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityDashBackwards : Ability
{

    [Header("This Ability")]
    public float dashLength;
    public GameObject effect;

    private Camera mainCam;

    public LayerMask layerMask;


    public override void Activate(GameObject parent)
    {

        GameObject spawnedObject = Instantiate(effect, parent.transform.position, parent.transform.rotation);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector2 direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position;
        spawnedObject.transform.right = direction * -1f;
        Vector2 newPosition = (Vector2)parent.transform.position - direction.normalized * dashLength;


        RaycastHit2D hit = Physics2D.Raycast(parent.transform.position, direction.normalized * -1f, dashLength, layerMask);

        if (hit)
        {
            Debug.Log(direction.y);
            if (direction.y < 0f)
            {
                parent.transform.position = new Vector2(hit.point.x, hit.point.y + 0.5f);
            }
            else
            {
                parent.transform.position = hit.point;
            }

        }
        else
        {
            parent.transform.position = newPosition;
        }



        PlayerManager.Instance.SetStaminaValue(PlayerManager.Instance.maxStamina);

    }


    public override void DeActivate(GameObject parent)
    {

    }


    public override void Trigger(GameObject parent)
    {
    }
}
