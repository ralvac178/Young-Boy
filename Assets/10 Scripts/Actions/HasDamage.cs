using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDamage : MonoBehaviour
{
    private CharacterAnimations animationsScript;
    private EnemyController enemyController;
    private SpriteRenderer sp;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        sp = GetComponent<SpriteRenderer>();
        animationsScript = GetComponent<CharacterAnimations>();
    }

    public void OnHasDamage()
    {
        
        animationsScript.HurtAnimation();
        if (transform.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.SoundPlayerHurt();
            GameManager.instance.SubLives(1);
            TurnToRed();
        }
        else
        {
            if (enemyController!= null && enemyController.isAlive)
            {              
                enemyController.SubLives();
                if (enemyController.lives > 1)
                {
                    SoundManager.instance.SoundEnemyHurt();
                    TurnToRed();
                }
            }
        }    
    }

    public void TurnToRed()
    {
        if (transform.gameObject.CompareTag("Player"))
        {
            sp.color = Color.red;
        }
        else
        {
            sp.color = Color.yellow;
        }

        Invoke(nameof(TurnToWhite), 2f);

    }

    public void TurnToWhite()
    {
        sp.color = Color.white;
    }

}
