using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovenent : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotSpeed;

    private float h;
    private float v;
    private Vector3 moveDirection = Vector3.zero;

    public float CurrentSpeed { get { return Vector3.Magnitude(moveDirection); } }

    public void Movenent(float h, float v)
    {
        this.h = h;
        this.v = v;

        moveDirection = new Vector3(this.h, 0, this.v);
        moveDirection.Normalize();

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            transform.position += moveDirection * _moveSpeed * 2f * Time.deltaTime;
        }
        else
        {
            transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        }
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.8f);
        }
    }
}
