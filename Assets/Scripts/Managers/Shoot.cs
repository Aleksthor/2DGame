using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    HUD hud;
    Animator playerAnimator;

    [Header("Moveable Object")]
    [SerializeField] GameObject player;

    bool fire;
    bool isFire;
    [SerializeField] int magicCost = 2;


    //--------------------

    private void Awake()
    {
        hud = FindObjectOfType<HUD>();
        playerAnimator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        ButtonInput();

        ShieldBlock();

        shootActivate();
        shootCost();
    }


    //--------------------


    void ButtonInput()
    {
        //fire = Input.GetButtonDown("Fire");
        //fire = Input.GetButtonUp("Fire");
    }
    
    void shootActivate()
    {
        //When Fire is pressed down
        if (Input.GetButtonDown("Fire1"))
        {
            isFire = true;

            playerAnimator.SetTrigger("Attack");


            print("isFire = true");
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isFire = false;
            print("isFire = false");
        }
    }

    void ShieldBlock()
    {
        playerAnimator.SetBool("Blocking", false);
        if (!isFire)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                playerAnimator.SetBool("Blocking", true);
            }
        }
       
    }
    void shootCost()
    {
        if (isFire)
        {
            hud.SetMagicValue(-magicCost);
            isFire = false;
        }
    }
}
