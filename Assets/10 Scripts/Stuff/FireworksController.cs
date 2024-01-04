using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksController : MonoBehaviour
{
    [SerializeField] private int waitForStartTime;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartCoroutineRetarded), waitForStartTime);
    }

    public IEnumerator StartFirework()
    {
        while (true)
        {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void StartCoroutineRetarded()
    {
        StartCoroutine(nameof(StartFirework));
    }

    public void Sound()
    {
        SoundManager.instance.FireworksSound();
    }
}
