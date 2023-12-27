using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public void Launch(GameObject powerUp, Vector3 position)
    {
        Instantiate(powerUp, position, Quaternion.identity);
    }
}
