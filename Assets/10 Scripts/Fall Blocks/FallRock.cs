using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = transform.parent.gameObject.GetComponent<BoxCollider2D>();
    }

    public void DisableCollider()
    {
        if (boxCollider2D.enabled)
        {
            boxCollider2D.enabled = false;
        }       
    }

    public void HideBlock()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        if (transform.gameObject.name.Contains("Falling"))
        {
            Invoke(nameof(EnableRock), 10f);
        }
    }

    public void EnableRock()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        boxCollider2D.enabled = true;
        GetComponent<Animator>().SetTrigger("Restore");
    }
}
