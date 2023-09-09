using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animations animationsScript;

    [SerializeField] private Animator playerAnimator;
    public void JumpAnimation()
    {
        animationsScript.JumpAnimation(playerAnimator, "Jump");
    }

    public void IdleAnimation()
    {
        animationsScript.IdleAnimation(playerAnimator, "IsOnGround");
    }
}
