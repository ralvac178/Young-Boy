using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBackGroundGUI : MonoBehaviour
{
    [SerializeField] private float speed;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>().material;
        material.mainTextureOffset = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float offset = speed * Time.deltaTime;
        
        material.mainTextureOffset = new Vector2(material.mainTextureOffset.x + offset,
            material.mainTextureOffset.y);
    }
}
