using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasGetKey : MonoBehaviour
{
    public void AddKey(string keyColor)
    {
        GameManager.instance.SetKey(keyColor);

        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        switch (buildIndex)
        {
            case 1:
                FindKey(keyColor, "1");
                break;
            case 2:
                FindKey(keyColor, "2");
                break;
            default:
                break;
        }
    }

    public void FindKey(string color, string level)
    {
        if (GameManager.instance.collectables.ContainsKey($"Key{color}{level}"))
        {
            GameManager.instance.collectables[$"Key{color}{level}"] = true;
        }
    }
}
