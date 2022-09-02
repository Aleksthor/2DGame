using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollider : MonoBehaviour
{

    public HUD playerHUD;
    public float damage = 10f;
    private float delay = 1f;
    private float delayClock = 0f;
    private bool doDelay = false;
    

    void Update()
    {
        if (playerHUD == null)
        {
            GameObject manager = GameObject.Find("Manager");
            GameObject HUDManager = manager.transform.Find("HUDManager").gameObject;
            playerHUD = HUDManager.GetComponent<HUD>();
        }



        if (doDelay)
        {
            delayClock += Time.deltaTime;
            if(delayClock > delay)
            {
                delayClock = 0f;
                doDelay = false;
            }
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Player" && !doDelay)
        {

            playerHUD.SetHealthValue(-damage);
            doDelay = true;

        }
    }
}
