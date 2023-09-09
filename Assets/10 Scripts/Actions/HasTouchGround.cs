using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasTouchGround : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask layerMask;
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, layerMask);
        if (hit.collider != null)
        {
            PlayerController.isOnGround = true;
            animator.SetBool("IsOnGround", true);
        }
        else
        {
            PlayerController.isOnGround = false;
            animator.SetBool("IsOnGround", false);
        }
    }
}
