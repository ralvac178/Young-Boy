using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraGetPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        player = GameObject.Find("Player");      
        cinemachineVirtualCamera.LookAt = player.transform;
        cinemachineVirtualCamera.Follow = player.transform;       
    }

    public void LeavePlayer()
    {
        cinemachineVirtualCamera.LookAt = null;
        cinemachineVirtualCamera.Follow = null;
    }

    public void GetPlayer()
    {
        cinemachineVirtualCamera.LookAt = player.transform;
        cinemachineVirtualCamera.Follow = player.transform;
    }
}
