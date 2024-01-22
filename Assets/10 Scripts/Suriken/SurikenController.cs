using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurikenController : MonoBehaviour
{
    [SerializeField] private SurikenConfig config;
    [SerializeField] private float offset;
    private Vector2 initPosition;
    private bool turnDown;
    private string type;
    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        type = config.type;
    }

    // Update is called once per frame
    void Update()
    {
        if (type.Equals("Vertical"))
        {
            MoveVertical();
        }
        else
        {
            MoveHorizontal();
        }
    }

    private void MoveVertical()
    {
        if (!turnDown && (transform.position.y > initPosition.y - offset))
        {
            transform.Translate(Vector2.down * force * Time.deltaTime);
        }
        else if (turnDown && (transform.position.y < initPosition.y))
        {
            transform.Translate(Vector2.up * force * Time.deltaTime);
        }
        else if (!turnDown && (transform.position.y < initPosition.y - offset))
        {
            turnDown = true;
        }
        else if (turnDown && (transform.position.y > initPosition.y))
        {
            turnDown = false;
        }
    }

    private void MoveHorizontal()
    {
        if (!turnDown && (transform.position.x < initPosition.x + offset))
        {
            transform.Translate(Vector2.right * force * Time.deltaTime);
        }
        else if (turnDown && (transform.position.x > initPosition.x))
        {
            transform.Translate(Vector2.left * force * Time.deltaTime);
        }
        else if (!turnDown && (transform.position.x > initPosition.x + offset))
        {
            turnDown = true;
        }
        else if (turnDown && (transform.position.x < initPosition.x))
        {
            turnDown = false;
        }
    }
}
