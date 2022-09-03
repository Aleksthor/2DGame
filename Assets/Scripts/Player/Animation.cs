using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Player player;
    Animator animator;


    private void Awake()
    {
        player = FindObjectOfType<Player>();

    }

    private void Start()
    {
        animator = player.GetPlayer().GetComponent<Animator>();
    }

    public void SetWalkingAnimation(bool value)
    {
        animator.SetBool("Moving", value);
    }

    public void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void SetBlockingAnimation(bool value)
    {
        animator.SetBool("Blocking", value);
    }

    public void SetDashingAnimation(bool value)
    {
        animator.SetBool("Dashing", value);
    }
}
