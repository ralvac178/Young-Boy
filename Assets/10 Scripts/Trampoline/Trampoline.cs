using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public static bool isAtTop;
    
    public void EnableJumper()
    {
        isAtTop = true;
    }

    public void DisableJumper()
    {
        isAtTop = false;
    }     
}
