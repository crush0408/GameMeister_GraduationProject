using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeChange : MonoBehaviour
{
    PlayerStat.PlayerType type1;
    PlayerStat.PlayerType type2;

    private void OnEnable()
    {
        Debug.Log("TypeChange 창 켜짐");

        switch (PlayerStat.instance.MyType)
        {
            case PlayerStat.PlayerType.NONE:
                type1 = PlayerStat.PlayerType.ICE_BALANCE;
                type2 = PlayerStat.PlayerType.LIGHTNING_SPEED;
                break;

            case PlayerStat.PlayerType.ICE_BALANCE:
                type1 = PlayerStat.PlayerType.LIGHTNING_SPEED;
                type2 = PlayerStat.PlayerType.WATER_POWER;
                break;

            case PlayerStat.PlayerType.LIGHTNING_SPEED:
                type1 = PlayerStat.PlayerType.ICE_BALANCE;
                type2 = PlayerStat.PlayerType.WATER_POWER;
                break;

            case PlayerStat.PlayerType.WATER_POWER:
                type1 = PlayerStat.PlayerType.ICE_BALANCE;
                type2 = PlayerStat.PlayerType.LIGHTNING_SPEED;
                break;
        }
    }

    public void PressTypeOne()
    {
        PlayerStat.instance.MyType = type1;
        Debug.Log(PlayerStat.instance.MyType);

        PanelManager.instance.TypeChangePanel.SetActive(false);
    }

    public void PressTypeTwo()
    {
        PlayerStat.instance.MyType = type2;
        Debug.Log(PlayerStat.instance.MyType);

        PanelManager.instance.TypeChangePanel.SetActive(false);
    }
}
