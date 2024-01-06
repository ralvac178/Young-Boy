using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    private CameraGetPlayer cameraGetPlayer;
    [SerializeField] private Transform flag;
    [SerializeField] private GameObject splash;
    private GameObject player;
    private HasDamage hasDamage;
    private void Start()
    {
        player = GameObject.Find("Player");
        hasDamage = player.GetComponent<HasDamage>();
        cameraGetPlayer = GameObject.FindGameObjectWithTag("cam").GetComponent<CameraGetPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasDamage.OnHasDamage();
            cameraGetPlayer.LeavePlayer();
            if (GameManager.instance.GetLives() > 0) Invoke(nameof(SetPosition), 3f);
        }
    }

    public void SetPosition()
    {
        // Use particles
        player.transform.position = flag.position;
        cameraGetPlayer.GetPlayer();
        GameObject splashObject = Instantiate(splash, flag.position, Quaternion.identity);
        SoundManager.instance.SoundEnemyDissapear();
        Destroy(splashObject, 4f);
    }
}
