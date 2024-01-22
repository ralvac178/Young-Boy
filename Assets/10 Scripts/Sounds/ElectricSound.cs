using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        SetVolume();
    }

    public void PlaySound()
    {
        if (audioSource != null) audioSource.Play();
    }

    public void StopSound()
    {
        if (audioSource != null) audioSource.Stop();
    }

    public void SetVolume()
    {
        if (audioSource != null)
        {
            if (SoundManager.SFXMute) audioSource.mute = true;
            else
            {
                audioSource.mute = false;
            }
        }
    }
}