using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float force;
    private Animator animator;
    private Rigidbody2D rb2D;
    private CapsuleCollider2D capCollider;
    private float randomForce;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Force", rb2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps") &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("Down"))
        {
            animator.SetBool("Grounded", true);           
        }
    }

    public void Restart()
    {
        randomForce = Random.Range(force - 0.15f, force + 1f);
        rb2D.AddForce(Vector2.up * randomForce, ForceMode2D.Impulse);
        animator.SetBool("Grounded", false);
        capCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnableCollider") && animator.GetCurrentAnimatorStateInfo(0).IsName("Down"))
        {
            capCollider.isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnableCollider") && animator.GetCurrentAnimatorStateInfo(0).IsName("Down"))
        {
            capCollider.isTrigger = false;
        }
    }
}
