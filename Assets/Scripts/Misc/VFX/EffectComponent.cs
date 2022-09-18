using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectComponent : MonoBehaviour
{
    PolygonCollider2D effectCollider;

    private void Start()
    {
        if (transform.GetComponent<PolygonCollider2D>() != null)
        {
            effectCollider = transform.GetComponent<PolygonCollider2D>();
            effectCollider.enabled = false;
        }

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


    public void ColldierOn()
    {
        effectCollider.enabled = true;
    }

    public void ColliderOff()
    {
        effectCollider.enabled = false;
    }
}
