using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProvider : MonoBehaviour
{
    public delegate void TrapCollision();
    public static TrapCollision trapCollision;

    public delegate void KeyCollision(string color);
    public static KeyCollision keyCollision;

    public delegate void LavapCollision();
    public static LavapCollision lavaCollision;

    public delegate void CoinCollision(int points);
    public static CoinCollision coinCollision;

    public delegate void ArrowsCollision();
    public static ArrowsCollision arrowsCollision;

    public delegate void DoubleJumpPowerUpCollision();
    public static DoubleJumpPowerUpCollision doubleJumpPowerUpCollision;

    public delegate void HeartCollision(int lives);
    public static HeartCollision heartCollision;

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

    public static void OnCoinCollision(int points)
    {
        coinCollision?.Invoke(points);
    }

    public static void OnArrowsCollision()
    {
        arrowsCollision?.Invoke();
    }

    public static void OnLavaCollision()
    {
        lavaCollision?.Invoke();
    }

    public static void OnKeyCollision(string color)
    {
        keyCollision?.Invoke(color);
    }

    public static void OnDoubleJumpPowerUpCollision()
    {
        doubleJumpPowerUpCollision?.Invoke();
    }

    public static void OnHeartCollision(int lives)
    {
        heartCollision?.Invoke(lives);
    }
}
