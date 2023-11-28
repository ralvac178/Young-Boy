using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMain : MonoBehaviour
{
    public static Parallax[] parallax;

    //Start is called before the first frame update
    void Start()
    {
        parallax = GetComponentsInChildren<Parallax>();
    }
}
