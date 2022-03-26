using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject SettingPanel;

    private RectTransform PanelRectTransform;

    private void Start()
    {
        PanelRectTransform = SettingPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetSize();
        }
    }

    private void SetSize()
    {
        float width = PanelRectTransform.rect.width;
        float height = PanelRectTransform.rect.height;
        Debug.Log("가로 : " + width + "세로 : " + height);

        float Left = PanelRectTransform.offsetMin.x;
        float Bottom = PanelRectTransform.offsetMin.y;

        float Right = PanelRectTransform.offsetMax.x;
        float Top = PanelRectTransform.offsetMax.y;

        PanelRectTransform.offsetMin = new Vector2(0, 0);
        PanelRectTransform.offsetMax = new Vector2(200 - Screen.width, 200 - Screen.height);
    }
}
