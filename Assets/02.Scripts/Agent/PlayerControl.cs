using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using NaughtyAttributes;

public class PlayerControl : MonoBehaviour
{


    private bool isMoveAble = false;
    private Rigidbody rigid;
    public float h, v;

    float rotationX = 0f;

    private Camera playerCam;
    private object playerCamtransform;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        Init();
    }

    void FixedUpdate()
    {
        KeyMove();
    }

    void Init()
    {
        isMoveAble = true;
    }

    void KeyMove()
    {
        if (!isMoveAble) return;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 right = transform.TransformDirection(Vector3.right);

        
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (!other.CompareTag("POKEZONE")) return;
    //    if (h + v < 0.1f) return;
    //    timer += Time.deltaTime;
    //    if (timer < 1f) return;
    //    timer = 0;
    //    if (Random.Range(0, 100.0f) > 15) return;
    //    Debug.Log("포켓몬 출현!");
    //}
}
