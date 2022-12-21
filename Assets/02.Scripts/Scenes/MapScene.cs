 using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : BaseScene
{
    private CameraController _cc;

    private GameInfo _gameInfo;

    private Player _player;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Map;

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();

        _player = Managers.Resource.Instantiate("Player/Player").GetComponent<Player>();

        _player.SetInfo(_gameInfo.PlayerInfo);
        _player.transform.position = new Vector3(_gameInfo.PlayerInfo.position.x, _gameInfo.PlayerInfo.position.y, _gameInfo.PlayerInfo.position.z);

        _cc = Camera.main.GetComponent<CameraController>();
        _cc.SetTarget(_player.gameObject);
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
