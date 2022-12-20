using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using NaughtyAttributes;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;

    [SerializeField]
    private Vector3 topCameraPosOffset;

    private bool isMoveAble = false;
    private Rigidbody rigid;
    public float h, v, timer;

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
        timer = 0;
    }

    void KeyMove()
    {
        if (!isMoveAble) return;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = new Vector3(h, 0, v);
        moveDirection.Normalize();

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if(moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.8f);
        }

        playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, transform.position + topCameraPosOffset, 0.6f);
        playerCam.transform.LookAt(this.transform);
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
