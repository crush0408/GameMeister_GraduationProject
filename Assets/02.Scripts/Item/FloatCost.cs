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

    private void OnEnable()
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

        _itemCostRectTrm = costObject.GetComponent<RectTransform>();
        _itemCostRectTrm.position = mainCam.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, yDist, 0));
    }
}
