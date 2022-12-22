using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 _topCameraPosOffset = new Vector3(0, 10, -5);

    private GameObject _target;

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, _target.transform.position + _topCameraPosOffset, 0.6f);
            this.transform.LookAt(_target.transform);
        }
    }
}
