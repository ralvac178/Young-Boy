using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasChangeAttackType : MonoBehaviour
{
    public void EnableArrowAttackItem()
    {
        GameManager.instance.EnableArrowAttackItem();
    }

    public void EnablePunchAttackItem()
    {
        GameManager.instance.EnablePunchAttackItem();
    }
}
