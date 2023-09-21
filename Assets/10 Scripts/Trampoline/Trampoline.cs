using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public static bool isAtTop;
    [HideInInspector] public float force;
    [SerializeField] TrampolineConfig trampolineConfig;

    private void Start()
    {
        force = trampolineConfig.forceJump;
    }

    public void EnableJumper()
    {
        isAtTop = true;
    }

    public void DisableJumper()
    {
        isAtTop = false;
    }     
}
