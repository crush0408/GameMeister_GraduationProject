using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipDisplay : MonoBehaviour
{
    [Header("Item")]
    public ItemSO item;

    [Header("UI")]
    public Image itemIcon;
    public Text itemName;
    public Text itemUse;

    // private int randomNum;

    private void Start()
    {

        /*
        if(randomNum < 40)
        {

        }
        else if(randomNum < 50)
        {

        }
        else if (randomNum < 60)
        {

        }
        else if (randomNum < 70)
        {

        }
        else if (randomNum < 80)
        {

        }
        else if (randomNum < 90)
        {

        }
        else if (randomNum < 97)
        {

        }
        else
        {

        }
        */
    }

    /*
    private bool PercentReturn(int percentListNum)
    {
        randomNum = Random.Range(0, 100);

        int percent = 0;

        for (int i = 0; i < percentListNum - 1; i++)
        {
            percent += percentList[i];
        }

        Debug.Log(string.Format("{0} >= {1} && {2} < {3} + {4}", randomNum, percent, randomNum, percent, percentList[percentListNum]));

        return randomNum >= percent && randomNum < percent + percentList[percentListNum] ? true : false;
    }
    */

    private void OnEnable()
    {
        Print();
    }

    public void Print()
    {
        itemIcon.sprite = item.itemImage;
        itemName.text = item.itemName;
        itemUse.text = item.itemUse;
    }
}
