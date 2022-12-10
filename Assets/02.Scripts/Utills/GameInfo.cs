using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AgentInfo
{
    public Pokemon[] PokemonList = new Pokemon[6];
    public Dictionary<Item, int> itemDict = new Dictionary<Item, int>();
    public Vector3 position = new Vector3(0, 1, 0);
}

[System.Serializable]
public class GameInfo
{
    public AgentInfo PlayerInfo;
    public AgentInfo EnemyInfo;

    public bool isWildPokemon;
    public Pokemon wildPokemon;
}
