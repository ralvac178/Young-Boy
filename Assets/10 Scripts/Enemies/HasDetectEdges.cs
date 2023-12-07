using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDetectEdges : MonoBehaviour
{
    public bool isOnRightEdge; 
    public bool isOnLeftEdge;

    private CapsuleCollider2D capsuleCollider2D;
    private Vector2 centerRight, size, normalSize, centerLeft;
    [SerializeField] private LayerMask layerMask;

    private float xDistance, yDistance;
    private void Start()
    {
        capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();

        normalSize = capsuleCollider2D.bounds.size;
        size = normalSize / 3;
        //Get distance to use in boxcast
        //On x
        xDistance = size.x + size.x / 2 + 0.35f;

        //On y
        yDistance = size.y + size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        centerRight = capsuleCollider2D.bounds.center + new Vector3(xDistance, 0, 0);

        centerLeft = capsuleCollider2D.bounds.center - new Vector3(xDistance, 0, 0);

        //Edges
        RaycastHit2D hitRight = Physics2D.BoxCast(centerRight, size, 0, Vector2.down, 0.25f, layerMask);
        RaycastHit2D hitLeft= Physics2D.BoxCast(centerLeft, size, 0, Vector2.down, 0.25f, layerMask);

        
        bool isOnleftSide = hitLeft.collider == null;
        bool isOnRightSide = hitRight.collider == null;

        if (!isOnleftSide && isOnRightSide)
        {
            isOnRightEdge = true;
            isOnLeftEdge = false;
        }
        else if (isOnleftSide && !isOnRightSide)
        {
            isOnLeftEdge = true;
            isOnRightEdge = false;
        }
        else if (!isOnleftSide && !isOnRightSide)
        {
            isOnLeftEdge = false;
            isOnRightEdge = false;
        }
    }
}
