using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFireballs : MonoBehaviour
{
    [SerializeField] private GameObject fireballsParent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fireballsParent.SetActive(true);
        }
    }
}
