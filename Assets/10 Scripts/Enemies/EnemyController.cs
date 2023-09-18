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
    private SpriteRenderer sr;

    public bool enableMovement = true;
    public Vector2 direction = Vector2.right;

    private CharacterAnimations characterAnimations;
    // Start is called before the first frame update
    void Start()
    {
        speed = enemyConfig.speed;
        sr = GetComponent<SpriteRenderer>();
        hasTouchGroundScript = GetComponent<HasTouchGround>();
        hasDetectEdgesScript = GetComponent<HasDetectEdges>();
        characterAnimations = GetComponent<CharacterAnimations>();
        StartCoroutine(nameof(LookAt));
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
            yield return null;
        }      
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(LookAt));
    }
}
