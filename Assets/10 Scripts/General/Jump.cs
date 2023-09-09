using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public void OnJump(Rigidbody2D rb2D, float forceJump)
    {
        rb2D.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
    }
}
