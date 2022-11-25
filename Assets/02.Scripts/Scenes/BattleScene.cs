using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    private EnemyInfo enemyInfo;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Battle;

        enemyInfo = Managers.Instance.LoadJsonFile<EnemyInfo>();

        foreach (var info in enemyInfo.pokemonList)
        {
            Debug.Log($"이름:{info.name}, 레벨:{info.Level}, 공격력:{info.CurrentAttack}, 방어력:{info.CurrentDefense}, 체력:{info.CurrentHP}");
        }

        // TODO : 1. UI에 정보 연결
        // TODO : 2. UI Amination
    }

    public override void Clear()
    {
        
    }
}
