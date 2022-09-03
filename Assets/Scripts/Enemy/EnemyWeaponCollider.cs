using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollider : MonoBehaviour
{
    [Header("Reference to Managers")]
    public HUD playerHUD;
    public Movement movement;
    [Header("Attack Variables")]
    public float damage = 10f;
    [Header("Private Variabels")]
    [SerializeField]
    private float delay = 0.2f;
    [SerializeField]
    private float delayClock = 0f;
    [SerializeField]
    private bool doDelay = false;


    private void Awake()
    {
        movement = FindObjectOfType<Movement>();
    }
    

    void Update()
    {

        // A way for the arrow to get manager since it spawns without this variable
        if (playerHUD == null)
        {
            GameObject manager = GameObject.Find("Manager");
            GameObject HUDManager = manager.transform.Find("HUDManager").gameObject;
            playerHUD = HUDManager.GetComponent<HUD>();
        }


        // Only damage the player once
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
            if(movement.isShielding)
            {
                playerHUD.SetHealthValue(-damage * 0.5f);
            }
            else
            {
                playerHUD.SetHealthValue(-damage);
            }
            
            doDelay = true;
            if(gameObject.tag == "Arrow")
            {
                Destroy(gameObject);
            }

        }
    }
}
