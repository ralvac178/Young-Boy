using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTraps : MonoBehaviour
{
    [SerializeField] private List<GameObject> traps;
    private SpriteRenderer renderFlag;

    private void Start()
    {
        renderFlag = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        if (renderFlag.isVisible) 
        {
            foreach (var trap in traps)
            {
                if (!trap.activeInHierarchy)
                {
                    trap.SetActive(true);
                }
            }            
        }
        else 
        {
            foreach (var trap in traps)
            {
                if (trap.activeInHierarchy)
                {
                    trap.SetActive(false);
                }
            }          
        }
    }
}
