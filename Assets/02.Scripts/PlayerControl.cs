using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private bool isMoveAble = false;
    private Rigidbody rigid;
    public float h, v, timer;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Init();
    }

    void Update()
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
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (!isMoveAble) return;

        rigid.velocity = new Vector3(h, rigid.velocity.y, v) * moveSpeed;
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
