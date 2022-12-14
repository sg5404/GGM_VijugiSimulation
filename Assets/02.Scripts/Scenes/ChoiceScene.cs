using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceScene : BaseScene
{
    private GameInfo _gameInfo;

    [SerializeField]
    private List<PokemonInfoSO> _pokemonInfoList;
    [SerializeField]
    private int _startLevel;

    [SerializeField]
    private ItemSO _startItem;
    [SerializeField]
    private int _cnt;

    protected override void Init()
    {
        SceneType = Define.Scene.Choice;

        _gameInfo = new GameInfo();
    }

    public void ChoicePokemon(int i)
    {
        _gameInfo.PlayerInfo.PokemonList[0] = new Pokemon(_pokemonInfoList[i], _startLevel);
        _gameInfo.PlayerInfo.itemList.Add(new ItemPair(_startItem, _cnt));
        Managers.Save.DeleteFile();
        Managers.Save.SaveJson(_gameInfo);
        Managers.Scene.LoadScene(Define.Scene.Map);
    }

    public override void Clear()
    {

    }
}
