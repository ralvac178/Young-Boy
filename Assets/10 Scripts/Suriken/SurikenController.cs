using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurikenController : MonoBehaviour
{
    [SerializeField] private float offset;
    private Vector2 initPosition;
    private bool turnDown;
    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        //transform.Translate
    }

    // Update is called once per frame
    void Update()
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
}
