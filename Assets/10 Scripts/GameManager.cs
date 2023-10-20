using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int points;
    public int arrows;
    public int keys;
    public static GameManager instance;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddArrows()
    {
        arrows += 5;
    }

    public void SubArrows()
    {
        arrows--;
        Debug.Log(arrows);
    }
}
