using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject tooltipObject;
    private RectTransform _tooltipRectTrm;
    private RectTransform _canvasRectTrm;
    public float dist = 2f;

    private Vector3 tooltipPos;
    private bool isRight = false;
    private Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;
        tooltipObject.SetActive(false);

        tooltipPos = Camera.main.ScreenToWorldPoint(tooltipObject.transform.position);
        _tooltipRectTrm = tooltipObject.GetComponent<RectTransform>();
        _canvasRectTrm = tooltipObject.transform.parent.GetComponent<RectTransform>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isRight = gameObject.transform.position.x < col.transform.position.x ? true : false;
            Debug.Log("isRight : " + isRight);

            Vector3 leftPos = mainCam.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, dist, 0));
            //leftPos *= _canvasRectTrm.localScale.x;
            //_tooltipRectTrm.anchoredPosition  = isRight ?
            //    mainCam.WorldToScreenPoint(new Vector3(gameObject.transform.position.x - dist, tooltipPos.y))
            //    : leftPos;
            _tooltipRectTrm.position = leftPos;


            Debug.Log(leftPos);

            Debug.Log(mainCam.WorldToScreenPoint(new Vector3(gameObject.transform.position.x + dist, tooltipPos.y)));
            tooltipObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //if (!col.gameObject.CompareTag("Player")) return;

        //isRight = gameObject.transform.position.x < col.transform.position.x ? true : false;

        //tooltipObject.transform.position = isRight ?
        //    Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x - dist, -2))
        //    : Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x + dist, -2));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tooltipObject.SetActive(false);
        }
    }
}
