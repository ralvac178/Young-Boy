using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController
{
    void AttackAnimation();
    void SubLives();
    void DeleteCharacter();
    void MakeTransparent();
    void PlayDust();
    void AddForcekWithContact();

}
