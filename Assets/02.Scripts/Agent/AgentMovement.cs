using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    public UnityEvent<float, float> OnMovementEvent;

    public void Move(float h, float v)
    {
        OnMovementEvent?.Invoke(h, v);
    }
}
