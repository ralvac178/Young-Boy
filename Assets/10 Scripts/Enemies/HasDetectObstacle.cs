using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDetectObstacle : MonoBehaviour
{
    public bool playerOnRight, wallOnRight;
    public bool playerOnLeft, wallOnLeft;

    private CapsuleCollider2D capsuleCollider2D;
    private Vector2 normalSize, origin;
    [SerializeField] private LayerMask layerMask;

    private EnemyController enemyController;
    private DragonController dragonController;

    private void Start()
    {
        capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
        dragonController = GetComponent<DragonController>();
        enemyController = GetComponent<EnemyController>();

        normalSize = capsuleCollider2D.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController != null)
        {
            if (transform.name.Contains("Shaman"))
            {
                DetectPlayer(enemyController.direction, 0.22f);
            }
            else if (transform.name.Contains("Barzag"))
            {
                DetectPlayer(enemyController.direction, 0.34f);
            }
        }
        else if (dragonController != null)
        {
            if (transform.name.Contains("Dragon"))
            {
                DetectPlayer(dragonController.direction, 0.75f);
            }
        }
    }

    private void DetectPlayer(Vector2 direction, float distance)
    {

        int layerLeft = 0;
        int layerRight = 0;

        //Player && Wall
        RaycastHit2D hitRight = Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, normalSize, CapsuleDirection2D.Horizontal, 0, transform.right, distance, layerMask);
        RaycastHit2D hitLeft = Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, normalSize, CapsuleDirection2D.Horizontal, 0, -transform.right, distance, layerMask);

        if (hitLeft.collider != null)
        {
            layerLeft = hitLeft.collider.gameObject.layer;
        }
        if (hitRight.collider != null)
        {
            layerRight = hitRight.collider.gameObject.layer;
        }
        
        if (layerLeft == 0 && layerRight == 6 && direction == Vector2.right)
        {
            playerOnRight = true;
            playerOnLeft = false;
            wallOnLeft = false;
            wallOnRight = false;
        }
        else if (layerLeft == 6 && layerRight == 0 && direction == Vector2.left)
        {
            playerOnRight = false;
            playerOnLeft = true;
            wallOnRight = false;
            wallOnLeft = false;
        }
        else if (layerLeft == 0 && layerRight == 3 && direction == Vector2.right)
        {
            wallOnRight = true;
            wallOnLeft = false;
            playerOnRight = false;
            playerOnLeft = false;
        }
        else if (layerLeft == 3 && layerRight == 0 && direction == Vector2.left)
        {
            wallOnRight = false;
            wallOnLeft = true;
            playerOnRight = false;
            playerOnLeft = false;
        }
        else if (layerLeft == 6 && layerRight == 3 && direction == Vector2.left)
        {
            playerOnLeft = true;
            wallOnLeft = false;
            playerOnRight = false;
            wallOnRight = false;
        }
        else if (layerLeft == 3 && layerRight == 6 && direction == Vector2.right)
        {
            playerOnLeft = false;
            wallOnLeft = false;
            playerOnRight = true;
            wallOnRight = false;
        }
        else if (layerLeft == 0 && layerRight == 0)
        {
            playerOnLeft = false;
            wallOnLeft = false;
            playerOnRight = false;
            wallOnRight = false;
        }
    }
}
