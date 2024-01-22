using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayerDust()
    {
        if (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1))
        {
            if (PlayerController.isOnGround)
            {
                animator.SetTrigger("isWalking");
            }
        }
        else
        {
            if (!PlayerController.isOnGround)
            {
                animator.SetTrigger("IsOnAir");
            }
        }      
    }

    public void EnemyDust()
    {
        if (animator != null)
        {
            animator.Play("dust_walk");
        }
    }
}
