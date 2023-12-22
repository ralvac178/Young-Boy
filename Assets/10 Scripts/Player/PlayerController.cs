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
    public static bool isOnGround, isOnCeil;
    [SerializeField] private float speed;
    private float forceMovement;

    [HideInInspector] public static bool playerLookAt = true;

    private Rigidbody2D rb;

    public static bool isPunching;
    public static bool canDoubleJump;

    private Dictionary<int, string> attackOptions = new Dictionary<int, string>()
    {
        {1,"Punch"},
        {2,"Arrow"}
    };

    private string attackType = "Punch";
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
        if (SceneManager.GetActiveScene().name.Equals("Loading"))
        {
            return;
        }

        inputManager.Enable();

        inputManager.Player.Jump.performed += _ =>
        {
            hasjump.OnHasJump();
            playerAnimations.JumpAnimation();
        };

        inputManager.Player.ChangeAttack.performed += _ =>
        {
            if (GameManager.instance.GetArrows() > 0 && attackType.Equals("Punch"))
            {
                attackType = attackOptions[2];
                GameManager.instance.EnableArrowAttackItem();
            }
            else if (GameManager.instance.GetArrows() > 0 && attackType.Equals("Arrow"))
            {
                attackType = attackOptions[1];
                GameManager.instance.EnablePunchAttackItem();
            }
        };

        inputManager.Player.Shoot.performed += _ =>
        {
            if (attackType.Equals(attackOptions[2]))
            {
                playerAnimations.ShootAnimation();
                GameManager.instance.SubArrows();
                if (GameManager.instance.GetArrows() <= 0)
                {
                    attackType = attackOptions[1];
                    GameManager.instance.EnablePunchAttackItem();
                }
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
        CollisionProvider.doubleJumpPowerUpCollision += hasjump.EnableDoubleJump;
        CollisionProvider.doubleJumpPowerUpCollision += GameManager.instance.SetDoubleJumpGem;
        SceneManager.sceneLoaded += GetComponent<InitPosPlayer>().NewPosPlayer;
        CollisionProvider.keyCollision += GameManager.instance.SetKey;
        //inputManager.Player.HorMove.performed += _ => playerAnimations.JumpAnimation();
    }

    private void OnhasHurtJump()
    {
        if (rb.velocity.y > 0.5f) return;
        rb.AddForce(Vector2.up * 195, ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name.Equals("Loading"))
        {
            return;
        }

        inputManager.Player.Jump.performed -= _ =>
        {
            hasjump.OnHasJump();
            playerAnimations.JumpAnimation();
        };

        inputManager.Player.Shoot.performed -= _ =>
        {
            if (GameManager.instance.GetArrows() > 0)
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
        CollisionProvider.keyCollision -= GameManager.instance.SetKey;
        CollisionProvider.doubleJumpPowerUpCollision -= hasjump.EnableDoubleJump;
        CollisionProvider.doubleJumpPowerUpCollision -= GameManager.instance.SetDoubleJumpGem;
        SceneManager.sceneLoaded -= GetComponent<InitPosPlayer>().NewPosPlayer;
        //inputManager.Player.HorMove.performed += _ => playerAnimations.JumpAnimation();

        inputManager.Disable();
    }

    // Update is called once per frame

    private void Update()
    {
        isOnGround = hasTouchGroundScript.isOnGround;
        isOnCeil = hasTouchGroundScript.isOnCeil;
        forceMovement = inputManager.Player.HorMove.ReadValue<float>();

    }

    private void FixedUpdate()
    {
        //Check if is on ground   
        if (forceMovement != 0)
        {
            hasMove.OnHasMove(forceMovement, isOnGround || isOnCeil, speed);
            playerAnimations.WalkAnimation(true);
            if (forceMovement > 0)
            {
                if (rb.velocity.magnitude > 0.5f)
                {
                    for (int i = 0; i < CamaraMain.parallax.Length; i++)
                    {
                        if (CamaraMain.parallax[i] != null)
                        {
                            CamaraMain.parallax[i].MoveBackgroundRight();
                        }
                    }
                }

                lookAt.flipX = false;
                playerLookAt = true;
            }
            else
            {
                if (rb.velocity.magnitude > 0.5f)
                {
                    for (int i = 0; i < CamaraMain.parallax.Length; i++)
                    {
                        CamaraMain.parallax[i].MoveBackgroundLeft();
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
