using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatCost : MonoBehaviour
{
    [Header("ToolTip")]
    public GameObject costObject;
    private RectTransform _itemCostRectTrm;
    public float yDist = -2f;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;

        if (StageManager.instance.insertData.isStore)
        {
            costObject.SetActive(true);
        }
        else
        {
            costObject.SetActive(false);
        }

        // _itemCostRectTrm = costObject.GetComponent<RectTransform>();

        costObject.transform.position = PlayerStat.instance.gameObject.transform.position;

        Debug.Log("costObject 현재 상태 : " + costObject.activeSelf);
        Debug.Log(mainCam.ScreenToWorldPoint(gameObject.transform.position));

        // _itemCostRectTrm = costObject.GetComponent<RectTransform>();
        // _itemCostRectTrm.position = mainCam.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, 0, 0));
    }
}
