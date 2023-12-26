using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    [SerializeField] private float speed, separationDistamce;
    private Vector3 initPosition;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        StartCoroutine(nameof(PowerUpTranslate));
    }

    public IEnumerator PowerUpTranslate()
    {
        bool enableMovement = true;
        while (enableMovement)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (transform.position.y - separationDistamce > initPosition.y)
            {
                enableMovement = false;
            }

            yield return null;
        }

        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
