using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private HasMove hasMove;
    [SerializeField] private HasJump hasjump;
    [SerializeField] private PlayerAnimations playerAnimations;

    [SerializeField] private HasDamage hasDamage;
    [SerializeField] private SpriteRenderer lookAt;

    public static bool isOnGround;
    // Start is called before the first frame update
    void Awake()
    {
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        inputManager.Enable();

        inputManager.Player.Jump.performed += _ =>
        {
            hasjump.OnHasJump();
            playerAnimations.JumpAnimation();
        };

        CollisionProvider.trapCollision += hasDamage.OnHasDamage;
        CollisionProvider.trapCollision += playerAnimations.HurtAnimation;

        //inputManager.Player.HorMove.performed += _ => playerAnimations.JumpAnimation();
    }

    private void OnDisable()
    {
        inputManager.Player.Jump.performed -= _ =>
        {
            hasjump.OnHasJump();
            playerAnimations.JumpAnimation();
        };

        CollisionProvider.trapCollision += hasDamage.OnHasDamage;
        CollisionProvider.trapCollision += playerAnimations.HurtAnimation;

        //inputManager.Player.HorMove.performed += _ => playerAnimations.JumpAnimation();

        inputManager.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float forceMovement = inputManager.Player.HorMove.ReadValue<float>();
        if (forceMovement != 0)
        {
            hasMove.OnHasMove(forceMovement, isOnGround);
            playerAnimations.WalkAnimation(true);
            if (forceMovement > 0)
            {
                lookAt.flipX = false;
            }
            else
            {
                lookAt.flipX = true;;
            }
        }
        else
        {
            playerAnimations.WalkAnimation(false);
        }
        
    }
}
