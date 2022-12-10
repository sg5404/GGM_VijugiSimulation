using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Creature/PokemonHABCDS")]
public class PokemonHABCDSSO : ScriptableObject
{
    // ������ SO
    // ������
    public float hp;
    public float attack;
    public float block;
    public float speed;

    // �ɷ�ġ ����
    // [{(������ * 2) + ��ü��} * ����/100] + 10 + ����

    // ������ ����
    // (((((���� �� 2 �� 5) + 2) �� ���� �� Ư������ �� 50) �� Ư�����) + 2) * �޼�* ��1 * ��2
}
