using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAction : AIState
{
    public override void OnStateEnter()
    {
        // ī�޶� ��ȯ!
        // ��Ʋ�̴� �ؽ�Ʈ ����!

        GameInfo gameInfo = new GameInfo();
        gameInfo.PlayerInfo = _aiBrain.Target.GetComponent<Player>().GetInfo();
        gameInfo.EnemyInfo = _aiBrain.GetComponentInParent<Enemy>().GetInfo();
        gameInfo.isWildPokemon = false;
        Managers.Save.SaveJson(gameInfo);

        Managers.Scene.LoadScene(Define.Scene.Battle);
    }

    public override void TakeAAction()
    {

    }
}
