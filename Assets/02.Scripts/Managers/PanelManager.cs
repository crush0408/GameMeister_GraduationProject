using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject SettingPanel;

    void Update()
    {
        UIManager.instance.PanelOnOff(SettingPanel, KeyCode.Escape);
    }
}
