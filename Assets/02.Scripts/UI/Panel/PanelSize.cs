using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSize : MonoBehaviour
{
    public Image testPanel;

    private float panelWidth = 500f;
    private float panelHeight = 300f;

    private RectTransform rt;

    private void Start()
    {
        testPanel.rectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);

        rt = testPanel.GetComponent<RectTransform>();
        rt.offsetMin = rt.offsetMax = Vector2.zero;

        rt.anchoredPosition = transform.position;
    }
}
