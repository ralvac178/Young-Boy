using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
