using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDamage : MonoBehaviour
{
    private PlayerAnimations playerAnimationsScript;

    private SpriteRenderer sp;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        playerAnimationsScript = GetComponent<PlayerAnimations>();
    }

    public void OnHasDamage(Collider2D collider)
    {
        DisableCollider(collider);
        TurnToRed();
        playerAnimationsScript.HurtAnimation();
    }

    public void DisableCollider(Collider2D collider2D) 
    {
        collider2D.enabled = false;
        StartCoroutine(WaitSeconds(collider2D));
    }

    public void EnableCollider(Collider2D collider2D)
    {
        collider2D.enabled = true;
        StopCoroutine(WaitSeconds(collider2D));
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

    IEnumerator WaitSeconds(Collider2D collider2D)
    {
        yield return new WaitForSeconds(2f);
        EnableCollider(collider2D);
    }
}
