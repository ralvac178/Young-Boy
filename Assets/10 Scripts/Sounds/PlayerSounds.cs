using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public void PlaySoundStep1()
    {
        SoundManager.instance.SoundPlayerStep1();
    }

    public void PlaySoundStep2()
    {
        SoundManager.instance.SoundPlayerStep2();
    }

}
