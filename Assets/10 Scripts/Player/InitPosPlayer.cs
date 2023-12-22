using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPosPlayer : MonoBehaviour
{
    public void NewPosPlayer(Scene scene, LoadSceneMode loadSceneMode)
    {
        GameObject initPosGameObject = GameObject.Find("InitPosPlayer");
        if (initPosGameObject != null)
        {
            Transform initPos = initPosGameObject.transform;

            if (initPos != null)
            {
                transform.position = initPos.position;
            }
        }
        

        
    }
}
