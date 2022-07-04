using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeSelect : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;

    PlayerStat.PlayerType type1;
    PlayerStat.PlayerType type2;
    PlayerStat.PlayerType type3;

    private void OnEnable()
    {
        Debug.Log("Start - TypeSelect");

        Time.timeScale = 0f;

        type1 = PlayerStat.PlayerType.WATER;
        type2 = PlayerStat.PlayerType.LIGHTNING;
        type3 = PlayerStat.PlayerType.ICE;

        btn1.GetComponentInChildren<Text>().text = type1.ToString();
        btn2.GetComponentInChildren<Text>().text = type2.ToString();
        btn3.GetComponentInChildren<Text>().text = type3.ToString();

        ChangeButtonColor(type1, btn1);
        ChangeButtonColor(type2, btn2);
        ChangeButtonColor(type3, btn3);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void PressTypeOne()
    {
        GameManager.instance.ChangeType(type1);
        Debug.Log("플레이어 타입 : " + PlayerStat.instance.MyType);

        PanelManager.instance.StartTypeSelectPanel.SetActive(false);
    }

    public void PressTypeTwo()
    {
        GameManager.instance.ChangeType(type2);
        Debug.Log("플레이어 타입 : " + PlayerStat.instance.MyType);

        PanelManager.instance.StartTypeSelectPanel.SetActive(false);
    }

    public void PressTypeThree()
    {
        GameManager.instance.ChangeType(type3);
        Debug.Log("플레이어 타입 : " + PlayerStat.instance.MyType);

        PanelManager.instance.StartTypeSelectPanel.SetActive(false);
    }

    public void ChangeButtonColor(PlayerStat.PlayerType type, Button btn)
    {
        Image image = btn.GetComponent<Image>();

        switch (type)
        {
            case PlayerStat.PlayerType.ICE:
                image.color = Color.blue;
                break;
            case PlayerStat.PlayerType.LIGHTNING:
                image.color = Color.yellow;
                break;
            case PlayerStat.PlayerType.WATER:
                image.color = Color.cyan;
                break;
        }
    }
}
