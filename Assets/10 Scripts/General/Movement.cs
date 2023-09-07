using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public void HorMove(Rigidbody2D rb, float force, float dir)
    {
        rb.AddForce(Vector2.right * force * dir);
    }
}
