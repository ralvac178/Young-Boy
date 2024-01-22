using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GetGem : MonoBehaviour
{
    [SerializeField] private GameObject fireworks;
    private CamaraMain cameraMain;
    private void Start()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamaraMain>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.NextLevel();
            GameManager.instance.SetGem();
            SoundManager.instance.SoundPlayerGotGem();
            GameManager.instance.stopPlayer = true;        
            GameOverCanvasSingleton.instance.ChangeToMissionComplete();
            GameOverCanvasSingleton.instance.OpenGameOverCanvas();
            gameObject.SetActive(false);
            if (fireworks != null) fireworks.SetActive(true);

            // PlayerPrefs
            PlayerPrefs.SetInt("Coins", GameManager.instance.totalCoins);
            AchievementsController.SetKeys();
            

            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            switch (buildIndex)
            {
                case 1:
                    FindGem("1");
                    break;
                case 2:
                    FindGem("2");
                    break;
                default:
                    break;
            }

            AchievementsController.SetGems();
            // End PlayerPrefs

            cameraMain.ChangeToWinnerTheme();
            
        }       
    }

    public void FindGem(string level)
    {
        if (GameManager.instance.collectables.ContainsKey($"Gem{level}"))
        {
            GameManager.instance.collectables[$"Gem{level}"] = true;
        }
    }


}
