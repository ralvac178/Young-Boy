using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent onHighlighedEvent;
    [SerializeField] private TextMeshProUGUI text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHighlighedEvent?.Invoke();
    }

    public void GoToScene()
    {
        if (gameObject.name.Equals("RetryButton"))
        {
            if (text.text.Equals("RETRY"))
            {
                LoadingScreen.retry = true;
            }
            GameOverCanvasSingleton.instance.GameOverCanvasDeactive();
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
}
