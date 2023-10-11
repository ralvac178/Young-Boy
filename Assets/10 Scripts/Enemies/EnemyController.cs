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
    private HasDetectPlayer hasDetectPlayerScript;
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
        hasDetectPlayerScript = GetComponent<HasDetectPlayer>();
        characterAnimations = GetComponent<CharacterAnimations>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(nameof(LookAt));
    }

    private void Update()
    {
        if ((hasDetectPlayerScript.playerOnLeft || hasDetectPlayerScript.playerOnRight)
            && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Enemy_hurt") &&
            !(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
        {
            characterAnimations.PunchAnimation();
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
            if (hasDetectEdgesScript.isOnLeftEdge)
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
            else if (hasDetectEdgesScript.isOnRightEdge)
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
            else if (hasDetectPlayerScript.playerOnLeft || hasDetectPlayerScript.playerOnRight)
            {
                speed = 0;
                characterAnimations.WalkAnimation(false);               
            }
            else
            {
                characterAnimations.WalkAnimation(true);
            }
            yield return null;
            if (rb.velocity.x > 1f || rb.velocity.x < -1)
            {
                speed = enemyConfig.speed / 1.5f;
            }
            else
            {
                speed = enemyConfig.speed;
            }
        }      
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(LookAt));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player")) return;

            if (hasDetectPlayerScript.playerOnLeft)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 50, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<HasDamage>().OnHasDamage();
            }
            else if (hasDetectPlayerScript.playerOnRight)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 50, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<HasDamage>().OnHasDamage();
            }

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30, ForceMode2D.Impulse);
    }
}
