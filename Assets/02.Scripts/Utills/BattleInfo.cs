using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AgentInfo
{
    public Pokemon[] PokemonList = new Pokemon[6];
    public Dictionary<Item, int> itemDict = new Dictionary<Item, int>();
}

[System.Serializable]
public class BattleInfo
{
    public AgentInfo PlayerInfo;
    public AgentInfo EnemtInfo;
}
