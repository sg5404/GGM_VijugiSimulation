using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPair
{
    public ItemSO item;
    public int cnt;

    public ItemPair()
    {
        this.item = null;
        this.cnt = 0;
    }

    public ItemPair(ItemSO item, int cnt)
    {
        this.item = item;
        this.cnt = cnt;
    }
}

[System.Serializable]
public class AgentInfo
{
    public string Name;
    public Pokemon[] PokemonList = new Pokemon[6];
    //public Dictionary<ItemSO, int> itemDict = new Dictionary<ItemSO, int>(); // ��ųʸ��� ����ȭ �ȵ�
    public List<ItemPair> itemList = new List<ItemPair>();
    public Vector3 position = new Vector3(0, 1, 0);

    public AgentInfo()
    {
        Name = "poke";

        for(int i = 0; i < 6; i++)
        {
            PokemonList[i] = null;
        }
        //itemDict = new Dictionary<ItemSO, int>();
        itemList = new List<ItemPair>();
        position = new Vector3(0, 1, 0);
    }
}

[System.Serializable]
public class GameInfo
{
    public AgentInfo PlayerInfo;
    public AgentInfo EnemyInfo;

    public bool isWildPokemon;
    public Pokemon wildPokemon;

    public GameInfo()
    {
        PlayerInfo = new AgentInfo();
        EnemyInfo = new AgentInfo();
        isWildPokemon = false;
        wildPokemon = null;
    }
}
