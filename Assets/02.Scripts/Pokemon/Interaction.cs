using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    GameInfo gameInfo = new GameInfo();
    AgentInfo agentInfo = new AgentInfo();
    Pokemon pokemon = new Pokemon();
    void Start()
    {
        gameInfo = Managers.Save.LoadJsonFile<GameInfo>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            interaction_Nurse();
        }
    }

    void interaction_Nurse() //상호작용
    {
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Nurse");
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 20, layerMask))
        {
            agentInfo = gameInfo.PlayerInfo;
            Debug.Log(agentInfo.PokemonList[0].Name);
            foreach (Pokemon poke in agentInfo.PokemonList)
            {
                pokemon.Heal(poke.MaxHp, poke);
                //여기에 상대가 있었을때 상대의 말하는 스크립트를 받아와서 함수 실행
            }
        }
    }
}
