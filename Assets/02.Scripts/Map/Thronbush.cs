using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thronbush : MonoBehaviour
{
    public float damage = 0f;
    public float delay = 2f;
    public bool push = false;

    private bool isTouched = false;
    private IEnumerator delayCoroutine = null;

    private void Update()
    {
        Debug.Log(isTouched);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BoxCollider2D boxCollider = collision.GetComponent<BoxCollider2D>();

        if (collision.gameObject.CompareTag("Player") && boxCollider == collision)
        {
            isTouched = true;

            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null)
            {
                Debug.Log("진입");

                delayCoroutine = Delay(delay, target);
                StartCoroutine(delayCoroutine);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BoxCollider2D boxCollider = collision.GetComponent<BoxCollider2D>();

        if (boxCollider.gameObject.CompareTag("Player"))
        {
            isTouched = false;

            delayCoroutine = null;
        }
    }

    public IEnumerator Delay(float delay, IDamageable target)
    {
        while (isTouched)
        {
            target.OnDamage(damage, transform.position, push);
            yield return new WaitForSeconds(delay);
        }
    }
}
