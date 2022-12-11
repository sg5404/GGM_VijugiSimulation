using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillRange
{
    One, // ���� �Ѹ���
    AllEnemy,  // �� ���
    EvenyoneButMe, // �� ���� ���
    Evenyone // �� ���� ���
}

public class SkillSO : ScriptableObject
{
    public string skillName;
    public int power; // ����
    public int accuracyRate; // ���߷�
    public bool isMustHit = false; // ���߱�
    public bool isInstantaneousDeath = false; // �ϰ��ʻ�!
    public SkillRange skillRange = SkillRange.One;
}
