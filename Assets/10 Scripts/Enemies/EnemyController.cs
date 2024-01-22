using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private HasMove hasMoveScript;
    public bool isAlive = true;
    public float speed;
    [HideInInspector] public int lives;
    private GameObject powerUp, explosion;
    private HasTouchGround hasTouchGroundScript;
    private HasDetectEdges hasDetectEdgesScript;
    private HasDetectObstacle hasDetectObstaclesScript;
    private HasDamage playerHasDamage;
    private SpriteRenderer sr;
    private CapsuleCollider2D capsuleCollider;
    public bool enableMovement = true;
    public Vector2 direction = Vector2.right;

    private CharacterAnimations characterAnimations;
    private Rigidbody2D rb;
    private Launcher launcher;
    private Dust dust;
    private GameObject player;
    private Rigidbody2D playerRigidBody2D;
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

        lives = enemyConfig.lives;
        speed = enemyConfig.speed;
        explosion = enemyConfig.explosion;
        sr = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        hasTouchGroundScript = GetComponent<HasTouchGround>();
        player = GameObject.Find("Player");
        playerHasDamage = player.GetComponent<HasDamage>();
        playerRigidBody2D = player.GetComponent<Rigidbody2D>();
        hasDetectEdgesScript = GetComponent<HasDetectEdges>();
        hasDetectObstaclesScript = GetComponent<HasDetectObstacle>();
        characterAnimations = GetComponent<CharacterAnimations>();
        rb = GetComponent<Rigidbody2D>();
        dust = GetComponentInChildren<Dust>();
        launcher = GetComponent<Launcher>();
        StartCoroutine(nameof(LookAt));
    }

    private void Update()
    {
        if (lives <= 0) return;

        if ((hasDetectObstaclesScript.playerOnLeft || hasDetectObstaclesScript.playerOnRight)
             && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Enemy_hurt") &&
             !(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
        {
            speed = 0;
            characterAnimations.WalkAnimation(false);
            AttackAnimation();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lives > 0) hasMoveScript.OnHasMove(direction.x, hasTouchGroundScript.isOnGround, speed);
    }

    IEnumerator LookAt()
    {
        characterAnimations.WalkAnimation(true);
        while (lives > 0)
        {
            if (hasDetectEdgesScript.isOnLeftEdge || hasDetectObstaclesScript.wallOnLeft)
            {
                direction = Vector2.right;
                transform.localScale = Vector3.one;
                speed = 0;
                characterAnimations.WalkAnimation(false);
                yield return new WaitForSeconds(2.8f);
                speed = enemyConfig.speed;             
                characterAnimations.WalkAnimation(true);
                yield return new WaitForSeconds(1.5f);
            }
            else if (hasDetectEdgesScript.isOnRightEdge || hasDetectObstaclesScript.wallOnRight)
            {
                direction = Vector2.left;
                transform.localScale = new Vector3(-1, 1, 1);
                speed = 0;
                characterAnimations.WalkAnimation(false);
                yield return new WaitForSeconds(2.8f);
                speed = enemyConfig.speed;
                characterAnimations.WalkAnimation(true);
                yield return new WaitForSeconds(1.5f);
            }
            else
            {
                speed = enemyConfig.speed;
                characterAnimations.WalkAnimation(true);
            }   
            
            ////////////////////////////// Speed Regulator
            if (rb.velocity.x > 1f || rb.velocity.x < -1)
            {
                speed = enemyConfig.speed / 1.5f;
            }
            else
            {
                speed = enemyConfig.speed;
            }

            yield return null;
        }

        StopCoroutine(nameof(LookAt));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(LookAt));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.tag.Equals("Player")) return;

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attack") &&
            GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            if (hasDetectObstaclesScript.playerOnLeft && direction == Vector2.left)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 120, ForceMode2D.Impulse);
                if (!playerHasDamage.isDamage)
                {
                    playerHasDamage.OnHasDamage();
                }               
            }
            else if (hasDetectObstaclesScript.playerOnRight && direction == Vector2.right)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 120, ForceMode2D.Impulse);
                if (!playerHasDamage.isDamage)
                {
                    playerHasDamage.OnHasDamage();
                }
            }
        }

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100, ForceMode2D.Impulse);
        hasMoveScript.OnHasMove(direction.x, hasTouchGroundScript.isOnGround, speed * 4.25f);
        AddForcekWithContact();
    }

    private void AttackAnimation()
    {
        int random = Random.Range(0, 2);
        if (random == 1) characterAnimations.PunchAnimation();
        else characterAnimations.BiteAnimation();
    }

    public void SubLives()
    {
        if (lives > 0)
        {
            lives--;
            if (lives <= 0)
            {
                isAlive = false;
                Invoke(nameof(DeleteEnemy), 5);
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

    public void PlayDust()
    {
        dust.EnemyDust();
    }

    public void AddForcekWithContact()
    {
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
}
