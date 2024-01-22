using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSourceList;
    public static SoundManager instance;
    public static float volume;
    public static bool SFXMute = false;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        audioSourceList = GetComponents<AudioSource>();
    }

    public void SetBGMVolume(Slider volumeSlider)
    {
        if (volumeSlider != null)
        {
            volume = volumeSlider.value;
            PlayerPrefs.SetFloat("BGMusic", volume);
        }
        else
        {
            volume = PlayerPrefs.GetFloat("BGMusic", 0.2f);
        }   
    }

    //Set SFX Volume
    public void SetVolumeSFX()
    {
        if (audioSourceList != null)
        {
            if (SFXMute)
            {
                PlayerPrefs.SetInt("SFX", 1);
                foreach (var audiosource in audioSourceList)
                {
                    audiosource.mute = true;
                }
            }
            else
            {
                PlayerPrefs.SetInt("SFX", 0);
                foreach (var audiosource in audioSourceList)
                {
                    audiosource.mute = false;
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
        if(PlayerController.isOnGround) audioSourceList[8].Play();
    }

    public void SoundPlayerStep2()
    {
        if (PlayerController.isOnGround) audioSourceList[9].Play();
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

    public void GameOverSound()
    {
        audioSourceList[15].Play();
    }

    public void BricksSound()
    {
        audioSourceList[16].Play();
    }

    public void OpenSafeSound()
    {
        audioSourceList[17].Play();
    }

    public void FireworksSound()
    {
        audioSourceList[18].Play();
    }

    public void DragonHurtSound()
    {
        audioSourceList[19].Play();
    }

    public void DragonDeadSound()
    {
        audioSourceList[20].Play();
    }

    public void SFXSwitch()
    {
        if (SFXMute == true) SFXMute = false;
        else SFXMute = true;

        SetVolumeSFX();
    }
}
