using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class JVector3
//{
//    [JsonProperty("x")]
//    public float x;
//    [JsonProperty("y")]
//    public float y;
//    [JsonProperty("z")]
//    public float z;

//    public JVector3()
//    {
//        x = y = z = 0;
//    }

//    public JVector3(float x, float y, float z)
//    {
//        this.x = x;
//        this.y = y;
//        this.z = z;
//    }

//    public JVector3(Transform transform)
//    {
//        this.x = transform.position.x;
//        this.y = transform.position.y;
//        this.z = transform.position.z;
//    }

//    public JVector3(Vector3 pos)
//    {
//        this.x = pos.x;
//        this.y = pos.y;
//        this.z = pos.z;
//    }
//}

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
    //public Dictionary<ItemSO, int> itemDict = new Dictionary<ItemSO, int>(); // µñ¼Å³Ê¸®´Â Á÷·ÄÈ­ ¾ÈµÊ
    public List<ItemPair> itemList = new List<ItemPair>();
    public Vector3 position = new Vector3(0, 1, 0);

    public AgentInfo()
    {
        Name = "ºñÁÖ±â";

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
