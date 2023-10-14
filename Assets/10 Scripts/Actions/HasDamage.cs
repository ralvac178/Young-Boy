using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDamage : MonoBehaviour
{
    private CharacterAnimations playerAnimationsScript;

    private SpriteRenderer sp;
    private Rigidbody2D rb2D;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
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
