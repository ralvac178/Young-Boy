using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animations animationsScript;

    [SerializeField] private Animator animator;
    public void JumpAnimation()
    {
        animationsScript.JumpAnimation(animator, "Jump");
    }

    public void IdleAnimation()
    {
        animationsScript.IdleAnimation(animator, "IsOnGround");
    }

    public void WalkAnimation(bool state)
    {
        animationsScript.WalkAnimation(animator, "IsWalking", state);
    }

    public void HurtAnimation()
    {
        animationsScript.HurtAnimation(animator, "Hurt");
    }

    public void ShootAnimation()
    {
        animationsScript.ShootAnimation(animator, "Shoot");
    }

    public void PunchAnimation()
    {
        animationsScript.PunchAnimation(animator, "Punch");
    }

    public void BiteAnimation()
    {
        animationsScript.BiteAnimation(animator, "Bite");
    }

    public void DeadAnimation()
    {
        animationsScript.DeadAnimation(animator, "Dead");
    }

    public void ResetAnimator()
    {
        animationsScript.ResetAnimation(animator,"PlayerIdle");
    }

    public void RunAnimationEnabled()
    {
        animationsScript.RunAnimationTrue(animator, "IsRunning");
    }

    public void RunAnimationDisabled()
    {
        animationsScript.RunAnimationFalse(animator, "IsRunning");
    }
}
