using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public void HorMove(Rigidbody2D rb, float force, float dir, bool isOnGround)
    {
        if (isOnGround)
        {
            rb.AddForce(Vector2.right * force * dir);
        }
        else
        {
            rb.AddForce(Vector2.right * force/3.6f * dir);
        }
    }
}
