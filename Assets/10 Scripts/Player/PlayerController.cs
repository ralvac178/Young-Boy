using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private HasMove hasMove;
    [SerializeField] private HasJump hasjump;
    [SerializeField] private HasGetArrows hasGetArrows;
    [SerializeField] private HasGetHeart hasGetHeart;
    [SerializeField] private HasGetKey hasGetKey;
    [SerializeField] private HasChangeAttackType hasChangeAttackType;
    [SerializeField] private CharacterAnimations playerAnimations;

    [SerializeField] private HasDamage hasDamage;
    [SerializeField] private HasGetCoin hasGetCoin;
    [SerializeField] private SpriteRenderer lookAt;

    private HasTouchGround hasTouchGroundScript;
    public static bool isOnGround, isOnCeil;
    [SerializeField] private float speed;
    private float initSpeed;
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

    private Dust dust;

    [SerializeField] private UnityEvent onPause;

    // Start is called before the first frame update
    void Awake()
    {
        inputManager = new InputManager();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }    
    }

    private void Start()
    {
        dust = GetComponentInChildren<Dust>();
        hasTouchGroundScript = transform.GetComponent<HasTouchGround>();
        rb = GetComponent<Rigidbody2D>();
        initSpeed = speed;
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
            if (GameManager.GetArrows() > 0 && attackType.Equals("Punch"))
            {
                attackType = attackOptions[2];
                hasChangeAttackType.EnableArrowAttackItem();
            }
            else if (GameManager.GetArrows() > 0 && attackType.Equals("Arrow"))
            {
                attackType = attackOptions[1];
                hasChangeAttackType.EnablePunchAttackItem();
            }
        };

        inputManager.Player.Shoot.performed += _ =>
        {
            if (attackType.Equals(attackOptions[2]))
            {
                playerAnimations.ShootAnimation();
                GameManager.instance.SubArrows();
                if (GameManager.GetArrows() <= 0)
                {
                    attackType = attackOptions[1];
                    hasChangeAttackType.EnablePunchAttackItem();
                }
            }
            else
            {
                playerAnimations.PunchAnimation();
            }
        };

        inputManager.PauseManager.Pause.performed += _ =>
        {
            if (SceneManager.GetActiveScene().buildIndex != 0 &&
                SceneManager.GetActiveScene().buildIndex != 3)
            {
                PauseScriptSingleton.instance.EnablePauseCanvas();
                onPause?.Invoke();
            }
        };

        inputManager.Player.Run.started += EnhanceSpeed;
        inputManager.Player.Run.canceled += RestartSpeed;

        CollisionProvider.trapCollision += hasDamage.OnHasDamage;
        CollisionProvider.trapCollision += hasDamage.OnFinishGame;
        CollisionProvider.coinCollision += hasGetCoin.AddPoints;
        CollisionProvider.arrowsCollision += hasGetArrows.AddArrows;
        CollisionProvider.lavaCollision += OnhasHurtJump;
        CollisionProvider.lavaCollision += hasDamage.OnHasDamage;
        CollisionProvider.lavaCollision += hasDamage.OnFinishGame;
        CollisionProvider.doubleJumpPowerUpCollision += hasjump.EnableDoubleJump;
        CollisionProvider.doubleJumpPowerUpCollision += hasjump.SetDoubleJumpIcon;
        SceneManager.sceneLoaded += GetComponent<InitPosPlayer>().NewPosPlayer;
        CollisionProvider.keyCollision += hasGetKey.AddKey;
        CollisionProvider.heartCollision += hasGetHeart.AddLives;
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

        inputManager.Player.ChangeAttack.performed -= _ =>
        {
            // if Get Arrows > 0 && ... replaced by true for test
            if (GameManager.GetArrows() > 0 && attackType.Equals("Punch"))
            {
                attackType = attackOptions[2];
                hasChangeAttackType.EnableArrowAttackItem();
            }
            else if (GameManager.GetArrows() > 0 && attackType.Equals("Arrow"))
            {
                attackType = attackOptions[1];
                hasChangeAttackType.EnablePunchAttackItem();
            }
        };

        inputManager.Player.Shoot.performed -= _ =>
        {
            if (GameManager.GetArrows() > 0)
            {
                playerAnimations.ShootAnimation();
                GameManager.instance.SubArrows();
                if (GameManager.GetArrows() <= 0)
                {
                    attackType = attackOptions[1];
                    hasChangeAttackType.EnablePunchAttackItem();
                }
            }
            else
            {
                playerAnimations.PunchAnimation();
            }
        };

        inputManager.PauseManager.Pause.performed += _ =>
        {
            if (SceneManager.GetActiveScene().buildIndex != 0 &&
                SceneManager.GetActiveScene().buildIndex != 3)
            {
                PauseScriptSingleton.instance.EnablePauseCanvas();
                onPause?.Invoke();
            }
        };

        inputManager.Player.Run.started -= EnhanceSpeed;
        inputManager.Player.Run.canceled -= RestartSpeed;

        CollisionProvider.trapCollision -= hasDamage.OnHasDamage;
        CollisionProvider.trapCollision -= hasDamage.OnFinishGame;
        CollisionProvider.coinCollision -= hasGetCoin.AddPoints;
        CollisionProvider.arrowsCollision -= hasGetArrows.AddArrows;
        CollisionProvider.lavaCollision -= OnhasHurtJump;
        CollisionProvider.lavaCollision -= hasDamage.OnHasDamage;
        CollisionProvider.lavaCollision -= hasDamage.OnFinishGame;
        CollisionProvider.keyCollision -= hasGetKey.AddKey;
        CollisionProvider.doubleJumpPowerUpCollision -= hasjump.EnableDoubleJump;
        CollisionProvider.doubleJumpPowerUpCollision -= hasjump.SetDoubleJumpIcon;
        SceneManager.sceneLoaded -= GetComponent<InitPosPlayer>().NewPosPlayer;
        CollisionProvider.heartCollision -= hasGetHeart.AddLives;

        inputManager.Disable();
    }

    // Update is called once per frame

    private void Update()
    {
        isOnGround = hasTouchGroundScript.isOnGround;
        isOnCeil = hasTouchGroundScript.isOnCeil;
        if (GameManager.instance != null)
        {
            if (GameManager.instance.GetLives() > 0 && !GameManager.instance.stopPlayer)
            {
                forceMovement = inputManager.Player.HorMove.ReadValue<float>();
            }
            else
            {
                forceMovement = 0;
            }
        }     
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
                    dust.PlayerDust();
                    for (int i = 0; i < CamaraMain.parallax.Length; i++)
                    {
                        if (CamaraMain.parallax[i] != null)
                        {
                            CamaraMain.parallax[i].MoveBackgroundRight();
                        }
                    }
                }

                
                transform.localScale = Vector3.one;
                playerLookAt = true;
            }
            else
            {
                if (rb.velocity.magnitude > 0.5f)
                {
                    dust.PlayerDust();
                    for (int i = 0; i < CamaraMain.parallax.Length; i++)
                    {
                        CamaraMain.parallax[i].MoveBackgroundLeft();
                    }
                }


                transform.localScale = new Vector3(-1, 1, 1);
                playerLookAt = false;
            }
        }
        else
        {
            playerAnimations.WalkAnimation(false);
        }
    }

    public void SetDeadAnimation()
    {
        playerAnimations.DeadAnimation();
    }

    public void ResetLayer()
    {
        gameObject.layer = 6;
    }

    public void ResetAnimator()
    {
        playerAnimations.ResetAnimator();
    }

    public void EnhanceSpeed(InputAction.CallbackContext context)
    {
        speed = initSpeed * 1.45f;
        playerAnimations.RunAnimationEnabled();
    }

    public void RestartSpeed(InputAction.CallbackContext context)
    {
        speed = initSpeed;
        playerAnimations.RunAnimationDisabled();
    }

    public void RestartAttackOptions()
    {
        attackType = attackOptions[1];
        hasChangeAttackType.EnablePunchAttackItem();
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            // Code to handle pause state (e.g., pause game logic, show pause menu)
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            // Code to handle resume state (e.g., resume game logic, hide pause menu)
            Time.timeScale = 1f; // Resume the game
        }
    }
}
