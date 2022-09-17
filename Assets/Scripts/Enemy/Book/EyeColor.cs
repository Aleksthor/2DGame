using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeColor : MonoBehaviour
{

    public SpriteRenderer eye;
    private bool agro = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.OnSetAgro += SetAgro;
        GameEvents.current.OnRemoveAgro += RemoveAgro;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if(agro)
        {
            eye.color = new Color32(166, 35, 17, 255);
        }
        else
        {
            eye.color = Color.white;           
        }
    }

    private void SetAgro(GameObject GameObject)
    {
        if (gameObject == GameObject)
        {
            agro = true;
        }
    }


    private void RemoveAgro(GameObject GameObject)
    {
        if (gameObject == GameObject)
        {
            agro = false;
        }
    }
    private void OnDestroy()
    {
        GameEvents.current.OnSetAgro -= SetAgro;
        GameEvents.current.OnRemoveAgro -= RemoveAgro;
    }
}
