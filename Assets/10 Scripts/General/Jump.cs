using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    public void OnJump(Rigidbody2D rb2D, float forceJump)
    {
        if (GameManager.instance.GetLives() > 0 && !GameManager.instance.stopPlayer)
        {
            if (PlayerController.isOnGround || PlayerController.isOnCeil)
            {
                if (rb2D.velocity.y < 0.5f)
                {
                    rb2D.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
                    SoundManager.instance.SoundPlayerJump();
                }
            }
            else if (PlayerController.canDoubleJump)
            {
                rb2D.AddForce(Vector2.up * forceJump * 0.75f, ForceMode2D.Impulse);
                SoundManager.instance.SoundPlayerDoubleJump();
                PlayerController.canDoubleJump = false;
                GameManager.instance.DisableDoubleJumpGem();
            }
        }        
    }
}
