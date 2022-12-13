using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : BaseScene
{
    private GameInfo _gameInfo;

    private Player _player;

    private CameraController _cc;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Map;

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();

        //_player = Managers.Resource.Instantiate("Player/Player").GetComponent<Player>();

        _cc = Camera.main.GetComponent<CameraController>();
        //_player.SetInfo(_gameInfo.PlayerInfo);
        //_player.transform.position = _gameInfo.PlayerInfo.position;
        //_cc.SetTarget(_player.transform);
    }

    public override void Clear()
    {
        if(_player != null)
        {
            Managers.Pool.Push(_player.GetComponent<Poolable>());
            _player = null;
        }
    }
}
