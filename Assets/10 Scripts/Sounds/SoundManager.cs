using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSourceList;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceList = GetComponents<AudioSource>();
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
