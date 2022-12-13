using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private float _moveSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_target);
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _moveSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {
        this._target = target;
    }
}
