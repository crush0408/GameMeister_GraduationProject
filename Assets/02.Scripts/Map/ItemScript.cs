using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject tooltipObject;
    public float dist = 2f;

    private Vector3 tooltipPos;
    private bool isRight = false;

    private void Start()
    {
        tooltipObject.SetActive(false);

        tooltipPos = Camera.main.ScreenToWorldPoint(tooltipObject.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isRight = gameObject.transform.position.x < col.transform.position.x ? true : false;
            Debug.Log("isRight : " + isRight);

            tooltipObject.transform.position = isRight ?
                Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x - dist, tooltipPos.y))
                : Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x + dist, tooltipPos.y));

            Debug.Log(Camera.main.ScreenToWorldPoint(tooltipObject.transform.position));
            tooltipObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        isRight = gameObject.transform.position.x < col.transform.position.x ? true : false;

        tooltipObject.transform.position = isRight ?
            Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x - dist, -2))
            : Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x + dist, -2));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tooltipObject.SetActive(false);
        }
    }
}
