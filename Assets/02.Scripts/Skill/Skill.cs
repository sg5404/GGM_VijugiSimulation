using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이거 쓴려나?
public abstract class Skill : MonoBehaviour
{
    private int _power;
    public int Power => _power;

    private int _accuracyRate;
    public int AccuracyRate => _accuracyRate;

    private bool unconditionallyHit;
    public abstract void UseSkill();
}
