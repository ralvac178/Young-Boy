using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour, IEnemyController
{
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private SpriteRenderer sr;
    private float speed;
    private GameObject powerUp, explosion;
    [HideInInspector] public int lives;
    public bool isAlive = true;
    private Launcher launcher;
    private HasDamage playerHasDamage;
    private Rigidbody2D playerRigidBody2D;
    private HasMove hasMove;
    private Animator animator;
    private HasTouchGround hasTouchGround;
    private PlayerController player;
    public Vector2 direction;
    private HasDetectEdges hasDetectEdges;
    private HasDetectObstacle hasDetectObstacle;
    private CharacterAnimations characterAnimations;
    // Start is called before the first frame update
    void Start()
    {
        if (enemyConfig.hasAllPowerUps)
        {
            int random = Random.Range(0, enemyConfig.fullPowerUps.Length);
            powerUp = enemyConfig.fullPowerUps[random];
        }
        else
        {
            powerUp = enemyConfig.dectivateTrapPowerUp;
        }

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRigidBody2D = player.GetComponent<Rigidbody2D>();
        lives = enemyConfig.lives;
        explosion = enemyConfig.explosion;
        speed = enemyConfig.speed;
        hasMove = GetComponent<HasMove>();
        hasTouchGround = GetComponent<HasTouchGround>();
        animator = GetComponent<Animator>();
        playerHasDamage = GameObject.Find("Player").GetComponent<HasDamage>();
        hasDetectEdges = GetComponent<HasDetectEdges>();
        hasDetectObstacle = GetComponent<HasDetectObstacle>();
        characterAnimations = GetComponent<CharacterAnimations>();
        launcher = GetComponent<Launcher>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0) return;

        if ((hasDetectObstacle.playerOnLeft || hasDetectObstacle.playerOnRight)
             && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Enemy_hurt") &&
             !(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
        {

            if (direction == Vector2.zero)
            {
                characterAnimations.BiteAnimation();
            }
            else
            {
                characterAnimations.PunchAnimation();
            }
        }

        if (!PlayerController.isOnCeil)
        {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) > 2)
            {
                direction = Vector2.zero;
                if (animator.GetBool("IsWalking"))
                {
                    animator.SetBool("IsWalking", false);
                }
                return;
            }
            if (player.transform.position.x >= transform.position.x)
            {
                if (hasDetectObstacle.wallOnRight || hasDetectEdges.isOnRightEdge)
                {
                    direction = Vector2.zero;
                    if (animator.GetBool("IsWalking"))
                    {
                        animator.SetBool("IsWalking", false);
                    }
                  
                }
                else if (Mathf.Abs(transform.position.x - player.gameObject.transform.position.x) > 0.25f &&            
                        Mathf.Abs(Vector2.Distance(this.gameObject.transform.position,
                                            player.gameObject.transform.position)) < 4.5f)
                {
                    direction = Vector2.right;
                    transform.localScale = new Vector3(-1, 1, 1);
                    if (!animator.GetBool("IsWalking"))
                    {
                        animator.SetBool("IsWalking", true);
                    }
                }
                else
                {
                    direction = Vector2.zero;
                    if (animator.GetBool("IsWalking"))
                    {
                        animator.SetBool("IsWalking", false);
                    }
                }
            }
            else if (player.transform.position.x < transform.position.x)
            {
                if (hasDetectObstacle.wallOnLeft || hasDetectEdges.isOnLeftEdge)
                {
                    animator.SetBool("IsWalking", false);
                    direction = Vector2.zero;
               
                }
                else if (Mathf.Abs(transform.position.x - player.gameObject.transform.position.x) > 0.25f && // Hre I should evaluate if distance is > to 0.5f
                    Mathf.Abs(Vector2.Distance(this.gameObject.transform.position,
                                           player.gameObject.transform.position)) < 4.5f)
                {
                    direction = Vector2.left;
                    transform.localScale = new Vector3(1, 1, 1);
                    if (!animator.GetBool("IsWalking"))
                    {
                        animator.SetBool("IsWalking", true);
                    }
                }
                else
                {
                    direction = Vector2.zero;
                    if (animator.GetBool("IsWalking"))
                    {
                        animator.SetBool("IsWalking", false);
                    }
                }
            }
        }
        else
        {
            direction = Vector2.zero;
            if (animator.GetBool("IsWalking"))
            {
                animator.SetBool("IsWalking", false);
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (lives > 0) hasMove.OnHasMove(direction.x, hasTouchGround.isOnGround, speed);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("attack") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            if (hasDetectObstacle.playerOnLeft && direction == Vector2.left)
            {
                playerRigidBody2D.AddForce(Vector2.left * 120, ForceMode2D.Impulse);
                if (!playerHasDamage.isDamage)
                {
                    playerHasDamage.OnHasDamage();
                }
            }
            else if (hasDetectObstacle.playerOnRight && direction == Vector2.right)
            {
                playerRigidBody2D.AddForce(Vector2.right * 120, ForceMode2D.Impulse);
                if (!playerHasDamage.isDamage)
                {
                    playerHasDamage.OnHasDamage();
                }
            }
        }

        hasMove.OnHasMove(direction.x, hasTouchGround.isOnGround, speed);
        AttackWithContact();
    }

    public void SubLives()
    {
        if (lives > 0)
        {
            lives--;
            if (lives <= 0)
            {
                isAlive = false;
                Invoke(nameof(DeleteCharacter), 5f);
                characterAnimations.WalkAnimation(false);
                characterAnimations.DeadAnimation();
            }
        }
    }

    public void DeleteCharacter()
    {
        launcher.Launch(explosion, transform.position);
        launcher.Launch(powerUp, transform.position);
        SoundManager.instance.SoundEnemyDissapear();
        gameObject.SetActive(false);
    }

    public void MakeTransparent()
    {
        rb.bodyType = RigidbodyType2D.Static;
        capsuleCollider.isTrigger = true;
        sr.color = new Color(0.8f, 0.8f, 0.14f, 0.36f);
    }

    public void AttackAnimation()
    {
        int random = Random.Range(0, 2);
        if (random == 1) characterAnimations.PunchAnimation();
        else characterAnimations.BiteAnimation();
    }

    public void AttackWithContact()
    {
        int random = Random.Range(0, 4);
        if (random == 0)
        {
            if (!playerHasDamage.isDamage)
            {
                characterAnimations.BiteAnimation();
                Invoke(nameof(DamageTimered), 0.36f);              
            }
        }
        hasMove.OnHasMove(direction.x, hasTouchGround.isOnGround, speed * 1.25f);
        playerRigidBody2D.AddForce(Vector2.up * 140, ForceMode2D.Impulse);
        if (player.transform.position.x < transform.position.x)
        {
            playerRigidBody2D.AddForce(Vector2.left * 28, ForceMode2D.Impulse);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            playerRigidBody2D.AddForce(Vector2.right * 28, ForceMode2D.Impulse);
        }
        else
        {
            playerRigidBody2D.AddForce(direction * 12, ForceMode2D.Impulse);
        }
    }

    public void DamageTimered()
    {
        playerHasDamage.OnHasDamage();
    }

    public void PlayDust()
    {
        throw new System.NotImplementedException();
    }

    public void AddForcekWithContact()
    {
        throw new System.NotImplementedException();
    }
}
