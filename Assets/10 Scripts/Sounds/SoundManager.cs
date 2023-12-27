using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSourceList;
    [SerializeField] private Slider volumeSlider;
    public static SoundManager instance;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceList = GetComponents<AudioSource>();

    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
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
    public void SoundPlayerHurt()
    {
        if (!audioSourceList[7].isPlaying)
        {
            audioSourceList[2].Play();
        }      
    }

    public void SoundPlayerJump()
    {
        audioSourceList[4].Play();
    }

    public void SoundPlayerDoubleJump()
    {
        audioSourceList[5].Play();
    }

    public void SoundPlayerAttack()
    {
        audioSourceList[7].Play();
    }

    // Sounds of Items collected

    public void SoundCoinCollected()
    {
        audioSourceList[6].Play();
    }

    // Sounds of Enemies
    public void SoundEnemyHurt()
    {
        audioSourceList[3].Play();
    }

    // Steps Player
    public void SoundPlayerStep1()
    {
        audioSourceList[8].Play();
    }

    public void SoundPlayerStep2()
    {
        audioSourceList[9].Play();
    }

    // Get Gem

    public void SoundPlayerGotGem()
    {
        audioSourceList[10].Play();
    }

    // Get Key
    public void SoundPlayerGotKey()
    {
        audioSourceList[11].Play();
    }

    // Get Arrows and lives

    public void SoundPlayerGotArrowsNLives()
    {
        audioSourceList[12].Play();
    }

    // Enemy dead Sound
    public void SoundEnemyDead()
    {
        audioSourceList[13].Play();
    }

    public void SoundEnemyDissapear()
    {
        audioSourceList[14].Play();
    }
}
