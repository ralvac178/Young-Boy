using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasMove : MonoBehaviour
{
    [SerializeField] private Movement movementScript;
    private Rigidbody2D rb2D;
    [SerializeField] private float speed;
    private void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    public void OnHasMove(float dir)
    {
        movementScript.HorMove(rb2D, speed, dir);
    }
}
