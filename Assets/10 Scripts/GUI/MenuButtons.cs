using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent onHighlighedEvent; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHighlighedEvent?.Invoke();
    }

    public void GoToScene()
    {
        if (gameObject.name.Equals("RetryButton"))
        {
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
}
