using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Agent
{
    public bool isDebug = true;

    public PokemonInfoSO _pokemon;
    public ItemSO item;

    public UnityEvent<float, float> OnMovementEvent = null;
    public UnityEvent<bool> OnRunEvent = null;

    private void Start()
    {


        // transform.position = new Vector3(0, 0, -100);
    }

    private void Update()
    {
        OnMovementEvent?.Invoke(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnRunEvent?.Invoke(false);

        if (isDebug == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < 6; i++)
                {
                    _pokemonList[i] = new Pokemon(_pokemon, 50);
                }

                SetItem(item, 100);
            }
        }
    }
}