using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float force;
    private ArrowSound arrowSound;
    private void Start()
    {
        arrowSound = transform.parent.GetComponent<ArrowSound>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            arrowSound.PlayArrowSound();
            Dissapear();
        }
    }

    public void Dissapear()
    {
        Destroy(this.gameObject);
    }
}
