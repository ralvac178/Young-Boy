using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour, IAnimations
{
    public void JumpAnimation(Animator animator, string jumpState)
    {
        animator.SetTrigger(jumpState);
    }

    public void IdleAnimation(Animator animator, string idleState)
    {
        animator.SetBool(idleState, true);
    }
}
