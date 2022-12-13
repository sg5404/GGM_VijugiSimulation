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
    [SerializeField]
    private float _rotateSpeed;

    private float xAxis = 0f;
    private float yAxis = 0f;

    void LateUpdate()
    {
        //transform.LookAt(_target);
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _moveSpeed * Time.deltaTime);

        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");

        //transform.LookAt(_target.position);
        if(yAxis != 0 || xAxis != 0)
        {
            Quaternion q = _target.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x + yAxis * _rotateSpeed, q.eulerAngles.y + xAxis * _rotateSpeed, q.eulerAngles.z);
            
        }

        transform.rotation = Quaternion.Euler(yAxis, xAxis, 0);
    }

    public void SetTarget(Transform target)
    {
        this._target = target;
    }
}
