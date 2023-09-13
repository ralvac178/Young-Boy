using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProvider : MonoBehaviour
{
    public delegate void TrapCollision(Collider2D collider);
    public static TrapCollision trapCollision;

    public delegate void TrampolineCollision(float force);
    public static TrampolineCollision trampolineCollision;

    public static void OnTrapCollision(Collider2D collider)
    {
        trapCollision?.Invoke(collider);
    }

    public static void OnTrampolineCollision(float force)
    {
        trampolineCollision?.Invoke(force);
    }
}
