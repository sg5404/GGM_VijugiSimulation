using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public bool enemyIsHuman = false;
    public List<PokeInformationSO> pokemonList = new List<PokeInformationSO>();

    // enemyIsHuman가 true면 가질 데이터
    public EnemyStateInfoSO enemy = null;
    // 인밴토리
    // 적 배틀들어가기 전 시작 컷신(이건 선택 사항인듯. 안만들어도 됨)
}
