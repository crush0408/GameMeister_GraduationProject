using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWarmBall : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 24f;
    public bool push = false;

    private Transform fireWarmPosition;

    private void OnEnable()
    {
        fireWarmPosition = GetComponentInParent<Transform>();

        transform.position = fireWarmPosition.position;
        transform.rotation = fireWarmPosition.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
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
