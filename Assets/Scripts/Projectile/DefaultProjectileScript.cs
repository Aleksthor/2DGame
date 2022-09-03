using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectileScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" || collider.tag == "Enemy")
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
