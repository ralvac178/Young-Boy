using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProvider : MonoBehaviour
{
    public delegate void TrapCollision();
    public static TrapCollision trapCollision;

    public delegate void TrampolineCollision();
    public static TrapCollision trampolineCollision;

    public static void OnTrapCollision()
    {
        trapCollision?.Invoke();
    }

    public static void OnTrampolineCollision()
    {
        trampolineCollision?.Invoke();
    }
}
