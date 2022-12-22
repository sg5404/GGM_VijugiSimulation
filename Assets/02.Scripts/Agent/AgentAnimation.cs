using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    private Animator _animator;

    private readonly int _hashRun = Animator.StringToHash("isRun");
    private readonly int _hashMove = Animator.StringToHash("move");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void MovementAnimation(float h, float v)
    {
        if(h != 0 || v != 0)
        {
            _animator.SetFloat(_hashMove, 1);
        }
        else
        {
            _animator.SetFloat(_hashMove, 0);
        }
    }

    public void DashAnimation(bool dash)
    {
        _animator.SetFloat(_hashRun, dash ? 1 : 0);
    }
}
