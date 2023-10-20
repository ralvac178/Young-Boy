using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCasttleDoor : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.keys >= 3)
            {
                animator.SetTrigger("OpenDoor");
            }
            else
            {
                Debug.Log("Yo haven't keys enought");
            }
        }
    }
}
