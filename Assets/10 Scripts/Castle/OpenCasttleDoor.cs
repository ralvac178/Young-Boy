using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (GameManager.instance.keys >= 0)
            {
                animator.SetTrigger("OpenDoor");
                GameManager.instance.keys = 0;
                SceneManager.LoadScene("Stage2", LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("Yo haven't keys enought");
            }
        }
    }
}
