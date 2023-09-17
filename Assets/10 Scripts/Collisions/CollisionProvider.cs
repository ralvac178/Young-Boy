using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProvider : MonoBehaviour
{
    public delegate void TrapCollision();
    public static TrapCollision trapCollision;

    public delegate void TrampolineCollision(float force);
    public static TrampolineCollision trampolineCollision;

    public static void OnTrapCollision()
    {
        trapCollision?.Invoke();
    }

    public static void OnTrampolineCollision(float force)
    {
        trampolineCollision?.Invoke(force);
    }
}
