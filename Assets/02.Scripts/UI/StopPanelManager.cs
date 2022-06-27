using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopPanelManager : MonoBehaviour
{
    public GameObject StopPanel;
    public GameObject SettingPanel;
    public void ToGame()
    {
        StopPanel.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenMenu()
    {
        SettingPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
