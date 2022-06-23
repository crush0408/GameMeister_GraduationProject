using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("구속 및 데미지")]
    public float speed = 10f;
    public float damage = 5f;

    private Transform parentTransform;
    // private Vector3 direction;

    private void OnEnable()
    {
        parentTransform = GetComponentInParent<Transform>();

        transform.position = parentTransform.position;
        transform.rotation = parentTransform.rotation;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().OnDamage(damage, transform.position);

            Destroy(this.gameObject);
        }
    }
}
