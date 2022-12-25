using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Agent
{
    [SerializeField]
    private string enemyName;
    [SerializeField]
    private List<PokemonInfoSO> _startPokemonInfo;
    [SerializeField]
    private int _pokemonLevel;
    [SerializeField]
    private string _battleStartText;

    [SerializeField]
    private float _battleCoolTime = 60f;
    public string BattleStartText => _battleStartText;

    public UnityEvent<float, float> OnMovementEvent;

    private NavMeshAgent _agent;
    private FieldOfView _fov;

    private float _timer = 0f;
    private int _id;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _fov = GetComponent<FieldOfView>();

        for (int i = 0; i < _startPokemonInfo.Count; i++)
        {
            SetPokemonOfIndex(new Pokemon(_startPokemonInfo[i], _pokemonLevel), i);
        }

        _name = enemyName;
    }

    public void SetID(int i)
    {
        _id = i;
    }

    public float GetTime()
    {
        return _timer;
    }

    public void SetTIme(float time)
    {
        _timer = time;
    }

    public IEnumerator Battle()
    {
        GameInfo gameInfo = new GameInfo();
        MapScene scene = Managers.Scene.CurrentScene as MapScene;
        gameInfo.PlayerInfo = scene.Player.GetInfo();
        gameInfo.EnemyInfo = this.GetInfo();
        gameInfo.isWildPokemon = false;
        Managers.Save.SaveJson(gameInfo);
        scene?.SetText(BattleStartText);

        yield return new WaitForSeconds(1f);
        scene.TrainerBattle();
    }

    private void Update()
    {
        OnMovementEvent?.Invoke(_agent.isStopped == true ? 1 : 0, _agent.isStopped == true ? 1 : 0);

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0f)
        {
            if (_fov.SearchEneny)
            {
                _timer = _battleCoolTime;
                StartCoroutine(Battle());
            }
        }
    }
}
