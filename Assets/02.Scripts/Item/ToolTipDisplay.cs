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

    private void OnEnable()
    {
        item = GetComponentInParent<ItemScript>().item;
        Print();
    }

    public void Print()
    {
        itemIcon.sprite = item.itemImage;
        itemName.text = item.itemName;
        itemUse.text = item.itemUse;
    }
}
