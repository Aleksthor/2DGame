using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : SingletonMonoBehaviour<AnimationManager>
{
    PlayerManager player;
    Animator animator;


 

    private void Start()
    {
        player = PlayerManager.Instance;
        animator = PlayerSingleton.instance.gameObject.GetComponent<Animator>();
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

    public void SetWalkingSpeedAnimation(Vector2 InputVector)
    {
        animator.SetFloat("MovementSpeed", InputVector.magnitude);
    }
}
