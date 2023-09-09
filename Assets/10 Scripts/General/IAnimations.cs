using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimations
{
    public void JumpAnimation(Animator animator, string jumpState);
    public void IdleAnimation(Animator animator, string idleState);
}
