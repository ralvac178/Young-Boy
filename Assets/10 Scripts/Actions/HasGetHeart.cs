using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasGetHeart : MonoBehaviour
{
    public void AddLives(int lives)
    {
        GameManager.instance.AddLives(lives);
    }
}
