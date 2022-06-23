using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public string itemUse;

    public ItemType itemType;
    public enum ItemType
    {
        STAT,
        GOLD,
        TYPE
    }
}
