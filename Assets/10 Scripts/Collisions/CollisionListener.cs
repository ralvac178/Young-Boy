using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Traps"))
        {
            CollisionProvider.OnTrapCollision();
        }
        else if (collision.gameObject.tag.Equals("Trampoline"))
        {
            collision.gameObject.transform.GetComponent<Animator>().SetTrigger("Up");
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
