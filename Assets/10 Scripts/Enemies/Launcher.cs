using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public void Launch(GameObject powerUp, Vector3 position)
    {
        GameObject instance = Instantiate(powerUp, position, Quaternion.identity);
        if (instance.CompareTag("Splash"))
        {
            Destroy(instance, 5f);
        }
    }
}
