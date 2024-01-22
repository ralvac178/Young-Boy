using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseScriptSingleton : MonoBehaviour
{
    public static PauseScriptSingleton instance;

    // Start is called before the first frame update
    void Awake()
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

    public void EnablePauseCanvas()
    {
        gameObject.SetActive(true);
    }
}
