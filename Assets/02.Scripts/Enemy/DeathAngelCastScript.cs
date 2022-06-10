using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAngelCastScript : MonoBehaviour
{
    public int rotateSpeed = 3;
    public int moveSpeed = 10;
    public float damage = 5f;
    Vector2 myVelocity;
    public GameObject target;
    Rigidbody2D rigid;
    float time = 0f;
    float destroyTime = 3f;

    public GameObject explosionObj;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.playerObj;
        Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        myVelocity = dir.normalized;
        dir = -dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = angleAxis;


        time = Time.time;
    }
    void Update()
    {
        if(Time.time - time > destroyTime)
        {
            DestroyFunc();
        }
        rigid.velocity = myVelocity * 15f;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            BoxCollider2D col = collision.GetComponent<BoxCollider2D>();
            if(target != null && collision == col)
            {
                target.OnDamage(damage, this.transform.position, true);
                DestroyFunc();
            }
        }
    }

    private void DestroyFunc()
    {
        Destroy(this.gameObject);
        GameObject a = Instantiate(explosionObj);
        a.transform.position = this.transform.position;
    }
}
