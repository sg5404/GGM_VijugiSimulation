using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovenent : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotSpeed;

    public void Movenent(float h, float v)
    {
        Vector3 moveDirection = new Vector3(h, 0, v);
        moveDirection.Normalize();

        transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.8f);
        }
    }
}
