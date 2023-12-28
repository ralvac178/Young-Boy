using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvasSingleton : MonoBehaviour
{
    public static GameOverCanvasSingleton instance;

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
}
