using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCoin : MonoBehaviour
{
    public Text coinText;

    private void Update()
    {
        coinText.text = PlayerStat.instance.Coin.ToString().PadLeft(4, '0');
    }
}
