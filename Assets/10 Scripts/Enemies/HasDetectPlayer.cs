using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDetectPlayer : MonoBehaviour
{
    public bool playerOnRight;
    public bool playerOnLeft;

    private CapsuleCollider2D capsuleCollider2D;
    private Vector2 normalSize;
    [SerializeField] private LayerMask layerMask;

    private EnemyController enemyController;
    private void Start()
    {
        capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
        enemyController = GetComponent<EnemyController>();
        normalSize = capsuleCollider2D.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        //Player
        RaycastHit2D hitRight = Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, normalSize, CapsuleDirection2D.Horizontal, 0, Vector2.right, 0.045f, layerMask);
        RaycastHit2D hitLeft = Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, normalSize, CapsuleDirection2D.Horizontal, 0, Vector2.left, 0.1f, layerMask);
        bool isOnleftSide = hitLeft.collider != null;
        bool isOnRightSide = hitRight.collider != null;

        if (!isOnleftSide && isOnRightSide && enemyController.direction == Vector2.right)
        {
            playerOnRight = true;
            playerOnLeft = false;
        }
        else if (isOnleftSide && !isOnRightSide && enemyController.direction == Vector2.left)
        {
            playerOnRight = false;
            playerOnLeft = true;
        }
        else if (!isOnleftSide && !isOnRightSide)
        {
            playerOnLeft = false;
            playerOnRight = false;
        }
    }
}
