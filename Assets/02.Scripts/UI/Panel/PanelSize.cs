using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSize : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        SetSizeDelta(200f, 200f);
    }

    void SetSizeDelta(float width, float height)
    {
        rectTransform.sizeDelta = new Vector2(width, height);
    }
}
