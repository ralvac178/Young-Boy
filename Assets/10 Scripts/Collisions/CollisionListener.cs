using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    private HasTouchGround hasTouchGround;
    private void Start()
    {
        hasTouchGround = GetComponent<HasTouchGround>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Traps"))
        {
            CollisionProvider.OnTrapCollision();
        }
        else if (collision.gameObject.tag.Equals("Coin"))
        {
            CollisionProvider.OnCoinCollision(1);
            SoundManager.instance.SoundCoinCollected();
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("CollectableArrows"))
        {
            CollisionProvider.OnArrowsCollision();
            SoundManager.instance.SoundPlayerGotArrowsNLives();
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("Key"))
        {
            string color = collision.gameObject.name;
            switch (color)
            {
                case "BlueKey":
                    CollisionProvider.OnKeyCollision("blue"); ;
                    break;
                case "YellowKey":
                    CollisionProvider.OnKeyCollision("yellow"); ;
                    break;
                default:
                    CollisionProvider.OnKeyCollision("red"); ;
                    break;
            }

            SoundManager.instance.SoundPlayerGotKey();
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("DJGem"))
        {
            CollisionProvider.OnDoubleJumpPowerUpCollision();
            SoundManager.instance.SoundPlayerGotArrowsNLives();
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("Heart"))
        {
            SoundManager.instance.SoundPlayerGotArrowsNLives();
            CollisionProvider.OnHeartCollision(3);
            collision.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Pusher")) // Change for disabling lighting trap
        {
            collision.gameObject.transform.GetComponent<Animator>().SetTrigger("Pressed");
            collision.gameObject.layer = LayerMask.NameToLayer("Floor");
            collision.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (PlayerController.isPunching)
            {
                EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
                DragonController dc = collision.gameObject.GetComponent<DragonController>();

                if(ec != null)
                {
                    if (collision.gameObject.GetComponent<EnemyController>().isAlive
                    && !collision.gameObject.GetComponent<HasDamage>().isDamage)
                    {
                        collision.gameObject.transform.GetComponent<HasDamage>().OnHasDamage();
                    }
                }
                else if (dc != null)
                {
                    if (true // Repeat the shame thing of enemycontroller
                    && !collision.gameObject.GetComponent<HasDamage>().isDamage)
                    {
                        collision.gameObject.transform.GetComponent<HasDamage>().OnHasDamage();
                    }
                }                            
            }
        }
        else if (collision.gameObject.tag.Equals("Traps"))
        {
            CollisionProvider.OnLavaCollision();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            hasTouchGround.isOnGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasTouchGround.isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasTouchGround.isOnGround = false;
        }
    }
}
