using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Player player;
    

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void SetWalkingAnimation(bool value)
    {
        player.GetPlayerAnimator().SetBool("Moving", value);
    }
}
