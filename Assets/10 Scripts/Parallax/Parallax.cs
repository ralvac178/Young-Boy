using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void MoveBackgroundRight()
    {
        meshRenderer.material.mainTextureOffset = 
            new Vector2(meshRenderer.material.mainTextureOffset.x + Time.deltaTime* speed, 0);
    }

    public void MoveBackgroundLeft()
    {
        meshRenderer.material.mainTextureOffset =
            new Vector2(meshRenderer.material.mainTextureOffset.x - Time.deltaTime * speed, 0);
    }
}
