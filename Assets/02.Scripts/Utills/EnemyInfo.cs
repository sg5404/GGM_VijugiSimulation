using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public List<float> EnemyBattleCoolTime;

    public EnemyInfo()
    {
        EnemyBattleCoolTime = new List<float>();
    }
}
