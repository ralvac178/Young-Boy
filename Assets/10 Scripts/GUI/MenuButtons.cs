using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent onHighlighedEvent; 
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        onHighlighedEvent?.Invoke();
    }

    public void GoToScene()
    {
        SceneManager.LoadScene("Loading");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
