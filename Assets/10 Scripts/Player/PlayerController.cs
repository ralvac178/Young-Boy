using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private HasMove hasMove;
    [SerializeField] private HasJump hasjump;
    [SerializeField] private CharacterAnimations playerAnimations;

    [SerializeField] private HasDamage hasDamage;
    [SerializeField] private HasGetCoin hasGetCoin;
    [SerializeField] private SpriteRenderer lookAt;
    [SerializeField] private UpForceWithTrampoline upForceWithTrampoline;

    private HasTouchGround hasTouchGroundScript;
    public static bool isOnGround;
    [SerializeField] private float speed;
    private float forceMovement;

    [HideInInspector] public static bool playerLookAt = true;

    [SerializeField] private Parallax[] parallax;
    private Rigidbody2D rb;

    public static bool isPunching;
    // Start is called before the first frame update
    void Awake()
    {
        inputManager = new InputManager();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        hasTouchGroundScript = transform.GetComponent<HasTouchGround>();
        rb = GetComponent<Rigidbody2D>();
        //hasGetCoin = GetComponent<HasGetCoin>();
    }

    private void OnEnable()
    {
        inputManager.Enable();

        inputManager.Player.Jump.performed += _ =>
        {
            hasjump.OnHasJump();
            playerAnimations.JumpAnimation();
        };

        inputManager.Player.Shoot.performed += _ =>
        {
            if (GameManager.instance.arrows > 0)
            {
                playerAnimations.ShootAnimation();
                GameManager.instance.SubArrows();
            }
            else
            {
                playerAnimations.PunchAnimation();
            }
        };

        CollisionProvider.trapCollision += hasDamage.OnHasDamage;
        CollisionProvider.coinCollision += hasGetCoin.AddPoints;
        CollisionProvider.trampolineCollision += upForceWithTrampoline.JumpTrampoline;
        CollisionProvider.arrowsCollision += GameManager.instance.AddArrows;
        CollisionProvider.lavaCollision += OnhasHurtJump;
        CollisionProvider.lavaCollision += hasDamage.OnHasDamage;
        SceneManager.sceneLoaded += GetComponent<InitPosPlayer>().NewPosPlayer;
        //inputManager.Player.HorMove.performed += _ => playerAnimations.JumpAnimation();
    }

    private void OnhasHurtJump()
    {
        if (rb.velocity.y > 0.5f) return;
        rb.AddForce(Vector2.up * 185, ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        inputManager.Player.Jump.performed -= _ =>
        {
            hasjump.OnHasJump();
            playerAnimations.JumpAnimation();
        };

        inputManager.Player.Shoot.performed -= _ =>
        {
            if (GameManager.instance.arrows > 0)
            {
                playerAnimations.ShootAnimation();
                GameManager.instance.SubArrows();
            }
            else
            {
                if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PlayerHurt")
                && !(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
                {
                    playerAnimations.PunchAnimation();
                }              
            }
        };
        CollisionProvider.trapCollision -= hasDamage.OnHasDamage;
        CollisionProvider.coinCollision -= hasGetCoin.AddPoints;
        CollisionProvider.trampolineCollision -= upForceWithTrampoline.JumpTrampoline;
        CollisionProvider.arrowsCollision -= GameManager.instance.AddArrows;
        CollisionProvider.lavaCollision -= OnhasHurtJump;
        CollisionProvider.lavaCollision -= hasDamage.OnHasDamage;
        SceneManager.sceneLoaded -= GetComponent<InitPosPlayer>().NewPosPlayer;
        //inputManager.Player.HorMove.performed += _ => playerAnimations.JumpAnimation();

        inputManager.Disable();
    }

    // Update is called once per frame

    private void Update()
    {
        isOnGround = hasTouchGroundScript.isOnGround;

        forceMovement = inputManager.Player.HorMove.ReadValue<float>();
    }
    void FixedUpdate()
    {
        //Check if is on ground   
        if (forceMovement != 0)
        {
            hasMove.OnHasMove(forceMovement, isOnGround, speed);
            playerAnimations.WalkAnimation(true);
            if (forceMovement > 0)
            {
                if (rb.velocity.magnitude > 0.5f)
                {
                    for (int i = 0; i < parallax.Length; i++)
                    {
                        if (parallax[i] != null)
                        {
                            parallax[i].MoveBackgroundRight();
                        }
                    }
                }              
                
                lookAt.flipX = false;
                playerLookAt = true;
            }
            else
            {
                if (rb.velocity.magnitude < -0.5f)
                {
                    for (int i = 0; i < parallax.Length; i++)
                    {
                        parallax[i].MoveBackgroundLeft();
                    }
                }
                
                
                lookAt.flipX = true;
                playerLookAt = false;
            }
        }
        else
        {
            playerAnimations.WalkAnimation(false);
        }
        
    }
}
