using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWarmBall : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 24f;
    public bool push = false;

    private Transform fireWarmPosition;

    public Vector3 rightDirection;
    public Vector3 leftDirection;

    private GameObject player;
    private bool isRight = false;

    private void OnEnable()
    {
        fireWarmPosition = GetComponentInParent<Transform>();

        transform.position = fireWarmPosition.position;
        transform.rotation = fireWarmPosition.rotation;

        rightDirection = Vector3.one;
        leftDirection = new Vector3(-rightDirection.x, rightDirection.y, rightDirection.z);
        player = GameObject.Find("Player");

        Vector2 dir = player.transform.position - this.transform.position;
        if (dir.x > 0)
        {
            transform.localScale = rightDirection;
            isRight = true;
        }
        else
        {
            transform.localScale = leftDirection;
            isRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(damage, transform.position, push);
                Destroy(gameObject);
            }
        }
    }
}
