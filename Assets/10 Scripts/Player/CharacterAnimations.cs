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
}
