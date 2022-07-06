using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;

    public GameObject StatPanel;
    public GameObject TypeChangePanel;
    public GameObject GameoverPanel;
    public GameObject StartTypeSelectPanel;

    public Image playerProfileBackground;

    public bool isTuto = false;

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
        GameoverPanel.SetActive(false);
        if(isTuto)
            StartTypeSelectPanel.SetActive(true);

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
