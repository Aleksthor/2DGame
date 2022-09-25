using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchComponent : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {

        if (DayNightCycle.Instance.IsDay())
        {
            animator.SetBool("Day", true);

        }
        else
        {
            animator.SetBool("Day", false);

        }
    }
}
