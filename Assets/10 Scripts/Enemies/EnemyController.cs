using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private HasMove hasMoveScript;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = enemyConfig.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        hasMoveScript.OnHasMove(Vector2.right.x, true, speed);
    }
}
