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
    [SerializeField] private CharacterAnimations playerAnimations;

    [SerializeField] private HasDamage hasDamage;
    [SerializeField] private SpriteRenderer lookAt;
    [SerializeField] private UpForceWithTrampoline upForceWithTrampoline;

    private HasTouchGround hasTouchGroundScript;
    public static bool isOnGround;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Awake()
    {
        inputManager = new InputManager();
    }

    private void Start()
    {
        hasTouchGroundScript = transform.GetComponent<HasTouchGround>();
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

        CollisionProvider.trampolineCollision += upForceWithTrampoline.JumpTrampoline;

        //inputManager.Player.HorMove.performed += _ => playerAnimations.JumpAnimation();
    }

    private void OnDisable()
    {
        inputManager.Player.Jump.performed -= _ =>
        {
            hasjump.OnHasJump();
            playerAnimations.JumpAnimation();
        };

        CollisionProvider.trapCollision -= hasDamage.OnHasDamage;

        CollisionProvider.trampolineCollision -= upForceWithTrampoline.JumpTrampoline;

        //inputManager.Player.HorMove.performed += _ => playerAnimations.JumpAnimation();

        inputManager.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check if is on ground
        isOnGround = hasTouchGroundScript.isOnGround;

        float forceMovement = inputManager.Player.HorMove.ReadValue<float>();
        if (forceMovement != 0)
        {
            hasMove.OnHasMove(forceMovement, isOnGround, speed);
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
