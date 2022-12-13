using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;

    private bool isMoveAble = false;
    private Rigidbody rigid;
    public float h, v, timer;

    float rotationX = 0f;

    private Camera playerCam;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        playerCam = transform.GetComponentInChildren<Camera>();
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
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (!isMoveAble) return;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = (forward * moveSpeed * v) + (right * moveSpeed * h);

        rigid.velocity = moveDirection * Time.deltaTime;

        rotationX += -Input.GetAxis("Mouse Y") * rotSpeed;
        rotationX = Mathf.Clamp(rotationX, -60, 60);
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotSpeed, 0);
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
