using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private HasMove hasMoveScript;

    public float speed;

    private HasTouchGround hasTouchGroundScript;
    private HasDetectEdges hasDetectEdgesScript;
    private HasDetectObstacle hasDetectObstaclesScript;
    private SpriteRenderer sr;

    public bool enableMovement = true;
    public Vector2 direction = Vector2.right;

    private CharacterAnimations characterAnimations;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = enemyConfig.speed;
        sr = GetComponent<SpriteRenderer>();
        hasTouchGroundScript = GetComponent<HasTouchGround>();
        hasDetectEdgesScript = GetComponent<HasDetectEdges>();
        hasDetectObstaclesScript = GetComponent<HasDetectObstacle>();
        characterAnimations = GetComponent<CharacterAnimations>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(nameof(LookAt));
    }

    private void Update()
    {
        if ((hasDetectObstaclesScript.playerOnLeft || hasDetectObstaclesScript.playerOnRight)
            && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Enemy_hurt") &&
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
       hasMoveScript.OnHasMove(direction.x, hasTouchGroundScript.isOnGround, speed);
    }

    IEnumerator LookAt()
    {
        characterAnimations.WalkAnimation(true);
        while (true)
        {
            if (hasDetectEdgesScript.isOnLeftEdge || hasDetectObstaclesScript.wallOnLeft)
            {
                direction = Vector2.right;
                sr.flipX = false;
                speed = 0;
                characterAnimations.WalkAnimation(false);
                yield return new WaitForSeconds(3f);
                speed = enemyConfig.speed;             
                characterAnimations.WalkAnimation(true);
                yield return new WaitForSeconds(1.5f);
            }
            else if (hasDetectEdgesScript.isOnRightEdge || hasDetectObstaclesScript.wallOnRight)
            {
                direction = Vector2.left;
                sr.flipX = true;
                speed = 0;
                characterAnimations.WalkAnimation(false);
                yield return new WaitForSeconds(3f);
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
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(LookAt));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player")) return;

            if (hasDetectObstaclesScript.playerOnLeft && direction == Vector2.left)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 120, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<HasDamage>().OnHasDamage();
            }
            else if (hasDetectObstaclesScript.playerOnRight && direction == Vector2.right)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 120, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<HasDamage>().OnHasDamage();
            }

        if (transform.name.Contains("Barzag"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * 60, ForceMode2D.Impulse);
        }
        else if (transform.name.Contains("Shaman"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * 40, ForceMode2D.Impulse);
        }
        
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50, ForceMode2D.Impulse);
    }

    private void AttackAnimation()
    {
        int random = Random.Range(0, 2);
        if (random == 1) characterAnimations.PunchAnimation();
        else characterAnimations.BiteAnimation();
    }
}
