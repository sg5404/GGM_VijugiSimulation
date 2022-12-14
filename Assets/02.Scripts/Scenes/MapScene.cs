 using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : BaseScene
{
    private GameInfo _gameInfo;

    private Player _player;

    private Camera _mainCam;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Map;

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();

        _mainCam = Camera.main;
        _player = Managers.Resource.Instantiate("Player/Player").GetComponent<Player>();

        _player.SetInfo(_gameInfo.PlayerInfo);
        _player.transform.position = _gameInfo.PlayerInfo.position;
        if (_player != null)
        {
            _mainCam.transform.SetParent(_player.transform);
        }
    }

    public override void Clear()
    {
        if(_player != null)
        {
            Camera camera = _player.GetComponentInChildren<Camera>();
            if(camera != null)
            {
                camera.transform.SetParent(null);
            }

            Managers.Pool.Push(_player.GetComponent<Poolable>());
            _player = null;
        }
    }
}
