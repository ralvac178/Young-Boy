using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverCanvasSingleton : MonoBehaviour
{
    public static GameOverCanvasSingleton instance;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Text title;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        gameObject.SetActive(false);
    }

    public void OpenGameOverCanvas()
    {
        Invoke(nameof(GameOverCanvas), 1.25f);
    }

    public void GameOverCanvas()
    {
        gameObject.SetActive(true);
    }

    public void GameOverCanvasDeactive()
    {
        gameObject.SetActive(false);
    }

    public void ChangeToMissionComplete()
    {
        title.text = "MISSION COMPLETE!";
        if (GameManager.instance.GetLevel() != 3)
        {
            buttonText.text = "CONTINUE";
        }
        else
        {
            buttonText.text = "RETRY";
        }        
        gridLayoutGroup.cellSize = new Vector2(164, 50);
    }

    public void RestoreToGameOver()
    {
        title.text = "GAME OVER";
        buttonText.text = "RETRY";
        gridLayoutGroup.cellSize = new Vector2(142, 50);
    }
}
