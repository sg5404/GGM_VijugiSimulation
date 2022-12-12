using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    private void Start()
    {
        Debug.Log($"isFull : {IsFullPokemonList()}");
        Debug.Log($"isEmpty : {IsEmptyPokemonList()}");
    }
}
