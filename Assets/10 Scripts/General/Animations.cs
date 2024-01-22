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

    public void WalkAnimation(Animator animator, string walkState, bool state)
    {
        animator.SetBool(walkState, state);
    }

    public void HurtAnimation(Animator animator, string hurtState)
    {
        animator.SetTrigger(hurtState);
    }

    public void ShootAnimation(Animator animator, string shootState)
    {
        animator.SetTrigger(shootState);
    }

    public void PunchAnimation(Animator animator, string punchState)
    {
        animator.SetTrigger(punchState);
    }

    public void BiteAnimation(Animator animator, string bitState)
    {
        animator.SetTrigger(bitState);
    }

    public void DeadAnimation(Animator animator, string deadState)
    {
        animator.SetTrigger(deadState);
    }

    public void ResetAnimation(Animator animator, string idleState)
    {
        animator.Play(idleState);
    }

    public void RunAnimationTrue(Animator animator, string runState)
    {
        animator.SetBool(runState, true);
    }

    public void RunAnimationFalse(Animator animator, string runState)
    {
        animator.SetBool(runState, false);
    }
}
