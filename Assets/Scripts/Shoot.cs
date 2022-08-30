using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    HUD hud;

    bool fire;
    bool isFire;
    [SerializeField] int magicCost = 2;


    //--------------------

    private void Awake()
    {
        hud = FindObjectOfType<HUD>();
    }
    private void Update()
    {
        ButtonInput();

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

            print("isFire = true");
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isFire = false;
            print("isFire = false");
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
