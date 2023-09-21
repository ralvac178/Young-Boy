using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerArrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool shootOrder = true;
    [SerializeField] private float force;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (shootOrder)
        {
            if (PlayerController.playerLookAt)
            {
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
            }

            rb.AddForce(direction * force);
            shootOrder = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(this.gameObject);
        }
    }
}
