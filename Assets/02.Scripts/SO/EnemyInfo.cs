using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public bool enemyIsHuman = false;
    public List<PokeInformationSO> enemyPokemonList = new List<PokeInformationSO>();
    public List<PokeInformationSO> playerPokemonList = new List<PokeInformationSO>();

    // enemyIsHuman가 true면 가질 데이터
    public EnemyStateInfoSO enemy = null;
    // 인밴토리
}
