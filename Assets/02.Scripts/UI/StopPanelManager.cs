using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopPanelManager : MonoBehaviour
{
    public GameObject StopPanel;
    public GameObject SettingPanel;
    public GameObject WarningPanel;
    public void ToGame()
    {
        StopPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenMenu()
    {
        StopPanel.SetActive(false);
        SettingPanel.SetActive(true);
    }

    public void Quit()
    {
        WarningPanel.SetActive(true);
    }

    public void RealQuit()
    {
        Application.Quit();
    }

    public void CloseWarningPanel()
    {
        WarningPanel.SetActive(false);
    }
}
