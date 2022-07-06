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

    public enum GetItem
    {
        BOTH,
        STAGE,
        STORE
    }

    public Sprite tooltipImage; // 아이템 이미지
    public Sprite itemImage;    // 툴팁에 넣을 이미지
    [TextArea]
    public string itemName;     // 아이템 이름
    [TextArea]
    public string itemUse;      // 아이템 설명
    public int percent; // 확률
    public int cost;    // 가격

    [Header("효과")]
    public int addHp;
    public int addAtk;
    public int addDef;
    public int addPass; // 방어관통력
    public int addCoin;
    public float addMoveSpeed;
    public float addAtkSpeed;

    [Space]
    public ItemType itemType;
    public GetItem getType;
}
