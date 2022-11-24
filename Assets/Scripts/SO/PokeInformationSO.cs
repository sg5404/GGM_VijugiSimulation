using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PokeInformationSO", menuName = "SO/PokeMon")]
public class PokeInformationSO : ScriptableObject
{
    [SerializeField] private string pokeName; //�̸�
    [SerializeField] private Image pokeImage;
    [SerializeField] private Image pokeBackImage;

    public string PokeName { get { return pokeName; } }

    public int Level; //����
    public PokeRarity Rarity; //��͵�
    public PokeType MainType; //�ּӼ�
    public PokeType SubType; //�μӼ�
    public int CurrentAttack; //���� ���ݷ�
    public int CurrentDefense; //���� ����
    public int CurrentHP; //���� ü��

    [SerializeField] private float pokeAttack; //������ ���ݷ�
    public float PokeAttack { get { return pokeAttack; } }

    [SerializeField] private float PokeHP; //������ ü��
    public float pokeHP { get { return PokeHP; } }

    [SerializeField] private float pokeDefense; //������ ����
    public float PokeDefense { get { return pokeDefense; } }
}
