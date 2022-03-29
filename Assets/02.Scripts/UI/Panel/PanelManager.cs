using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject SettingPanel;
    public GameObject DeadPanel;

    void Update()
    {
        UIManager.instance.PanelOnOff(SettingPanel, KeyCode.Escape);
    }
}
