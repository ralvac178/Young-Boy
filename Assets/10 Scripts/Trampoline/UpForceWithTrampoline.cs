using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpForceWithTrampoline : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void JumpTrampoline(float force)
    {
        rb2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}
