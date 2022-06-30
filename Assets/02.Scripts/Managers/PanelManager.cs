using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;

    public GameObject StatPanel;
    public GameObject TypeChangePanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StatPanel.SetActive(false);
        TypeChangePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StatPanel.SetActive(!StatPanel.activeSelf);
            Time.timeScale = StatPanel.activeSelf ? 0f : 1f;
        }
    }
}
