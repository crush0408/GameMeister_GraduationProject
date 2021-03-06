using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToolTip : MonoBehaviour
{
    [Header("ToolTip")]
    public GameObject tooltipObject;
    private ItemSO getItem;
    private RectTransform _tooltipRectTrm;
    public float yDist = 1f;
    public float xDist = 1f;

    private bool isRight = false;
    private Camera mainCam;

    private void OnEnable()
    {
        SetTooltip();
    }

    private void SetTooltip()
    {
        mainCam = Camera.main;
        tooltipObject.SetActive(false);

        _tooltipRectTrm = tooltipObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (tooltipObject.activeSelf && Input.GetKeyDown(KeyCode.G))
        {
            getItem = GetComponentInChildren<ToolTipDisplay>().item;

            if (StageManager.instance.insertData.isStore)
            {
                if (!GameManager.instance.AddCoin(-getItem.cost))
                {
                    GetComponentInChildren<ToolTipDisplay>().warnText.text = "코인이 부족합니다";
                    Debug.Log("코인이 부족합니다");
                }
                else
                {
                    Debug.Log("획득한 아이템 : " + getItem.itemName);
                    
                    /*
                    // 임시
                    if(getItem.itemName == "Sustain\nMagic")
                    {
                        GameManager.instance.isGetSustain = true;
                        Debug.Log("서스테인 스킬 얻음");
                    }
                    */

                    gameObject.GetComponent<ItemScript>().ApplyItemfunction(getItem);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Debug.Log("획득한 아이템 : " + getItem.itemName);
                gameObject.GetComponent<ItemScript>().ApplyItemfunction(getItem);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // GetComponentInChildren<ToolTipDisplay>().warnText.text = "";

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
