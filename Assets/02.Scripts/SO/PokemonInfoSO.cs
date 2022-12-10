using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Creature/PokeMonInfo")]
public class PokemonInfoSO : ScriptableObject
{
    // ������
    public float hp;
    public float attack;
    public float defence;
    public float speed;

    // �ɷ�ġ ����
    // [{(������ * 2) + ��ü��} * ����/100] + 10 + ����

    // ������ ����
    // (((((���� �� 2 �� 5) + 2) �� ���� �� Ư������ �� 50) �� Ư�����) + ) * �޼�* ��1 * ��2
}
