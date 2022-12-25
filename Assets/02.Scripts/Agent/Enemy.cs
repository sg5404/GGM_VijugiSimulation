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
        bool isDead = true;
        for(int i = 0; i < gameInfo.PlayerInfo.PokemonList.Length; i++)
        {
            if (gameInfo.PlayerInfo.PokemonList[i].Info != null)
            {
                if(gameInfo.PlayerInfo.PokemonList[i].Hp > 0)
                {
                    isDead = false;
                }
            }
        }

        if(isDead == true)
        {
            // Game Over
            scene?.SetText("포켓몬도 없는 녀석이.. 어딜 돌아다녀!");
            yield return new WaitForSeconds(1f);
            Managers.Save.DeleteFile();
            Managers.Scene.LoadScene(Define.Scene.Menu);
        }
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
                // 플레이어 포켓몬 예외 처리해야..하나? 어쩌피 죽으면 메튜로 돌아가는데... 그래도 해야겠지...
                _timer = _battleCoolTime;
                StartCoroutine(Battle());
            }
        }
    }
}
