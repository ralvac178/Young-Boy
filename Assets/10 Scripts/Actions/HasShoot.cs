using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasShoot : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject shooter;

    // Start is called before the first frame update
    public void ShootArrow()
    {
        Instantiate(arrow, shooter.transform.position - new Vector3(0.1f,0,0), Quaternion.identity);
    }

    public void InitPunch()
    {
        PlayerController.isPunching = true;
        Invoke(nameof(EndPunch), 0.75f);
    }

    public void EndPunch()
    {
        PlayerController.isPunching = false;
    }
}
