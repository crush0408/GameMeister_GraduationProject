using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoard : MonoBehaviour
{
    private Rigidbody2D rigid;

    public int jumpForce = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            rigid = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rigid.velocity.y < 0)  // collision.gameObject.GetComponent<PlayerMove>().isGround
            {
                Debug.Log("점프 중이고 점프 발판에 닿았음");
                rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }

    }
}
