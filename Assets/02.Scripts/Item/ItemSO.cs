using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public string itemUse;

    public ItemType itemType;
    public enum ItemType
    {
        STAT,
        GOLD,
        TYPE
    }
}
