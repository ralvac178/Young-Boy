using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSourceList;
    [SerializeField] private Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceList = GetComponents<AudioSource>();

    }

    // Mute all menu sounds
    public void MuteMenuSounds()
    {
        foreach (var item in audioSourceList)
        {
            item.mute = true;
        }
    }

    // UnMute all menu sounds
    public void UnMuteMenuSounds()
    {
        foreach (var item in audioSourceList)
        {
            item.mute = false;
        }
    }

    //Set SFX Volume
    public void SetVolumeSFX()
    {
        if (volumeSlider != null)
        {
            foreach (var item in audioSourceList)
            {
                if (volumeSlider.value <= 0f)
                {
                    item.volume = 0;
                }
                else
                {
                    item.volume = volumeSlider.value + 0.4f;
                }             
            }
        }       
    }

    //Sounds of Menu
    public void SoundMenuButton()
    {
        audioSourceList[0].Play();
    }

    public void SoundClickButton()
    {
        audioSourceList[1].Play();
    }

    // Sounds of Player
}
