using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeChange : MonoBehaviour
{
    public Text btn1Text;
    public Text btn2Text;

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
                type1 = PlayerStat.PlayerType.LIGHTNING;
                type2 = PlayerStat.PlayerType.WATER;
                break;

            case PlayerStat.PlayerType.LIGHTNING:
                type1 = PlayerStat.PlayerType.ICE;
                type2 = PlayerStat.PlayerType.WATER;
                break;

            case PlayerStat.PlayerType.WATER:
                type1 = PlayerStat.PlayerType.ICE;
                type2 = PlayerStat.PlayerType.LIGHTNING;
                break;
        }

        btn1Text.text = type1.ToString();
        btn2Text.text = type2.ToString();
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
}
