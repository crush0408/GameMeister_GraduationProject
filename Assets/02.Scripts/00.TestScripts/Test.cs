using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int rotateSpeed = 3;
    Vector2 myVelocity;
    public GameObject target;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        //Vector2 dir = new Vector2(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y);
        Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        myVelocity = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        //Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
        transform.rotation = angleAxis;

    }
    void Update()
    {
        rigid.velocity = myVelocity * 15f;
        


    }
}
