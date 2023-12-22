using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    private AudioSource[] audioSources;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }
    public void PlayEnemyStep()
    {
        audioSources[0].Play();
    }

}