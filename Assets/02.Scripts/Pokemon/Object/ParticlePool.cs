using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        Managers.Pool.Push(this.GetComponent<Poolable>());
    }
}
