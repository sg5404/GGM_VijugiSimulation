using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillRange
{
    OneEnemy, // ���� �Ѹ���
    AllEnemy,  // �� ���
    EvenyoneButMe, // �� ���� ���
    Evenyone, // �� ���� ���
}

public enum SkillTransform
{
    Me,
    Enemy
}

[CreateAssetMenu(menuName = "SO/Creature/Pokemon/Skill/SkillData"), System.Serializable]
public class SkillSO : ScriptableObject
{
    public string skillName; // ��ų �̸�
    public string skillDescription; // ��ų ����

    public Define.PokeType type;
    public int power; // ����
    public int accuracyRate; // ���߷�
    public bool isMustHit = false; // ���߱�
    public bool isInstantaneousDeath = false; // �ϰ��ʻ�!
    public SkillRange skillRange = SkillRange.OneEnemy; // ��ų ����

    public SkillTransform skillTransform;
    [ShowAssetPreview(64, 64)]
    public GameObject skillEffectPrefab;
}
