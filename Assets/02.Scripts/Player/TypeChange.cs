using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeChange : MonoBehaviour
{
    public Button btn1;
    public Button btn2;

    PlayerStat.PlayerType type1;
    PlayerStat.PlayerType type2;

    private void OnEnable()
    {
        Debug.Log("TypeChange 창 켜짐");

        switch (PlayerStat.instance.MyType)
        {
            case PlayerStat.PlayerType.NONE:
                type1 = PlayerStat.PlayerType.ICE;
                type2 = PlayerStat.PlayerType.LIGHTNING;
                break;

            case PlayerStat.PlayerType.ICE:
                type1 = PlayerStat.PlayerType.WATER;
                type2 = PlayerStat.PlayerType.LIGHTNING;
                break;

            case PlayerStat.PlayerType.LIGHTNING:
                type1 = PlayerStat.PlayerType.WATER;
                type2 = PlayerStat.PlayerType.ICE;
                break;

            case PlayerStat.PlayerType.WATER:
                type1 = PlayerStat.PlayerType.LIGHTNING;
                type2 = PlayerStat.PlayerType.ICE;
                break;
        }

        btn1.GetComponentInChildren<Text>().text = type1.ToString();
        btn2.GetComponentInChildren<Text>().text = type2.ToString();

        ChangeButtonColor(type1, btn1);
        ChangeButtonColor(type2, btn2);
    }

    public void PressTypeOne()
    {
        GameManager.instance.ChangeType(type1);
        Debug.Log("플레이어 타입 : " + PlayerStat.instance.MyType);

        PanelManager.instance.TypeChangePanel.SetActive(false);
    }

    public void PressTypeTwo()
    {
        GameManager.instance.ChangeType(type2);
        Debug.Log(PlayerStat.instance.MyType);

        PanelManager.instance.TypeChangePanel.SetActive(false);
    }

    public void ChangeButtonColor(PlayerStat.PlayerType type, Button btn)
    {
        Image image = btn.GetComponent<Image>();

        switch (type)
        {
            case PlayerStat.PlayerType.ICE:
                image.color = new Color(196 / 255f, 253 / 255f, 255 / 255f);
                break;

            case PlayerStat.PlayerType.LIGHTNING:
                image.color = new Color(250/255f, 237/255f, 125/255f);
                break;

            case PlayerStat.PlayerType.WATER:
                image.color = new Color(178 / 255f, 204 / 255f, 255 / 255f);
                break;
        }
    }
}
