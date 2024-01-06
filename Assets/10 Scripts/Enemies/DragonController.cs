using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
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
             && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Enemy_hurt") &&
             !(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
        {
            direction = Vector2.zero;
            characterAnimations.WalkAnimation(false);
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
                else
                {
                    direction = Vector2.right;
                    transform.localScale = new Vector3(-1, 1, 1);
                    if (!animator.GetBool("IsWalking"))
                    {
                        animator.SetBool("IsWalking", true);
                    }                 

                    //if() condition to enable movement just within a short distance from player
                    hasMove.OnHasMove(direction.x, hasTouchGround.isOnGround, speed);
                }
            }
            else if (player.transform.position.x < transform.position.x)
            {
                if (hasDetectObstacle.wallOnLeft || hasDetectEdges.isOnLeftEdge)
                {
                    animator.SetBool("IsWalking", false);
                    direction = Vector2.zero;
               
                }
                else
                {
                    direction = Vector2.left;
                    transform.localScale = new Vector3(1, 1, 1);
                    if (!animator.GetBool("IsWalking"))
                    {
                        animator.SetBool("IsWalking", true);
                    }
                    //if() condition to enable movement just within a short distance from player
                    hasMove.OnHasMove(direction.x, hasTouchGround.isOnGround, speed);
                }
            }
            
        }     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attack") &&
            GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            if (hasDetectObstacle.playerOnLeft && direction == Vector2.left)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 120, ForceMode2D.Impulse);
                if (!playerHasDamage.isDamage)
                {
                    playerHasDamage.OnHasDamage();
                }
            }
            else if (hasDetectObstacle.playerOnRight && direction == Vector2.right)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 120, ForceMode2D.Impulse);
                if (!playerHasDamage.isDamage)
                {
                    playerHasDamage.OnHasDamage();
                }
            }
        }

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 120, ForceMode2D.Impulse);
    }

    public void SubLives()
    {
        if (lives > 0)
        {
            lives--;
            if (lives <= 0)
            {
                isAlive = false;
                Invoke(nameof(DeleteEnemy), 5f);
                characterAnimations.WalkAnimation(false);
                characterAnimations.DeadAnimation();
            }
        }
    }

    public void DeleteEnemy()
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

    private void AttackAnimation()
    {
        int random = Random.Range(0, 2);
        if (random == 1) characterAnimations.PunchAnimation();
        else characterAnimations.BiteAnimation();
    }
}
