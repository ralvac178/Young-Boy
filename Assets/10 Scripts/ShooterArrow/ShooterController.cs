using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private GameObject arrowAsset;
    private SpriteRenderer sR;

    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }
    public void InstantiateArrow()
    {
        if (true)
        {
            Instantiate(arrowAsset, transform.position - new Vector3(0, 0.06f, 0), Quaternion.identity, transform);
        }
    }
}
