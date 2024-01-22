using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetVolume();
    }
    public void PlayArrowSound()
    {
        if(audioSource != null) audioSource.Play();
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
