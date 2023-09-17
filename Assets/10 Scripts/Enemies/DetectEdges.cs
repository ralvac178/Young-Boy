using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEdges : MonoBehaviour
{
    public bool isOnRightEdge; 
    public bool isOnLeftEdge;

    private BoxCollider2D boxCollider;
    private Vector2 centerRight, size, normalSize, centerLeft;
    [SerializeField] private LayerMask layerMask;

    private float xDistance, yDistance;
    private void Start()
    {
        boxCollider = transform.GetComponent<BoxCollider2D>();

        normalSize = boxCollider.bounds.size;
        size = normalSize / 3;
        //Get distance to use in boxcast
        //On x
        xDistance = size.x + size.x / 2 + 0.5f;

        //On y
        yDistance = size.y + size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        centerRight = boxCollider.bounds.center + new Vector3(xDistance, -yDistance, 0);

        centerLeft = boxCollider.bounds.center - new Vector3(xDistance, yDistance, 0);

        //Edges
        RaycastHit2D hitRight = Physics2D.BoxCast(centerRight, size, 0, Vector2.down, 0.02f, layerMask);
        RaycastHit2D hitLeft= Physics2D.BoxCast(centerLeft, size, 0, Vector2.down, 0.02f, layerMask);

        //Walls
        RaycastHit2D hitRightWall = Physics2D.BoxCast(boxCollider.bounds.center, normalSize, 0, Vector2.right, 0.5f, layerMask);
        RaycastHit2D hitLeftWall = Physics2D.BoxCast(boxCollider.bounds.center, normalSize, 0, Vector2.left, 0.5f, layerMask);

        bool isOnleftSide = hitLeft.collider == null || hitLeftWall.collider != null;
        bool isOnRightSide = hitRight.collider == null || hitRightWall.collider != null;

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
