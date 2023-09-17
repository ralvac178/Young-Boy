using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasTouchGround : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask layerMask;

    private BoxCollider2D boxCollider;
    public bool isOnGround;

    private void Start()
    {
        boxCollider = transform.GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, 
            boxCollider.bounds.size, 0, Vector2.down, 0.02f,layerMask);

        if (hit.collider != null)
        {
            isOnGround = true;
            animator.SetBool("IsOnGround", true);
        }
        else
        {
            isOnGround = false;
            animator.SetBool("IsOnGround", false);
        }
    }
}
