using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    public Transform Player;
    public bool facingRight = true; //���� ������ ���� �ִ°�?
    public float viewRange = 10f;//�þ߰Ÿ�
    [Range(0, 360)]
    public float viewAngle = 40f;//�þ߰���
    public LayerMask obstacleLayer;//��ֹ� ����
    private int playerLayer;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("PLAYER");//�̸����� ���̾�ã�³�
    }
    public Vector2 GetFront()
    {
        //if (spriteRender == null)
        //{
        //    return transform.right;
        //}
        if (facingRight)
        {
            return transform.localScale.x > 0 ? transform.right : transform.right * -1;
        }
        else
        {
            return transform.localScale.x > 0 ? transform.right * -1 : transform.right;
        }
    }

    public Vector2 CirclePoint(float angle)
    {
        angle += GetFront().x < 0 ? -90f : 90f;

        return new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool IsViewPlayer()
    {
        bool isView = false;
        Vector2 dir = Player.position - transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir.normalized, viewAngle, LayerMask.GetMask("PLAYER"));

        if (hit2D.collider != null)
        {
            isView = (hit2D.collider.gameObject.CompareTag("Player"));

        }

        return isView;
    }

    public bool IsTracePlayer()
    {
        bool isTrace = false;

        // �����ڵ带 ����Ϸ��� LayerMask.GetMask("PLAYER")�� ��ü�Ͽ� �����
        Collider2D col = Physics2D.OverlapCircle(transform.position, viewRange, LayerMask.GetMask("PLAYER"));
        if (col != null)
        {
            //z���� ���������� Vector2�� ���� ����ȯ
            Vector2 dir = Player.position - transform.position;
            if (Vector2.Angle(GetFront(), dir) < viewAngle * 0.5f)
            {
                isTrace = true;
            }
        }
        return isTrace;
    }
}
