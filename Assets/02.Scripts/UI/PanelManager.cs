using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject StatPanel;

    private void Start()
    {
        StatPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("현재 스탯 패널 상태 : " + StatPanel.activeSelf);
            StatPanel.SetActive(!StatPanel.activeSelf);
        }
    }
}
