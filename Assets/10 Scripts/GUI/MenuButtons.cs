using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent onHighlighedEvent;
    [SerializeField] private TextMeshProUGUI textButton;
    [SerializeField] private GameObject uncheckSFXVolume, checkSFXVolume;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button level2;

    private Slider volumeSlider;
    private Button button;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        button = GetComponent<Button>();

        if (PlayerPrefs.HasKey("SFX"))
        {
            if (uncheckSFXVolume != null && checkSFXVolume != null)
            {
                if (PlayerPrefs.GetInt("SFX") == 0)
                {
                    uncheckSFXVolume.SetActive(false);
                    checkSFXVolume.SetActive(true);
                }
                else
                {
                    uncheckSFXVolume.SetActive(true);
                    checkSFXVolume.SetActive(false);
                }
            }
                     
        }
        else
        {
            SoundManager.SFXMute = false;
            PlayerPrefs.SetInt("SFX", 0);
            SoundManager.instance.SetVolumeSFX();
        }

        if (volumeSlider != null)
        {
            if (PlayerPrefs.HasKey("BGMusic"))
            {
                volumeSlider.value = PlayerPrefs.GetFloat("BGMusic", 0.2f);
            }
            else
            {
                volumeSlider.value = 0.2f;
                PlayerPrefs.SetFloat("BGMusic", 0.2f);
            }
        }
        else
        {
            if (audioSource!= null)
            {
                if (PlayerPrefs.HasKey("BGMusic"))
                {
                    audioSource.volume = PlayerPrefs.GetFloat("BGMusic", 0.2f);
                }
                else
                {
                    audioSource.volume = 0.2f;
                    PlayerPrefs.SetFloat("BGMusic", 0.2f);
                }

                SoundManager.instance.SetBGMVolume(null);
            }           
        }

        // Set sfx sounds at begin using play button
        if (gameObject.name.Equals("StartButton"))
        {
            if (PlayerPrefs.HasKey("SFX"))
            {
                if (PlayerPrefs.GetInt("SFX") == 0)
                {
                    SoundManager.SFXMute = false;
                }
                else
                {
                    Debug.Log("in");
                    SoundManager.SFXMute = true;
                }
            }
            else
            {
                SoundManager.SFXMute = false;
            }

            if (audioSource != null)
            {
                if (PlayerPrefs.HasKey("BGMusic"))
                {
                    audioSource.volume = PlayerPrefs.GetFloat("BGMusic", 0.2f);
                }
                else
                {
                    audioSource.volume = 0.2f;
                    PlayerPrefs.SetFloat("BGMusic", 0.2f);
                }
            }

            SoundManager.instance.SetVolumeSFX();
        }

        // Set Level 2 on Menu
        if (level2 != null)
        {
            if (PlayerPrefs.HasKey("EnableStage2"))
            {
                if (PlayerPrefs.GetInt("EnableStage2") == 1)
                {
                    level2.interactable = true;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHighlighedEvent?.Invoke();
    }

    public void GoToScene()
    {
        if (gameObject.name.Equals("RetryButton"))
        {
            if (textButton != null)
            {
                if (textButton.text.Equals("RETRY"))
                {
                    LoadingScreen.retry = true;
                }
                GameOverCanvasSingleton.instance.GameOverCanvasDeactive();
            }           
        }
        SceneManager.LoadScene("Loading");
    }

    public void GoToScene1()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetLevel(1);
        }
        else
        {
            LoadingScreen.levelToGo = 1;
        }  
        SceneManager.LoadScene("Loading");
    }

    public void GoToScene2()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetLevel(2);
        }
        else
        {
            LoadingScreen.levelToGo = 2;
        }
        SceneManager.LoadScene("Loading");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {       
        SceneManager.LoadScene(0);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            // Code to handle pause state (e.g., pause game logic, show pause menu)
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            // Code to handle resume state (e.g., resume game logic, hide pause menu)
            Time.timeScale = 1f; // Resume the game
        }
    }

    public void SetBGMVolume()
    {
        SoundManager.instance.SetBGMVolume(volumeSlider);
    }

    public void SetSFXVolume()
    {
        SoundManager.instance.SFXSwitch();
    }

    // Sounds
    //Sounds of Menu
    public void SoundMenuButton()
    {
        if(button != null)
        {
            if (button.IsInteractable())
            {
                SoundManager.instance.SoundMenuButton();
            }
        }
        
    }

    public void SoundClickButton()
    {
        SoundManager.instance.SoundClickButton();
    }
}
