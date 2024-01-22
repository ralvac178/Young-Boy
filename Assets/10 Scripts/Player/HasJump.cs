using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasJump : MonoBehaviour
{
    [SerializeField] private Jump jumpScript;
    private Rigidbody2D rb2D;
    [SerializeField] private float jumpForce;
    private void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    public void OnHasJump()
    {
        jumpScript.OnJump(rb2D, jumpForce);
    }

    public void EnableDoubleJump()
    {
        PlayerController.canDoubleJump = true;
    }

    public void SetDoubleJumpIcon()
    {
        GameManager.instance.SetDoubleJumpGem();
    }
}
