using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Enemy", order = 1)]

public class EnemyConfig : ScriptableObject
{
    public string type;
    public bool hasAllPowerUps;
    public GameObject[] fullPowerUps;
    public GameObject dectivateTrapPowerUp;
    public GameObject explosion;
    public int lives;
    public float speed;
}