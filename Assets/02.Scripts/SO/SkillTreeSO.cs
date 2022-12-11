using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillPair
{
    public int level;
    public SkillSO skill;
}

[CreateAssetMenu(menuName = "SO/Creature/Pokemon/SkillTree"), System.Serializable]
public class SkillTreeSO : ScriptableObject
{
    public List<SkillPair> pairs;
}
