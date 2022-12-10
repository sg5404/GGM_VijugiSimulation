using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Creature/PokemonHABCDS")]
public class PokemonHABCDSSO : ScriptableObject
{
    // 종종값 SO
    // 종족값
    public float hp;
    public float attack;
    public float block;
    public float speed;

    // 능력치 공식
    // [{(종족값 * 2) + 개체값} * 레벨/100] + 10 + 레벨

    // 데미지 공식
    // (((((레벨 × 2 ÷ 5) + 2) × 위력 × 특수공격 ÷ 50) ÷ 특수방어) + 2) * 급소* 상성1 * 상성2
}
