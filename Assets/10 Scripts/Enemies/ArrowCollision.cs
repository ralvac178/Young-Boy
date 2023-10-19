using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    private HasDamage hasDamage;
    private void Start()
    {
        hasDamage = GetComponent<HasDamage>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            hasDamage.OnHasDamage();
            Destroy(collision.gameObject);
        }
    }
}
