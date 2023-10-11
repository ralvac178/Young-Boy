using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDamage : MonoBehaviour
{
    private CharacterAnimations playerAnimationsScript;

    private SpriteRenderer sp;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        playerAnimationsScript = GetComponent<CharacterAnimations>();
    }

    public void OnHasDamage()
    {
        TurnToRed();
        playerAnimationsScript.HurtAnimation();
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
