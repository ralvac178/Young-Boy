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
        sp.color = Color.red;
        Invoke(nameof(TurnToWhite), 2f);
    }

    public void TurnToWhite()
    {
        sp.color = Color.white;
    }

}
