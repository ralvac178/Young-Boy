using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMain : MonoBehaviour
{
    public static Parallax[] parallax;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audioSource;

    //Start is called before the first frame update
    void Start()
    {
        parallax = GetComponentsInChildren<Parallax>();
    }


    public void ChangeToWinnerTheme()
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }      
    }
}
