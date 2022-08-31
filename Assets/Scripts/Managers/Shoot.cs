using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Player player;
    HUD hud;

    bool fire;
    bool isFire;
    [SerializeField] int magicCost = 2;


    //--------------------

    private void Awake()
    {
        hud = FindObjectOfType<HUD>();
        player = FindObjectOfType<Player>();
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

            player.GetPlayerAnimator().SetTrigger("Attack");


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
        //player.GetPlayerAnimator().SetBool("Blocking", false);
        if (!isFire)
        {
            if (Input.GetButtonDown("Fire2"))
            {
               // player.GetPlayerAnimator().SetBool("Blocking", true);
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
