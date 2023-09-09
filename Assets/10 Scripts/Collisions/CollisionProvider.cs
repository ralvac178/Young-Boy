using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProvider : MonoBehaviour
{
    public delegate void TrapCollision();
    public static TrapCollision trapCollision;

    public static void OnTrapCollision()
    {
        trapCollision?.Invoke();
    }
}
