using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Traps"))
        {
            Collider2D collider = collision.gameObject.transform.GetComponent<Collider2D>();
            CollisionProvider.OnTrapCollision(collider);
        }
        else if (collision.gameObject.tag.Equals("Trampoline"))
        {
            collision.gameObject.transform.GetComponent<Animator>().SetTrigger("Up");
        }
        else if (collision.gameObject.tag.Equals("Pusher"))
        {
            collision.gameObject.transform.GetComponent<Animator>().SetTrigger("Pressed");
            collision.gameObject.layer = LayerMask.NameToLayer("Floor");
            collision.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
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
