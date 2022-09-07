using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollider : MonoBehaviour
{

    private Player player;
    private Movement movement;

    [Header("Attack Variables")]
    public float damage = 10f;

    private float delay = 0.2f;
    private float delayClock = 0f;
    private bool doDelay = false;


    private void Awake()
    {
        movement = FindObjectOfType<Movement>();
        player = FindObjectOfType<Player>();
    }
    

    void Update()
    {




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

            player.Hit(damage);

            
            doDelay = true;
            if(gameObject.tag == "Arrow")
            {
                Destroy(gameObject);
            }

        }
    }
}
