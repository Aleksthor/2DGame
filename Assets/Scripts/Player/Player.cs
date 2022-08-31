using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Moveable Object")]
    [SerializeField] GameObject player;
    Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = player.GetComponent<Animator>();
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public Animator GetPlayerAnimator()
    {
        return playerAnimator;
    }
}

