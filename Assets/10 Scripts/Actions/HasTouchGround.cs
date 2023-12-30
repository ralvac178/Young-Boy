using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasTouchGround : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask layerMaskFloor, layerMaskCeil;

    private CapsuleCollider2D capsuleCollider;
    public bool isOnGround, isOnCeil;
    private PlayerController playerController;
    public static bool enableReturn = false;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        capsuleCollider = transform.GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (enableReturn)
        {
            return;
        }

        RaycastHit2D hitFloor = LookHit(layerMaskFloor);
        RaycastHit2D hitCeil = LookHit(layerMaskCeil);

        if (hitFloor.collider != null)
        {
            if (hitFloor.collider.IsTouchingLayers())
            {
                isOnGround = true;
                animator.SetBool("IsOnGround", true);
                Invoke(nameof(CallFinishGame), 0.5f); // Ask if finish the game
            }           
        }
        
        else if (hitCeil.collider != null)
        {
            if (hitCeil.collider.IsTouchingLayers())
            {
                isOnGround = true;
                animator.SetBool("IsOnGround", true);
                Invoke(nameof(CallFinishGame), 0.5f); // Ask if finish the game
            }    
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
        // Boxcast let double jump on ground
        RaycastHit2D hit = Physics2D.BoxCast(capsuleCollider.bounds.center,
            capsuleCollider.bounds.size, 0, Vector2.down, 0.02f, layerMask);

        return hit;
    }

    public void CallFinishGame()
    {
        GameManager.instance.FinishGame();
    }
}
