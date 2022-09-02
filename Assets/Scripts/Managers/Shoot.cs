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



        shootCost();
    }


    //--------------------



    



    void shootCost()
    {
        if (isFire)
        {
            hud.SetMagicValue(-magicCost);
            isFire = false;
        }
    }
}
