using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDamage : MonoBehaviour
{
    private CharacterAnimations animationsScript;
    private EnemyController enemyController;
    private PlayerController playerController;
    private SpriteRenderer sp;
    [HideInInspector] public bool isDamage;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        enemyController = GetComponent<EnemyController>();
        sp = GetComponent<SpriteRenderer>();
        animationsScript = GetComponent<CharacterAnimations>();
    }

    public void OnHasDamage()
    {
        if (isDamage) return;
        animationsScript.HurtAnimation();
        if (transform.gameObject.CompareTag("Player"))
        {
            GameManager.instance.SubLives(1);
            TurnColorHurt();
            SoundManager.instance.SoundPlayerHurt();
            if (GameManager.instance.GetLives() <= 0)
            {
                GameManager.instance.gameOver = true;
                GameOverCanvasSingleton.instance.RestoreToGameOver();
            }
        }
        else
        {
            if (enemyController!= null && enemyController.isAlive)
            {              
                enemyController.SubLives();
                TurnColorHurt();
                if (enemyController.lives >= 1)
                {
                    SoundManager.instance.SoundEnemyHurt();    
                }
                else
                {
                    SoundManager.instance.SoundEnemyDead();
                }
            }
        }    
    }

    public void TurnColorHurt()
    {
        isDamage = true;
        if (transform.gameObject.CompareTag("Player"))
        {
            sp.color = Color.red;
        }
        else
        {
            sp.color = new Color(0.8f, 0.8f, 0.14f, 0.8f);
        }

        Invoke(nameof(TurnToWhite), 1f);
    }

    public void TurnToWhite()
    {
        isDamage = false;
        if (transform.gameObject.CompareTag("Player"))
        {
            sp.color = Color.white;
        }
        else if (enemyController != null && enemyController.lives >= 1)
        {
            sp.color = Color.white;
        }        
    }

    public void OnFinishGame()
    {
        GameManager.instance.FinishGame();
    }
}
