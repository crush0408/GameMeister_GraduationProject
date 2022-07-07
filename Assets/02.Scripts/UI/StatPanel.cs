using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    public Image profileBackground;

    [Header("스탯 텍스트 표시")]
    public Text atkText;
    public Text defText;
    public Text passText;
    public Text atkSpeedText;
    public Text moveSpeedText;

    [Space]
    public Text timeText;
    private int timeTemp;
    private int hour = 0;
    private int minute = 0;
    private int second = 0;

    private void OnEnable()
    {
        atkText.text = "공격력 : " + PlayerStat.instance.Attack.ToString();
        defText.text = "방어력 : " + PlayerStat.instance.Defense.ToString();
        passText.text = "방어관통력 : " + PlayerStat.instance.Pass.ToString();
        atkSpeedText.text = "공격 속도 : " + PlayerStat.instance.AttackSpeed.ToString();
        moveSpeedText.text = "이동 속도 : " + PlayerStat.instance.MoveSpeed.ToString();

        second = (int)(Time.time - PlayerStat.instance.StartTime);

        if (second > 59 && minute > 59)
        {
            minute = 0;
            hour++;
        }
        else if (second > 59)
        {
            PlayerStat.instance.StartTime = Time.time;
            second = 0;
            minute++;
        }

        timeText.text = string.Format("{0:00} : {1:00} : {2:00}", hour, minute, second);

        // 플레이어 프로필 백그라운드 색상 조정
        switch (PlayerStat.instance.MyType)
        {
            case PlayerStat.PlayerType.NONE:
                profileBackground.color = Color.black;
                break;
            case PlayerStat.PlayerType.ICE:
                profileBackground.color = new Color(196 / 255f, 253 / 255f, 255 / 255f);
                break;
            case PlayerStat.PlayerType.LIGHTNING:
                profileBackground.color = new Color(250 / 255f, 237 / 255f, 125 / 255f);
                break;
            case PlayerStat.PlayerType.WATER:
                profileBackground.color = new Color(178 / 255f, 204 / 255f, 255 / 255f);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerStat.instance.MyType = PlayerStat.PlayerType.ICE; // 테스트용
        }
    }
}
