using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PokeInformationSO", menuName = "SO/Creature/PokeMon")]
public class PokeInformationSO : ScriptableObject
{
    [SerializeField] private string pokeName; //�̸�
    [SerializeField] private Image pokeImage;
    [SerializeField] private Image pokeBackImage;

    public string PokeName { get { return pokeName; } }

    public int Level; //����
    public Define.PokeRarity Rarity; //��͵�
    public Define.PokeType MainType; //�ּӼ�
    public Define.PokeType SubType; //�μӼ�
    public int CurrentAttack; //���� ���ݷ�
    public int CurrentDefense; //���� ����
    public int CurrentHP; //���� ü��
    public int CurrentSpeed; // ���� ���ǵ�

    [SerializeField] private float pokeAttack; //������ ���ݷ�
    public float PokeAttack { get { return pokeAttack; } }

    [SerializeField] private float pokeHP; //������ ü��
    public float PokeHP { get { return pokeHP; } }

    [SerializeField] private float pokeDefense; //������ ����
    public float PokeDefense { get { return pokeDefense; } }

    [SerializeField] private float pokeSpeed; // ������ ���ǵ�
    public float PokeSpeed { get { return pokeSpeed; } }

    [SerializeField]
    private List<Skill> skillList;
    public List<Skill> SkillList { get { return skillList; } }
    [SerializeField]
    private int skillCnt = 2;
    public int SkillCnt => skillCnt;
}
