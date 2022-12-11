using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillRange
{
    One, // 지정 한마리
    AllEnemy,  // 적 모두
    EvenyoneButMe, // 나 제외 모두
    Evenyone // 나 포함 모두
}

public class SkillSO : ScriptableObject
{
    public string skillName;
    public int power; // 위력
    public int accuracyRate; // 명중률
    public bool isMustHit = false; // 필중기
    public bool isInstantaneousDeath = false; // 일격필상!
    public SkillRange skillRange = SkillRange.One;
}
