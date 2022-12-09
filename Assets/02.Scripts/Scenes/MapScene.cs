using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : BaseScene
{
    private BattleInfo _battleInfo;

    private Player player;
    
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Map;

        _battleInfo = Managers.Save.LoadJsonFile<BattleInfo>();

        player.SetInfo(_battleInfo.PlayerInfo);
    }

    public override void Clear()
    {
        
    }
}
