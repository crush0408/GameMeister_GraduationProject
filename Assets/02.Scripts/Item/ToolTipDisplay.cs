using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipDisplay : MonoBehaviour
{
    public ItemSO item;

    public Image itemIcon;
    public Text itemName;
    public Text itemUse;

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
