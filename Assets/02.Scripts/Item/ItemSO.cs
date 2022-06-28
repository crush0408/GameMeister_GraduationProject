using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        STAT,
        GOLD,
        TYPE
    }

    public Sprite tooltipImage; // 아이템 이미지
    public Sprite itemImage;    // 툴팁에 넣을 이미지
    public string itemName;     // 아이템 이름
    public string itemUse;      // 아이템 설명
    public int percent; // 확률
    public int cost;    // 가격

    public ItemType itemType;
}
