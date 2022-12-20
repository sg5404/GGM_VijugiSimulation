using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            interaction();
        }
    }

    void interaction() //상호작용
    {
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Enemy") | LayerMask.GetMask("Objects");
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 20, layerMask))
        {
            Debug.Log("물체 있음");
        }
    }
}
