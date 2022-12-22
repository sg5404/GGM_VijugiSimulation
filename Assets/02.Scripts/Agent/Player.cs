using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;

public class Player : Agent
{
    public UnityEvent<float, float> OnMovementEvent = null;
    public UnityEvent<bool> OnRunEvent = null;

    private void Start()
    {
        transform.position = new Vector3(0, 0, -100);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnMovementEvent?.Invoke(Input.GetAxis("Horizontal") * 1.5f, Input.GetAxis("Vertical") * 1.5f);
            OnRunEvent?.Invoke(true);
        }
        else
        {
            OnMovementEvent?.Invoke(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            OnRunEvent?.Invoke(false);
        }
    }
}
