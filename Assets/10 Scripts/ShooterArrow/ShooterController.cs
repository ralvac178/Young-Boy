using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private GameObject arrowAsset;
    public void InstantiateArrow()
    {
        Instantiate(arrowAsset, transform.position - new Vector3(0, 0.06f, 0), Quaternion.identity);
    }
}
