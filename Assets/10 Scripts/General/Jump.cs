using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public void OnJump(Rigidbody2D rb2D, float forceJump)
    {
        if (PlayerController.isOnGround || PlayerController.isOnCeil)
        {
            if (rb2D.velocity.y < 0.5f)
            {
                rb2D.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
            }
        }
        else if (PlayerController.canDoubleJump)
        {
            rb2D.AddForce(Vector2.up * forceJump * 0.75f, ForceMode2D.Impulse);
            PlayerController.canDoubleJump = false;
        }
    }
}
