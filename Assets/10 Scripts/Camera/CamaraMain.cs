using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMain : MonoBehaviour
{
    public static Parallax[] parallax;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audioSource;

    private GameObject vcam;
    //Start is called before the first frame update
    void Start()
    {
        parallax = GetComponentsInChildren<Parallax>();

        vcam = GameObject.FindGameObjectWithTag("cam");

        //if (vcam != null) vcam.GetComponent<CameraGetPlayer>().GetBorder();
    }


    public void ChangeToWinnerTheme()
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }      
    }
}
