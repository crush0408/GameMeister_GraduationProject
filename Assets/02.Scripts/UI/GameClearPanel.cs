using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearPanel : MonoBehaviour
{
    public Text resultText;
    public Text timeText;

    int hour;
    int minute;
    int second;

    private void OnEnable()
    {
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
        resultText.text = second == 0 ? "조건" : "탈출 실패"; // 임시용

    }
}
