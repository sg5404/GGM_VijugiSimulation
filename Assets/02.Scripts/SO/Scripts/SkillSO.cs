using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillRange
{
    OneEnemy, // 지정 한마리
    AllEnemy,  // 적 모두
    EvenyoneButMe, // 나 제외 모두
    Evenyone, // 나 포함 모두
}

[CreateAssetMenu(menuName = "SO/Creature/Pokemon/Skill/SkillData"), System.Serializable]
public class SkillSO : ScriptableObject
{
    public string skillName; // 스킬 이름
    public string skillDescription; // 스킬 설명

    public Define.PokeType type;
    public int power; // 위력
    public int accuracyRate; // 명중률
    public bool isMustHit = false; // 필중기
    public bool isInstantaneousDeath = false; // 일격필상!
    public SkillRange skillRange = SkillRange.OneEnemy; // 스킬 범위
}
