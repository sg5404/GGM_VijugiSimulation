using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public bool enemyIsHuman = false;
    public List<PokeInformationSO> enemyPokemonList = new List<PokeInformationSO>();
    public List<PokeInformationSO> playerPokemonList = new List<PokeInformationSO>();

    // enemyIsHuman�� true�� ���� ������
    public EnemyStateInfoSO enemy = null;
    // �ι��丮
}
