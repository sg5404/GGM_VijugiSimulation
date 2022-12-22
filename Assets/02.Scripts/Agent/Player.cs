using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;

public class Player : Agent
{
    // Debug Code
    [SerializeField]
    private List<PokemonInfoSO> _startPokemonInfo;
    [SerializeField]
    private ItemSO _item;
    public int _cnt = 5;
    // Debug Code

    public UnityEvent<float, float> OnMovementEvent = null;

    private void Start()
    {
        // Debug Code

        if (IsEmptyPokemonList())
        {
            for (int i = 0; i < _startPokemonInfo.Count; i++)
            {
                SetPokemonOfIndex(new Pokemon(_startPokemonInfo[i], 3), i);
            }
        }

        if(IsGetItem(_item) == false)
        {
            SetItem(_item, _cnt);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnMovementEvent?.Invoke(Input.GetAxis("Horizontal") * 1.5f, Input.GetAxis("Vertical") * 1.5f);
        }
        else
        {
            OnMovementEvent?.Invoke(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
