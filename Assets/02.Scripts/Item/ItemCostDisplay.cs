using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCostDisplay : MonoBehaviour
{
    [Header("Item")]
    public ItemSO item;

    [Header("UI")]
    public Text itemCost;

    private void OnEnable()
    {
        item = GetComponentInParent<ItemScript>().item;
        Print();
    }

    public void Print()
    {
        itemCost.text = item.cost.ToString();
    }
}
