using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasGetCoin : MonoBehaviour
{
    public void AddPoints(int points)
    {
        GameManager.instance.AddCoins(points);
    }
}
