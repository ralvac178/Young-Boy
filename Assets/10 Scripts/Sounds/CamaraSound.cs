using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
    }

    public void SetVolume()
    {
        if (audioSource != null)
        {
            if (SoundManager.volume == 0)
            {
                audioSource.volume = 0;
            }
            else if (SoundManager.volume <= 0.2f)
            {
                audioSource.volume = 0.15f;
            }
            else
            {
                audioSource.volume = SoundManager.volume;
            }
        }
    }
}