using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Moveable Object")]
    [SerializeField] GameObject playerObject;
    Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = playerObject.GetComponent<Animator>();
    }

    public GameObject GetPlayer()
    {
        return playerObject;
    }

    public Animator GetPlayerAnimator()
    {
        return playerAnimator;
    }
}

