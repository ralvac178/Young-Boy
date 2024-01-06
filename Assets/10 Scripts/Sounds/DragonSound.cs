using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void StepSound()
    {
        audioSource.Play();
    }
}
