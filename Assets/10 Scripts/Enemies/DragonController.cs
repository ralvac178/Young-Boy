using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public int speed;
    private HasMove hasMove;
    private Animator animator;
    private HasTouchGround hasTouchGround;
    [SerializeField]  private PlayerController player;
    public Vector2 direction;
    private HasDetectEdges hasDetectEdges;
    private HasDetectObstacle hasDetectObstacle;
    private CharacterAnimations characterAnimations;
    // Start is called before the first frame update
    void Start()
    {
        hasMove = GetComponent<HasMove>();
        hasTouchGround = GetComponent<HasTouchGround>();
        animator = GetComponent<Animator>();
        hasDetectEdges = GetComponent<HasDetectEdges>();
        hasDetectObstacle = GetComponent<HasDetectObstacle>();
        characterAnimations = GetComponent<CharacterAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.isOnCeil){

            if (player.transform.position.x >= transform.position.x)
            {
                if (hasDetectObstacle.wallOnRight || hasDetectEdges.isOnRightEdge)
                {
                    if (hasDetectObstacle.playerOnRight)
                    {
                        characterAnimations.BiteAnimation();
                    }

                    direction = Vector2.zero;
                    if (animator.GetBool("IsMoving"))
                    {
                       animator.SetBool("IsMoving", false);
                    }
                 
                }
                else
                {
                    direction = Vector2.right;
                    transform.localScale = new Vector3(-1, 1, 1);
                    animator.SetBool("IsMoving", true);
                    if (hasDetectObstacle.playerOnRight)
                    {
                        characterAnimations.PunchAnimation();
                    }
                }
            }
            else if (player.transform.position.x < transform.position.x)
            {
                if (hasDetectObstacle.wallOnLeft || hasDetectEdges.isOnLeftEdge)
                {
                    if (hasDetectObstacle.playerOnLeft)
                    {
                        characterAnimations.BiteAnimation();
                    }
                    direction = Vector2.zero;
                    if (animator.GetBool("IsMoving"))
                    {
                        animator.SetBool("IsMoving", false);
                    }
                }
                else
                {
                    direction = Vector2.left;
                    transform.localScale = new Vector3(1, 1, 1);
                    animator.SetBool("IsMoving", true);
                    if (hasDetectObstacle.playerOnLeft)
                    {
                        characterAnimations.PunchAnimation();
                    }
                }
            }
        }
        else
        {          
            direction = Vector2.zero;
            if (animator.GetBool("IsMoving"))
            {
                animator.SetBool("IsMoving", false);
            }
        }

        hasMove.OnHasMove(direction.x, hasTouchGround.isOnGround, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttackAnimation();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 80, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 80, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<HasDamage>().OnHasDamage();
        }   
    }

    private void AttackAnimation()
    {
        int random = Random.Range(0, 2);
        if (random == 2) characterAnimations.PunchAnimation();
        else characterAnimations.BiteAnimation();
    }
}
