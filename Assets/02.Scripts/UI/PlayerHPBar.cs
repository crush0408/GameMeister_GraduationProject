using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : HpBar
{
    public Text fillTxt;
    

    private void Start()
    {
        fill = GetComponentInChildren<Image>();
        fillTxt = GetComponentInChildren<Text>();
    }
}
