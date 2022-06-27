using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [Header("아이템 관련")]
    public ItemSO[] itemList;
    public List<int> percentList;
    [SerializeField] private int randomNum;

    [Space]

    public GameObject tooltipObject;
    private RectTransform _tooltipRectTrm;
    public float yDist = 1f;
    public float xDist = 1f;

    private bool isRight = false;
    private Camera mainCam;

    private void Start()
    {
        SetTooltip();

        Debug.Log(1);
        for (int i = 0; i < itemList.Length; i++)
        {
            percentList.Add(itemList[i].percent);
        }
        Debug.Log(2);

        for (int i = 0; i < percentList.Count; i++)
        {
            if (PercentReturn(percentList[i]))
            {
                tooltipObject.GetComponent<ToolTipDisplay>().item = itemList[i];
                Debug.Log("툴팁 확인용");
            }
            else
            {
                continue;
            }
        }
    }

    private bool PercentReturn(int percentListNum)
    {
        randomNum = Random.Range(0, 100);

        int percent = 0;

        for (int i = 0; i < percentListNum; i++)
        {
            percent += percentList[i];
        }

        Debug.Log(string.Format("{0} >= {1} && {2} < {3} + {4}", randomNum, percent, randomNum, percent, percentList[percentListNum]));

        if(randomNum >= percent && randomNum < percent + percentList[percentListNum])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetTooltip()
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
