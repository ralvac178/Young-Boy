using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetGem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.NextLevel();
            SoundManager.instance.SoundPlayerGotGem();
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }       
    }
}
