using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Traps"))
        {
            CollisionProvider.OnTrapCollision();
        }
        else if (collision.gameObject.tag.Equals("Coin"))
        {
            CollisionProvider.OnCoinCollision(1);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("CollectableArrows"))
        {
            CollisionProvider.OnArrowsCollision();
            collision.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Trampoline"))
        {
            collision.gameObject.transform.GetComponent<Animator>().SetTrigger("Up");
        }
        else if (collision.gameObject.tag.Equals("Pusher"))
        {
            collision.gameObject.transform.GetComponent<Animator>().SetTrigger("Pressed");
            collision.gameObject.layer = LayerMask.NameToLayer("Floor");
            collision.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (PlayerController.isPunching)
            {
                collision.gameObject.transform.GetComponent<HasDamage>().OnHasDamage();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Trampoline"))
        {
            float force = collision.gameObject.transform.GetComponent<Trampoline>().force;
            if (Trampoline.isAtTop)
            {
                CollisionProvider.OnTrampolineCollision(force);
            }            
        }
    }
}
