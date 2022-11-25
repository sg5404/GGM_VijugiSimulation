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
            Debug.Log($"�̸�:{info.name}, ����:{info.Level}, ���ݷ�:{info.CurrentAttack}, ����:{info.CurrentDefense}, ü��:{info.CurrentHP}");
        }

        // TODO : 1. UI�� ���� ����
        // TODO : 2. UI Amination
    }

    public override void Clear()
    {
        
    }
}
