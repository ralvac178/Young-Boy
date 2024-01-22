using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlocks : MonoBehaviour
{
    private float timer;
    [SerializeField] private Animator[] animatorArray;

    private void Start()
    {
        animatorArray = GetComponentsInChildren<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!transform.gameObject.name.Contains("Fall")) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            if (timer >= 1.25f)
            {
                foreach (var animator in animatorArray)
                {
                    animator.SetTrigger("Breaking");
                }
                timer = 0;
            }
        }     
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            foreach (var animator in animatorArray)
            {
                animator.SetTrigger("Breaking");
                SoundManager.instance.BricksSound();
            }

            Destroy(collision.gameObject);
        }
    }
}
