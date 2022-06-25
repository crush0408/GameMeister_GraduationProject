using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject tooltipObject;
    private RectTransform _tooltipRectTrm;
    public float yDist = 1f;
    public float xDist = 2f;

    private bool isRight = false;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        tooltipObject.SetActive(false);

        _tooltipRectTrm = tooltipObject.GetComponent<RectTransform>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isRight = gameObject.transform.position.x < col.transform.position.x ? true : false;
            Debug.Log("isRight : " + isRight);

            if (isRight)
            {
                _tooltipRectTrm.position = mainCam.WorldToScreenPoint(gameObject.transform.position + new Vector3(-xDist, yDist, 0));
            }
            else
            {
                _tooltipRectTrm.position = mainCam.WorldToScreenPoint(gameObject.transform.position + new Vector3(xDist, yDist, 0));
            }

            tooltipObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        isRight = gameObject.transform.position.x < col.transform.position.x ? true : false;

        if (isRight)
        {
            _tooltipRectTrm.position = mainCam.WorldToScreenPoint(gameObject.transform.position + new Vector3(-xDist, yDist, 0));
        }
        else
        {
            _tooltipRectTrm.position = mainCam.WorldToScreenPoint(gameObject.transform.position + new Vector3(xDist, yDist, 0));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tooltipObject.SetActive(false);
        }
    }
}
