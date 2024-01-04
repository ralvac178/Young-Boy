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
            SoundManager.instance.SoundPlayerGotGem();
            GameManager.instance.stopPlayer = true;        
            GameOverCanvasSingleton.instance.ChangeToMissionComplete();
            GameOverCanvasSingleton.instance.OpenGameOverCanvas();
            gameObject.SetActive(false);
            if (fireworks != null) fireworks.SetActive(true);
            cameraMain.ChangeToWinnerTheme();
        }       
    }

}
