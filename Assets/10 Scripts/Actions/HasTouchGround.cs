using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasTouchGround : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask layerMaskFloor, layerMaskCeil;

    private CapsuleCollider2D capsuleCollider;
    public bool isOnGround, isOnCeil;

    private void Start()
    {
        capsuleCollider = transform.GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitFloor = LookHit(layerMaskFloor);
        RaycastHit2D hitCeil = LookHit(layerMaskCeil);

        if (hitFloor.collider != null)
        {
            isOnGround = true;
            animator.SetBool("IsOnGround", true);
        }
        else if (hitCeil.collider != null)
        {
            isOnCeil = true;
            animator.SetBool("IsOnGround", true);
        }
        else
        {
            isOnGround = false;
            isOnCeil = false;
            animator.SetBool("IsOnGround", false);
        }
    }

    RaycastHit2D LookHit(LayerMask layerMask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(capsuleCollider.bounds.center,
            capsuleCollider.bounds.size, 0, Vector2.down, 0.02f, layerMask);
        return hit;
    }
}
