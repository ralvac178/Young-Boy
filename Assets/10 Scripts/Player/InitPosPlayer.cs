using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPosPlayer : MonoBehaviour
{
    public void NewPosPlayer(Scene scene, LoadSceneMode loadSceneMode)
    {
        Transform initPos = GameObject.Find("InitPosPlayer").transform;
        transform.position = initPos.position;
    }
}
