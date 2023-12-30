using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasGetKey : MonoBehaviour
{
    public void AddKey(string keyColor)
    {
        GameManager.instance.SetKey(keyColor);
    }
}
